using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateEnRuIrregVerbFormsParser : LanguageParser, ILanguageParser
    {
        public TranslateEnRuIrregVerbFormsParser()
            : base(false, WordType.Verb)
        { }

        public void Parse(IList<string> lines)
        {
            var list = _order ?
                lines.Where(l => !string.IsNullOrEmpty(l))
                    .Select(l => Parse(l.Trim()))
                    .OrderBy(l => l.Infinitive.De)
                    .ToArray()
                :
                lines.Where(l => !string.IsNullOrEmpty(l))
                    .Select(l => Parse(l.Trim()))
                    .ToArray();

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    item.Write(sw);
                }
            }
        }

        protected IrregularEnVerb Parse(string de)
        {
            Console.WriteLine(de);

            var items = de.Split(new [] {"\t", " "}, StringSplitOptions.RemoveEmptyEntries);

            string infinitive = items[0];
            string past = items[1];
            string part2 = items[2];

            var verb = new IrregularEnVerb
            {
                Infinitive = GetInfVerbFormClassPrev(infinitive),
                Past = GetVerbFormClassPrev(past),
                Part2 = GetPart2FormClassPrev(part2),
            };
            verb.Ru = verb.Infinitive.Ru;

            var model = verb.Infinitive.De.GetEnLevelAndSounds(WordType.Verb);
            verb.Level = model.Level;

            return verb;
        }

        private WordClass GetInfVerbFormClassPrev(string de)
        {
            if (de.Contains("/"))
            {
                var des = de.Split(new[] { "/"}, StringSplitOptions.RemoveEmptyEntries);

                var words = des.Select(GetInfVerbFormClass).ToArray();

                return new IrrVerb
                {
                    De = string.Join("/", words.Select(w => w.De)),
                    DeTranscription = string.Join("/", words.Select(w => w.DeTranscription)),
                    Ru = words[0].Ru,
                    Level = words[0].Level,
                };
            }

            return GetInfVerbFormClass(de);
        }

        private WordClass GetInfVerbFormClass(string de)
        {
            var factory = new PonsEnTranslationContainerFactory(de, WordType.Verb);
            var words = factory.GetWords();

            factory.UploadSound();
            return words.FirstOrDefault();

            //var trContainer = GetTranslationNode(de, WordType.Verb);
            //if (trContainer == null)
            //{
            //    Console.WriteLine($"Not found {de}");
            //    return new WordClass
            //    {
            //        De = de
            //    };
            //}

            //var trItem = trContainer.Node ?? trContainer.AllNodes.First();
            //var word = GetVerb(trItem, de, trContainer.AllNodes);

            //if (trContainer.Node != null)
            //{
            //    word.Ru = string.Join("/", trContainer.Node.GetTranslations(de).Distinct())
            //        .Replace(" m", "")
            //        .Replace(" f", "")
            //        .Replace(" nt", "");
            //}

            

            //return words.FirstOrDefault();
        }

        private WordClass GetVerbFormClassPrev(string de)
        {
            if (de.Contains("/"))
            {
                var des = de.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                var words = des.Select(GetVerbFormClass).ToArray();

                return new IrrVerb
                {
                    De = string.Join("/", words.Select(w => w.De)),
                    DeTranscription = string.Join("/", words.Select(w => w.DeTranscription)),
                };
            }

            return GetVerbFormClass(de);
        }

        private WordClass GetVerbFormClass(string de)
        {
            var factory = new PonsEnTranslationContainerFactory(de, WordType.Verb);
            var words = factory.GetWords();

            factory.UploadSound();
            return words.FirstOrDefault();

            //var trContainer = GetTranslationNode(de, WordType.Verb);
            //if (trContainer == null)
            //{
            //    Console.WriteLine($"Not found {de}");
            //    return new WordClass
            //    {
            //        De = de
            //    };
            //}

            //var trItem = trContainer.Node ?? trContainer.AllNodes.First();
            //var word = GetVerb(trItem, de, trContainer.AllNodes);

            //UploadEnSound(trItem, word.De);

            //return word;
        }

        private WordClass GetPart2FormClassPrev(string de)
        {
            if (de.Contains("/"))
            {
                var des = de.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                var words = des.Select(GetPart2FormClass).ToArray();

                return new IrrVerb
                {
                    De = string.Join("/", words.Select(w => w.De)),
                    DeTranscription = string.Join("/", words.Select(w => w.DeTranscription)),
                };
            }

            return GetPart2FormClass(de);
        }

        private WordClass GetPart2FormClass(string de)
        {
            var factory = new PonsEnTranslationContainerFactory(de, WordType.Adj);
            var words = factory.GetWords();

            factory.UploadSound();
            return words.FirstOrDefault();

            //var trContainer = GetTranslationNode(de, WordType.Adj);
            //if (trContainer == null)
            //{
            //    Console.WriteLine($"Not found {de}");
            //    return new WordClass
            //    {
            //        De = de
            //    };
            //}

            //var trItem = trContainer.Node ?? trContainer.AllNodes.First();
            //var word = GetVerb(trItem, de, trContainer.AllNodes);

            //UploadEnSound(trItem, word.De);

            //return word;
        }

        //private TranslationContainer GetTranslationNode(string de, WordType type)
        //{
        //    var hostUrl = "https://en.pons.com/translate/english-russian/";
        //    var document = GetHtml(hostUrl + de);
        //    var trNode = new PonsEnTranslationContainerFactory().GetTranslationContainer(document, de, type);
        //    return trNode?.FirstOrDefault();
        //}
    }
}