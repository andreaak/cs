using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HtmlParser.Language.Model;
using HtmlParser.Language.VerbFormParsers;

namespace HtmlParser.Language
{
    public class DeVerbFormParser : LanguageParser, ILanguageParser
    {
        private const string Folder = "D:\\Temp\\Deutch\\DeutschAndroid\\VerbForms";

        public DeVerbFormParser()
            : base(false, WordType.Verb)
        { }

        public void Parse(IList<string> lines)
        {
            var list = lines.Where(l => !string.IsNullOrEmpty(l))
                    .Select(l => Parse(l.Trim()))
                    .ToArray();

            foreach (var verbForm in list)
            {
                if (verbForm.List == null)
                {
                    continue;
                }

                WriteFile(verbForm);
            }
        }

        private void WriteFile(DeVerbForms verbForm)
        {
            XmlTextWriter textWriter = new XmlTextWriter($"{Folder}\\{Normalize(verbForm.Value)}.xml", null);
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

        private DeVerbForms Parse(string de)
        {
            Console.WriteLine(de);

            de = de.Replace("|", "");

            var hostUrl = "https://de.pons.com/verbtabellen/deutsch/";

            var document = GetHtml(hostUrl + de);

            if (document == null && de.Contains("ß"))
            {
                de = de.Replace("ß", "ss");
                document = GetHtml(hostUrl + de);
            }

            var item = new DeVerbForms
            {
                Value = de
            };

            var trNodes = GetVerbFormsContainer(document);
            if (trNodes == null || trNodes.Count == 0)
            {
                Console.WriteLine($"Not found {de}");
                return item;
            }

            var list = new List<DeVerbForm>();

            foreach (var trNode in trNodes)
            {
                var verbFormType = trNode.SelectSingleNode(".//h3")?.InnerText.RemoveNewLine().Trim();

                if (string.IsNullOrEmpty(verbFormType))
                {
                    var imperative = trNode.SelectSingleNode(".//span[@data-pons-flection-id='IMPERATIV_2S']")?.InnerText.RemoveNewLine().Trim();
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

            hostUrl = "https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/";

            document = GetHtml(hostUrl + de);

            var trContainer = GetTranslationContainer(document, de);
            if (trContainer == null || trContainer.Count == 0)
            {
                return item;
            }

            var word = GetWords(trContainer, de);
            item.Translation = string.Join("/", word.Select(w => w.Ru));

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
    }
}