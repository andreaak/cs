using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeRuVerbPrefixParser : LanguageParser, ILanguageParser
    {
        private static string[] prefixes =
        {
            "ab", "an", "auf", "aus", "bei", "ein", "fest", "her", "hin", "los", "mit", "nach", "teil", "vor", "weg", "wieder",
            "zu","zurück","zusammen",
            "über","unter","durch","um","wieder",
            "be","emp","ent","er","ge","miss","ver","zer",
        };

        private static string[] selectPrefixes =
        {
            "teil"
        };

        public TranslateDeRuVerbPrefixParser()
            : base(false, WordType.Verb)
        { }

        public void Parse(IList<string> lines)
        {
            using (var sw = File.CreateText("notFound.txt"))
            {
                var temp = lines.Where(l => !string.IsNullOrWhiteSpace(l.Trim()))
                    .Select(l => l.Replace("|", ""))
                    .Distinct()
                    .OrderBy(l => l).ToArray();


                var list = temp.SelectMany(l => Parse(l.Trim(), sw)).ToArray();

                using (var sw2 = File.CreateText("out.txt"))
                {
                    foreach (var item in list)
                    {
                        item.Write(sw2);
                    }
                }
            }
        }

        private IList<WordClass> Parse(string line, StreamWriter notFoundSw)
        {
            var deBase = line.Trim();
            Console.WriteLine(deBase);
            if (IsIgnoreItem(deBase))
            {
                return new List<WordClass>();
            }

            var list = new List<WordClass>();

            var baseWords = GetWords(notFoundSw, deBase);
            if (baseWords.Count != 0)
            {
                baseWords.First().WrdClass += " $$1";
            }
            foreach (var prefix in selectPrefixes)
            {
                var de = prefix + deBase;

                var words = GetWords(notFoundSw, de);
                list.AddRange(words);
            }

            var ordered = list.OrderBy(w => w.De).ToList();
            ordered.InsertRange(0, baseWords);
            return ordered;
        }

        private IList<WordClass> GetWords(StreamWriter notFoundSw, string de)
        {
            var hostUrl = "https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/";
            var document = GetHtml(hostUrl + de);

            var trNode = new PonsTranslationContainerFactory().GetTranslationContainer(document, de, WordType.Verb);
            if (trNode == null || trNode.Count == 0)
            {
                Console.WriteLine($"Not found {de}");
                notFoundSw.WriteLine(de);
                return Array.Empty<WordClass>();
            }

            Console.WriteLine(de);

            var words = GetWordsWithoutEmpty(trNode, de);

            if (trNode.First().Node != null)
            {
                UploadSound(trNode.First().Node, de.Replace("|", ""));
            }

            return words;
        }

        private bool IsIgnoreItem(string de)
        {
            return de.Contains("|") || prefixes.Any(de.StartsWith);
        }
    }
}