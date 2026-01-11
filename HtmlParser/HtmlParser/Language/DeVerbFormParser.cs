using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HtmlAgilityPack;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;
using HtmlParser.Language.VerbFormParsers;

namespace HtmlParser.Language
{
    public class DeVerbFormParser : LanguageParser, ILanguageParser
    {

        public DeVerbFormParser()
            : base(false, WordType.Verb)
        { }

        public void Parse(IList<string> lines)
        {
            var existing = GetExistingVerbs();


            var list = lines.Where(l => !string.IsNullOrEmpty(l))
                    .Select(l => l.Replace("|", ""))
                    .Distinct()
                    .Where(l => !existing.Contains(l)).ToArray();


            foreach (var item in list)
            {
                var verbs = Parse(item.Trim());
                foreach (var verbForm in verbs)
                {
                    if (verbForm.List == null)
                    {
                        continue;
                    }

                    WriteFile(verbForm);
                }
            }

            //.SelectMany(l => Parse(l.Trim()))
            //.Where(l => l != null)
            //.ToArray();

            //foreach (var verbForm in list)
            //{
            //    if (verbForm.List == null)
            //    {
            //        continue;
            //    }

            //    WriteFile(verbForm);
            //}
        }

        private IList<string> GetExistingVerbs()
        {
            //return Directory.GetFiles(Program.VerbFormFolder).Select(f => f.Replace(Program.VerbFormFolder + "\\", "").Replace(".xml", "").DeNormalize()).ToArray();
            return new List<string>();
        }

        private void WriteFile(DeVerbForms verbForm)
        {
            string fileName = string.IsNullOrEmpty(verbForm.FileSuffix) ?
                $"{Constants.VerbFormFolder}\\{verbForm.Value.Normalizee()}.xml" :
                $"{Constants.VerbFormFolder}\\{verbForm.Value.Normalizee()}_{verbForm.FileSuffix}.xml" ;

            //if (File.Exists(fileName))
            //{
            //    return;
            //}

            XmlTextWriter textWriter = new XmlTextWriter(fileName, null);
            textWriter.WriteStartDocument();

            textWriter.WriteStartElement("VerbForms");

            WriteElement(textWriter, "ru", verbForm.Translation);

            foreach (var item in verbForm.List)
            {
                textWriter.WriteStartElement("VerbForm");

                WriteElement(textWriter, "Type", item.Form.ToString());
                WriteElement(textWriter, "_1S", item.Single1);
                WriteElement(textWriter, "_2S", item.Single2);
                WriteElement(textWriter, "_3S", item.Single3);
                WriteElement(textWriter, "_1P", item.Plural1);
                WriteElement(textWriter, "_2P", item.Plural2);
                WriteElement(textWriter, "_3P", item.Plural3);

                textWriter.WriteEndElement();
            }

            textWriter.WriteEndElement();

            textWriter.WriteEndDocument();
            textWriter.Close();
        }

        private static void WriteElement(XmlTextWriter textWriter, string tag, string value)
        {
            textWriter.WriteStartElement(tag);
            textWriter.WriteString(value);
            textWriter.WriteEndElement();
        }

        private IList<DeVerbForms> Parse(string de)
        {
            Console.WriteLine(de);

            var hostUrl = "https://de.pons.com/verbtabellen/deutsch/";

            var document = GetHtml(hostUrl + de);

            if (document == null && de.Contains("ß"))
            {
                de = de.Replace("ß", "ss");
                document = GetHtml(hostUrl + de);
            }

            var trNodes = GetVerbFormsContainer(document);
            if (trNodes == null || trNodes.Count == 0)
            {
                Console.WriteLine($"Not found {de}");
                return new List<DeVerbForms> ();
            }

            var factory = new PonsDeTranslationContainerFactory(de, _type);
            var words = factory.GetWords( "/\r\n").OfType<Verb>().ToArray();
            //if (words.Length == 0)
            //{
            //    return new List<DeVerbForms>();
            //}


            var (level, sound) = de.GetLevelAndSound(_type.ToString().ToLower());
            factory.UploadSound(sound);

            var conjugations = GetConjugations(document);
            if (conjugations.Count == 0)
            {
                VerbType verbType = VerbType.All;
                if (words.Length != 0)
                {
                    verbType = GetVerbType(words[0]?.VerbClass);
                }

                return new List<DeVerbForms> { GetDeVerbForm(de, trNodes, words, level, verbType) };
            }

            var list = new List<DeVerbForms>();

            int index = 0;
            foreach (var conjugation in conjugations)
            {
                if (IsIgnored(conjugation.Info))
                {
                    continue;
                }
                string url = $"{hostUrl}{de}?instance={conjugation.Id}";
                document = GetHtml(url);
                trNodes = GetVerbFormsContainer(document);
                var (fileSuffix, verbType) = GetFileSuffix(conjugation.Info, index++);
                var verbForm = GetDeVerbForm(de, trNodes, words, level, verbType, fileSuffix);

                list.Add(verbForm);
            }

            return list;

        }

        private (string, VerbType) GetFileSuffix(string conjugationInfo, int index)
        {
            conjugationInfo = conjugationInfo.ToLower();
            switch (conjugationInfo)
            {
                case "konjugation mit haben":
                    return ($"{index}_hb", VerbType.Trans);
                case "konjugation mit sein":
                    return ($"{index}_sn", VerbType.Trans);
                case "reflexiv, akkusativpronomen, konjugation mit haben":
                    return ($"{index}_refl_akk_hb", VerbType.Refl);
                case "reflexiv, dativpronomen, konjugation mit haben":
                    return ($"{index}_refl_dat_hb", VerbType.Refl);
                default:
                    throw new Exception("Not defined conjugationInfo");
            }
        }

        private DeVerbForms GetDeVerbForm(string de, IList<HtmlNode> trNodes, IList<Verb> words, string level, VerbType verbType,  string fileSuffix = null)
        {
            var item = new DeVerbForms
            {
                Value = de,
                FileSuffix = fileSuffix
            };

            var list = new List<DeVerbForm>();

            foreach (var trNode in trNodes)
            {
                var verbFormType = trNode.SelectSingleNode(".//caption")?.InnerText.RemoveNewLine().Trim();

                if (string.IsNullOrEmpty(verbFormType))
                {
                    var imperative = trNode.SelectSingleNode(".//span[@data-pons-flection-id='IMPERATIV_2S']")?.InnerText
                        .RemoveNewLine().Trim();
                    if (!string.IsNullOrEmpty(imperative))
                    {
                        list.Add(new ImperativeVerbFormParser().Parse(trNode));
                    }
                }
                else
                {
                    var parser = GetParser(verbFormType);
                    var deVerbForm = parser.Parse(trNode);
                    if (deVerbForm != null)
                    {
                        list.Add(deVerbForm);
                    }
                }
            }

            item.List = list;

            if (words.Count != 0)
            {
                var ru = (verbType == VerbType.Refl ? words.Where(IsReflexive) : words.Where(w => !IsReflexive(w))).Select(w => $"{w.GetInfo()}: {w.Ru}\n").ToArray();
                item.Translation = (string.IsNullOrEmpty(level) ? "" : $"{level} ") + (string.Join("", ru)).Trim();
            }

            

            return item;
        }

        private IVerbFormParser GetParser(string verbFormType)
        {
            switch (verbFormType)
            {
                case "Präsens":
                    return new PrasensVerbFormParser();
                case "Präteritum":
                    return new PrateritumVerbFormParser();
                case "Perfekt":
                    return new PerfektVerbFormParser();
                case "Plusquamperfekt":
                    return new PlusquamperfektVerbFormParser();
                case "Futur I":
                    return new FuturIVerbFormParser();
                case "Futur II":
                    return new FuturIIVerbFormParser();
                case "Konjunktiv I - Präsens":
                    return new KonjunktivIPrasensVerbFormParser();
                case "Konjunktiv I - Perfekt":
                    return new KonjunktivIPerfektVerbFormParser();
                case "Konjunktiv I - Futur I":
                    return new KonjunktivIFuturIVerbFormParser();
                case "Konjunktiv I - Futur II":
                    return new KonjunktivIFuturIIVerbFormParser();
                case "Konjunktiv II - Präteritum":
                    return new KonjunktivIIPrateritumVerbFormParser();
                case "Konj. II - Plusquamperfekt":
                case "Konjunktiv II - Plusquamperfekt":
                    return new KonjunktivIIPlusquamperfektVerbFormParser();
                case "Konjunktiv II - Futur I":
                    return new KonjunktivIIFuturIVerbFormParser();
                case "Konjunktiv II - Futur II":
                    return new KonjunktivIIFuturIIVerbFormParser();
            }

            return new EmptyVerbFormParser();
        }

        private bool IsIgnored(string conjugationInfo)
        {
            return string.Equals(conjugationInfo, "Unpersönliches Verb", StringComparison.InvariantCultureIgnoreCase)
            || string.Equals(conjugationInfo, "Unpersönliches Verb, Konjugation mit sein", StringComparison.InvariantCultureIgnoreCase);
        }

        private bool IsReflexive(WordClass verb)
        {
            return ((Verb)verb).VerbClass?.Contains("refl") ?? false;
        }

        private IList<HtmlNode> GetVerbFormsContainer(HtmlDocument document)
        {
            var r =  document?.DocumentNode.SelectNodes(".//div[@class='flex min-w-xs flex-1 flex-col bg-white p-2 xl:max-w-1/2']");
            return document?.DocumentNode.SelectNodes(".//table[contains(@class, 'conjugation-table')]");
        }

        private IList<Conjugation> GetConjugations(HtmlDocument document)
        {
            var items = document?.DocumentNode.SelectNodes(".//form[@id='conjugation-table-navigation-form']//label");
            if (items == null || items.Count == 0)
            {
                return new List<Conjugation>();
            }

            return items.Select(i => new Conjugation(i)).ToArray();
        }

        private VerbType GetVerbType(string verbClass)
        {
            switch ((verbClass ?? "").ToLower())
            {
                case "refl":
                    return VerbType.Refl;
                case "trans":
                    return VerbType.Trans;
                case "intr":
                    return VerbType.Intr;
                case "":
                case "trans, intr":
                case "intr, trans":
                    return VerbType.All;
                default:
                    throw new ArgumentException("Wrong verb class");
            }
        }
    }
}