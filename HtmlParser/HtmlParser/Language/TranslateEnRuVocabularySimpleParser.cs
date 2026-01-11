using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateEnRuVocabularySimpleParser : LanguageParser, ILanguageParser
    {
        public TranslateEnRuVocabularySimpleParser(Parameters parameters)
            : base(parameters.Order, parameters.WordType)
        { }

        public void Parse(IList<string> lines)
        {
            
            using (var sw = File.CreateText("notFound.txt"))
            {
                var temp = lines.Where(l => !string.IsNullOrWhiteSpace(l))
                    .SelectMany(l => Parse(l.Trim(), sw));


                var list = _order ?
                    temp.OrderBy(l => l.De).ToArray() :
                    temp.ToArray();
                
                using (var sw2 = File.CreateText("out.txt"))
                {
                    foreach (var item in list)
                    {
                        item.Write(sw2);
                    }
                }
            }
        }

        private IList<WordClass> Parse(string line, StreamWriter sw)
        {
            var item = GetItem(line);
            Console.WriteLine(item.En);

            return new List<WordClass>()
            {
                new WordClass
                {
                    De = item.En,
                    DeTranscription = item.EnTr,
                    Ru = item.Ru
                }
            };
        }

        private EnListItem GetItem(string de)
        {
            de = de.Trim();

            var items = de.Split(new []{ "  " } , StringSplitOptions.RemoveEmptyEntries);

            var listItem = new EnListItem
            {
                En = items[0],
                EnTr = items[1],
                Ru = string.Join(" ", items.Skip(2)),
                Type = _type
            };

            return listItem;
        }
    }

}