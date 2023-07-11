using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    class TranscriptionResponse
    {
        public string Text { get; set; }
    }

    public class TranslateRuDeParser : LanguageParser, ILanguageParser
    {
        public TranslateRuDeParser(bool order, string type)
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

        protected WordClass Parse(string ru)
        {
            Console.WriteLine(ru);

            var hostUrl = "https://de.pons.com/%C3%BCbersetzung/russisch-deutsch/";

            var document = GetHtml(hostUrl + ru);

            var deNode = document.DocumentNode.SelectSingleNode(".//div[@class='translations first']//div[@class='target']//a");
            if (deNode == null)
            {
                Console.WriteLine($"Not found {ru}");
                return new WordClass
                {
                    Ru = ru
                };
            }

            var de = deNode.InnerText.Trim();

            hostUrl = "https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/";
            document = GetHtml(hostUrl + de);
            
            var translationNode = GetTranslationNode(document, de);
            if (translationNode == null)
            {
                Console.WriteLine($"Not found {de}");
                return new WordClass
                {
                    De = de,
                    Ru = ru
                };
            }

            var word = GetWord(translationNode, de);
            UploadSound(translationNode.Node, word.De);

            return word;
        }
    }
}