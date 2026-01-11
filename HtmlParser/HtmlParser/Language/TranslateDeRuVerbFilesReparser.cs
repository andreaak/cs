using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeRuVerbFilesReparser : LanguageParser, ILanguageParser
    {
        private static string[] prefixes =
        {
            "ab", "an", "auf", "aus", "bei", "ein", "fest", "her", "hin", "los", "mit", "nach", "teil", "vor", "weg", "wieder",
            "zu","zurück","zusammen",
            "über","unter","durch","um","wieder","weiter",
            "be","emp","ent","er","ge","miss","ver","zer",
            "auseinander", "dran", "entgegen", "fort", "gefangen", "heim", "heran", "heraus", "herein", "herunter", "hierher",
            "hinauf", "hinaus", "hinein", "hinüber", "hinunter", "hinzu", "hoch", "krumm", "leicht", "mal", "ran", "raus",
            "rein", "ruber", "schwer", "streng", "übel", "voraus", "vorlieb", "vorweg", "wahr", "wiederauf", "wunder",
            "bevor", "statt"
        };

        private static string[] selectPrefixes =
        {
            "ab", "an", "auf", "aus", "bei", "ein", "fest", "her", "hin", "los", "mit", "nach", "teil", "vor", "weg", "wieder",
            "zu","zurück","zusammen",
            "über","unter","durch","um","wieder","weiter",
            "be","emp","ent","er","ge","miss","ver","zer",
            "auseinander", "dran", "entgegen", "fort", "gefangen", "heim", "heran", "heraus", "herein", "herunter", "hierher",
            "hinauf", "hinaus", "hinein", "hinüber", "hinunter", "hinzu", "hoch", "krumm", "leicht", "mal", "ran", "raus",
            "rein", "ruber", "schwer", "streng", "übel", "voraus", "vorlieb", "vorweg", "wahr", "wiederauf", "wunder",
            "bevor", "statt"
        };

        public TranslateDeRuVerbFilesReparser()
            : base(false, WordType.Verb)
        { }

        public void Parse(IList<string> lines)
        {
            lines = GetExistingVerbs(FileExtensions.ParseFirstEntity).Select(i => i.De).ToArray();

            using (var sw = File.CreateText("notFound.txt"))
            {
                var temp = lines.Where(l => !string.IsNullOrWhiteSpace(l.Trim()))
                    .Select(l => l.Replace("|", ""))
                    .Distinct();


                temp = _order ?
                    temp.OrderBy(l => l).ToArray() :
                    temp.ToArray();


                foreach (var verb in temp)
                {
                    var verbs = Parse(verb.Trim(), sw);
                    verb.SaveToFile(verbs);
                }
            }
        }


        private IList<WordClass> GetExistingVerbs(Func<string, IEnumerable<WordClass>> selector)
        {
            var files = Directory.GetFiles(Constants.VerbFolder).OrderBy(FileExtensions.DeNormalize).ToArray();

            var words = new List<WordClass>();

            foreach (var file in files)
            {
                var verbs = selector(file).ToArray();
                words.AddRange(verbs);
            }

            return words;
        }

        private IList<Verb> Parse(string line, StreamWriter notFoundSw)
        {
            var deBase = line.Trim();
            Console.WriteLine(deBase);

            var list = new List<Verb>();

            var baseWords = GetWords(notFoundSw, deBase).Select(w => (Verb)w).ToArray();

            deBase.SetLevel(baseWords);
            deBase.SetExample(baseWords);


            foreach (var prefix in selectPrefixes)
            {
                var de = prefix + deBase;

                var words = GetWords(notFoundSw, de).Select(w => (Verb)w).ToArray();

                de.SetLevel(words);
                de.SetExample(words);

                list.AddRange(words);
            }

            var ordered = list.OrderBy(w => w.De).ToList();
            ordered.InsertRange(0, baseWords);
            return ordered;
        }

        private IList<WordClass> GetWords(StreamWriter notFoundSw, string de)
        {
            Console.WriteLine(de);

            var factory = new PonsDeTranslationContainerFactory(de, _type);
            var words = factory.GetWords();
            factory.UploadSound();

            return words;
        }
    }
}