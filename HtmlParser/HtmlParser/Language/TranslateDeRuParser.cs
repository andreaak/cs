using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeRuParser : LanguageParser, ILanguageParser
    {
        public TranslateDeRuParser(bool order, string type)
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

        private WordClass Parse(string de)
        {
            Console.WriteLine(de);
            
            var hostUrl = "https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/";

            var document = GetHtml(hostUrl + de);

            var trNode = GetTranslationContainer(document, de);
            if (trNode == null)
            {
                Console.WriteLine($"Not found {de}");
                return new WordClass
                {
                    De = de
                };
            }

            var word = GetWord(trNode, de);
            UploadSound(trNode.Node, word.De);

            return word;
        }
    }
}