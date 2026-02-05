using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateEnRuVocabularyParser : LanguageParser, ILanguageParser
    {
        public TranslateEnRuVocabularyParser(Parameters parameters)
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

            var en = item.En;
            if (item.En.Contains("("))
            {
                en = item.En.Substring(0, item.En.IndexOf("(")).Trim();
            }


            var factory = new PonsEnTranslationContainerFactory(en, item.Type);
            var words = factory.GetWords();

            foreach (var word in words)
            {
                word.De = item.En;
                if (string.IsNullOrEmpty(word.DeTranscription))
                {
                    word.DeTranscription = item.EnTr;
                }

                if (string.IsNullOrEmpty(word.Ru))
                {
                    word.Ru = item.Ru;
                }
                else if (word.Ru.IsOther(item.Ru))
                {
                    word.Ru += $"(---): {word.Ru.AnotherTranslation(item.Ru)}-!-";
                }

                en.SetEnLevel(words);
            }
            
            if (!words[0].Found)
            {
                words[0].De = item.En;
                words[0].DeTranscription = item.EnTr;
                words[0].Ru = item.Ru;
            }

            factory.UploadSound(null);

            return words;
        }



        private EnListItem GetItem(string de)
        {
            de = de.Trim();

            if (!de.Contains("  "))
            {
                return new EnListItem
                {
                    En = de,
                    Ru = de,
                    Type = _type
                };
            }

            var items = de.Split(new []{ "  " } , StringSplitOptions.RemoveEmptyEntries);

            var listItem = new EnListItem
            {
                En = items[0].Replace("\t", " ").Replace("  ", " "),
                EnTr = items[1].Replace("\t", " ").Replace("  ", " "),
                Ru = string.Join(" ", items.Skip(2)).Replace("\t", " ").Replace("  ", " "),
                Type = _type
            };

            return listItem;
        }
    }

    public class EnListItem
    {
        public string En { get; set; }
        public string EnTr { get; set; }
        public string Ru { get; set; }
        public WordType Type { get; set; }
    }
}