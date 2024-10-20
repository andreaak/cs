using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeRuParser : LanguageParser, ILanguageParser
    {
        public TranslateDeRuParser(bool order, WordType type)
            : base(order, type)
        { }

        public void Parse(IList<string> lines)
        {
            var temp = lines.Where(l => !string.IsNullOrEmpty(l))
                .SelectMany(l => Parse(l.Trim()));

            var list = _order ?
                temp.OrderBy(l => l.De).ToArray() :
                temp.ToArray();

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    item.Write(sw);
                }
            }
        }

        private IList<WordClass> Parse(string de)
        {
            Console.WriteLine(de);

            if (_type == WordType.Verb)
            {
                de = de.Replace("|", "");
            }

            var hostUrl = "https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/";

            var document = GetHtml(hostUrl + de);

            var factory = new PonsTranslationContainerFactory();

            var trNode = factory.GetTranslationContainer(document, de, _type);
            
            if (trNode == null || trNode.Count == 0)
            {
                //var hostUrl2 = "https://www.translate.ru/перевод/немецкий-русский/";

                Console.WriteLine($"Not found {de}");
                return new List<WordClass>()
                {
                    new WordClass
                    {
                        De = de
                    }
                };
            }

            var word = GetWords(trNode, de);
            UploadSound(trNode.First().Node, word.First().De.Replace("|", ""));

            return word;
        }
    }
}