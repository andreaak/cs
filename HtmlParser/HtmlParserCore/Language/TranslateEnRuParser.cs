using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateEnRuParser : LanguageParser, ILanguageParser
    {
        public TranslateEnRuParser(Parameters parameters)
            : base(parameters.Order, parameters.WordType)
        { }

        public void Parse(IList<string> lines)
        {
            var temp = lines.Where(l => !string.IsNullOrEmpty(l)).Distinct()
                .SelectMany(l => Parse(l.Trim()));

            var list = _order ?
                temp.OrderBy(l => l.De):
                temp;

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    item.Write(sw);
                    sw.Flush();
                }
            }
        }

        private IList<WordClass> Parse(string input)
        {
            var i = input.Split(new[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            var de = i[0];
            //var de = input.Trim();//  i[0];

            Console.WriteLine(de);

            var factory = new PonsEnTranslationContainerFactory(de, _type);
            var words = factory.GetWords();

            de.SetEnLevel(words);
            //de.SetExample(words, "en", _type.GetExampleType());

            return words;
        }
    }
}