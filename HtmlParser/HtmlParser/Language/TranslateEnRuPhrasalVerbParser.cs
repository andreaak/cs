using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;
using HtmlParser.Language.VerbFormParsers;

namespace HtmlParser.Language
{
    public class TranslateEnRuPhrasalVerbParser : LanguageParser, ILanguageParser
    {
        private static string[] selectPrefixes =
        {
            "about", "across", "after", "ahead", "apart", "around", "aside", "away",
            "back","back on","by","down",
            "for", "forward", "forward to", "in", "into",
            "off","on","off","out","over",
            "round", 
            "through","together","to",
            "up","up to","up with","without",
        };

        public TranslateEnRuPhrasalVerbParser(Parameters parameters)
            : base(parameters.Order, parameters.WordType)
        { }

        public void Parse(IList<string> lines)
        {
            var temp = lines.Where(l => !string.IsNullOrEmpty(l)).Distinct();
            temp = _order ?
                temp.OrderBy(l => l):
                temp;


            var list = temp.SelectMany(l => Parse(l.Trim()));


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

            //var factory = new PonsEnTranslationContainerFactory(de, WordType.Verb);
            //var words = factory.GetWords().ToList();

            //de.SetEnLevel(words);

            var prefixWords = new List<WordClass>();
            
            foreach (var prefix in selectPrefixes)
            {
                var verb = $"{de} {prefix}";
                Console.WriteLine(verb);
                var factory = new PonsEnTranslationContainerFactory(verb, WordType.Verb);
                var wrds = factory.GetWords();

                if (wrds.Count > 0 && wrds.All(w => w.Found))
                {
                    verb.SetEnLevel(wrds);
                    prefixWords.AddRange(wrds);
                }
            }
            return prefixWords;//.Count > 0 ? (IList<WordClass>)words.Concat(prefixWords).ToArray() : new List<WordClass>();
        }
    }
}