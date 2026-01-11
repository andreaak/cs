using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeRuVerbFilesSetLevel : LanguageParser, ILanguageParser
    {
        public TranslateDeRuVerbFilesSetLevel()
            : base(false, WordType.Verb)
        { }

        public void Parse(IList<string> lines)
        {
            var list = GetExistingVerbs(FileExtensions.ParseFile);

            using (var sw2 = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    item.Write(sw2);
                }
            }
        }

        private IList<WordClass> GetExistingVerbs(Func<string, IList<Verb>> selector)
        {
            var files = Directory.GetFiles(Constants.VerbFolder).OrderBy(FileExtensions.DeNormalize).ToArray();

            var words = new List<WordClass>();

            foreach (var file in files)
            {
                var verbs = selector(file);
                if (verbs.Select(i => i.De).Distinct().Count() <= 1)
                {
                    Console.WriteLine($"----------------------------- {verbs.First().De}");
                }
                else
                {
                    bool save = false;

                    foreach (var w in verbs)
                    {
                        if (string.IsNullOrEmpty(w.Level))
                        {
                            Console.WriteLine($"Set level {w.De}");
                            var (level, sound) = w.De.GetLevelAndSound(_type.ToString().ToLower());
                            if (!string.IsNullOrEmpty(level))
                            {
                                w.Level = level;
                                save = true;
                            }
                        }
                    }

                    if (save)
                    {
                        var verb = verbs.First();
                        verb.De.SaveToFile(verbs);
                    }
                }
            }

            return words;
        }
    }
}