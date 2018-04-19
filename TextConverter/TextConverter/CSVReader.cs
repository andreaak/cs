using System.Collections.Generic;
using System.IO;
using TextConverter.Models;
using System.Linq;

namespace TextConverter
{
    class CSVReader
    {
        public static readonly string[] Items = { "ru", "en", "en_tr", "de" };

        public static IList<WordItem> ReadWords(string path)
        {
            var lines = File.ReadAllLines(path);
            IList<WordItem> words = new List<WordItem>();

            foreach (string line in lines)
            {
                WordItem word = new WordItem();
                var items = line.Split(',', ';');
                for (int i = 0; i < items.Length; i++)
                {
                    word.AddItem(Items[i], items[i]);
                }

                words.Add(word);
            }

            return words;
        }

        public static IList<EnVerbItem> ReadVerbs(string path)
        {
            var lines = File.ReadAllLines(path);
            IList<EnVerbItem> words = new List<EnVerbItem>();

            foreach (string line in lines)
            {
                var items = line.Split(new char[] { ',', ';' }, System.StringSplitOptions.RemoveEmptyEntries);
                EnVerbItem word = new EnVerbItem
                {
                    Infinitive = items.ElementAtOrDefault(0),
                    InfinitiveTranscription = items.ElementAtOrDefault(2),
                    PastSimple = items.ElementAtOrDefault(3),
                    PastSimpleTranscription = items.ElementAtOrDefault(4),
                    PastPaticiple = items.ElementAtOrDefault(5),
                    PastPaticipleTranscription = items.ElementAtOrDefault(6),
                    Translation = items.ElementAtOrDefault(7),
                };
                words.Add(word);
            }

            return words;
        }
    }
}
