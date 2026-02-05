using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeRuVerbFilesParser : LanguageParser, ILanguageParser
    {
        public TranslateDeRuVerbFilesParser()
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
                var items = selector(file);
                if (items.Select(i => i.De).Distinct().Count() <= 1)
                {
                    Console.WriteLine(items.First().De);
                }
                else
                {
                    words.AddRange(items);
                }
            }

            return words;
        }
    }
}