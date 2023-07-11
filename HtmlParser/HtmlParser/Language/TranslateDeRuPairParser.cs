using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeRuPairParser : LanguageParser, ILanguageParser
    {
        public TranslateDeRuPairParser(bool order, string type)
            : base(order, type)
        { }

        public void Parse(IList<string> lines)
        {
            var list = _order ?
                lines.Where(l => !string.IsNullOrEmpty(l))
                    .Select(l => Parse(l.Trim()))
                    .OrderBy(l => l.De)
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

        protected WordClass Parse(string line)
        {
            Console.WriteLine(line);

            var items = line.Split(new [] {"\t"}, StringSplitOptions.RemoveEmptyEntries);

            string ru = items[0];
            string de = items[1];

            return GetWord(ru, de);
        }

        private WordClass GetWord(string ru, string de)
        {
            var hostUrl = "https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/";

            var document = GetHtml(hostUrl + de);

            var trNode = GetTranslationNode(document, de);
            if (trNode == null)
            {
                Console.WriteLine($"Not found {de}");
                return new WordClass
                {
                    De = de,
                    Ru = ru
                };
            }

            var word = GetWord(trNode, de);
            word.Ru = ru;
            UploadSound(trNode.Node, word.De);

            return word;
        }
    }
}