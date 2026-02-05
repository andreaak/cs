using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateRuDePairParser : LanguageParser, ILanguageParser
    {
        public TranslateRuDePairParser(bool order, WordType type)
            : base(order, type)
        { }

        public void Parse(IList<string> lines)
        {
            var list = _order ?
                lines.Where(l => !string.IsNullOrEmpty(l))
                    .Select(l => Parse(l.Trim()))
                    .OrderBy(l => l.De)
                    .ToArray()
                :
                lines.Where(l => !string.IsNullOrEmpty(l))
                    .Select(l => Parse(l.Trim()))
                    .ToArray();

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    item.Write(sw);
                }
            }
        }

        protected WordClass Parse(string line)
        {
            Console.WriteLine(line);

            var items = line.Split(new [] {"\t"}, StringSplitOptions.RemoveEmptyEntries);

            string ru = items[0];
            string de = items[1];

            return GetWords(ru, de);
        }

        private WordClass GetWords(string ru, string de)
        {
            var words = de.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 1)
            {
                return GetWord(ru, de);
            }

            var wordOut = new Verb()
            {
                Ru = ru,
                De = de
            };

            var allWords = new List<WordClass>();

            foreach (var word in words)
            {
                var normWord = word.Trim().Trim(new[] { '(', ')' });


                var factory = new PonsDeTranslationContainerFactory(normWord, _type);
                var words2 = factory.GetWords();
                factory.UploadSound();
                if (words2.Count != 0)
                {
                    var word2 = words2.First();
                    allWords.Add(word2);
                }
            }

            wordOut.DeTranscription = string.Join(" ", allWords.Select(w => w.DeTranscription ?? "-"));
            wordOut.Info = string.Join("; ",
                allWords.Where(w => w.GetType() == typeof(Verb)).OfType<Verb>().Select(w => w.Info));
            return wordOut;
        }

        private WordClass GetWord(string ru, string de)
        {
            var factory = new PonsDeTranslationContainerFactory(de, _type);
            var words = factory.GetWords();
            factory.UploadSound();
            var word = words.First();

            return word;
        }
    }
}