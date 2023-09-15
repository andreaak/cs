using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeRuVerbParser : LanguageParser, ILanguageParser
    {
        public TranslateDeRuVerbParser(bool order, string type)
            : base(order, type)
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

        protected IrregularDeVerb Parse(string de)
        {
            Console.WriteLine(de);

            var items = de.Split(new [] {"\t"}, StringSplitOptions.RemoveEmptyEntries);

            string infinitive = items[0];
            string present = items[1];
            string prateritum = items[2];
            string part2 = items[3];
            string ru = items[4];

            return new IrregularDeVerb
            {
                Infinitive = GetVerbFormClass(infinitive),
                Present = GetVerbFormClass(present),
                Prateritum = GetVerbFormClass(prateritum),
                Part2 = GetPartClass(part2),
                Ru = ru
            };
        }

        private WordClass GetVerbFormClass(string de)
        {
            var trContainer = GetTranslationNode(de);
            if (trContainer == null)
            {
                Console.WriteLine($"Not found {de}");
                return new WordClass
                {
                    De = de
                };
            }

            var trItem = trContainer.Node ?? trContainer.AllNodes.First();
            var word = GetVerb(trItem, de, trContainer.AllNodes);
            
            UploadSound(trItem, word.De);

            return word;
        }

        private IrrVerb GetPartClass(string line)
        {
            var items = line.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
            var de = items[1];

            var trContainer = GetTranslationNode(de);
            if (trContainer == null)
            {
                Console.WriteLine($"Not found {de}");
                return new IrrVerb
                {
                    De = de,
                    Part2Verb = items[0]
                };
            }

            var trItem = trContainer.Node ?? trContainer.AllNodes.First();
            var word = GetIrregularVerb(trItem, items[1], items[0]);
            
            UploadSound(trItem, word.De);

            return word;
        }

        private TranslationContainer GetTranslationNode(string de)
        {
            var hostUrl = "https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/";
            var document = GetHtml(hostUrl + de);
            var trNode = GetTranslationContainer(document, de);
            return trNode;
        }
    }
}