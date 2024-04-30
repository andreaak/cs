using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeRuListParser : LanguageParser, ILanguageParser
    {
        public TranslateDeRuListParser(bool order)
            : base(order, WordType.Other)
        { }

        public void Parse(IList<string> lines)
        {
            
            using (var sw = File.CreateText("notFound.txt"))
            {
                var temp = lines.Where(l => !string.IsNullOrEmpty(l))
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
            Console.WriteLine(item.De);

            item.De = item.De.Replace("_", " ");

            if (item.Type == WordType.Verb)
            {
                item.De = item.De.Replace("|", "");
            }

            var hostUrl = "https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/";

            var document = GetHtml(hostUrl + item.De);

            var trNode = GetTranslationContainer(document, item.De, item.Type);
            if (trNode == null || trNode.Count == 0)
            {
                Console.WriteLine($"Not found {item.De}");
                sw.WriteLine(item.De);
                return new List<WordClass>()
                {
                    new WordClass
                    {
                        De = item.De,
                    }
                };
            }

            var words = GetWords(trNode, item.De);

            if (words.Any(w => string.IsNullOrEmpty(w.Ru)))
            {
                sw.WriteLine(item.De);
            }

            if (trNode.First().Node != null)
            {
                UploadSound(trNode.First().Node, words.First().De.Replace("|", ""));
            }
            

            return words;
        }

        private ListItem GetItem(string de)
        {
            var items = de.Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var value = items[0];
            var listItem = new ListItem
            {
                De = value,
                Type = WordType.All
            };


            if (char.IsUpper(value[0]))
            {
                listItem.De = value.TrimEnd(',').Replace("/in", "(in)");
                listItem.Type = WordType.Subst;
            }
            else if (items.Any(i => i.ToLower() == "verb"))
            {
                listItem.Type = WordType.Verb;
            }
            return listItem;
        }
    }

    public class ListItem
    {
        public string De { get; set; }
        public WordType Type { get; set; }
    }
}