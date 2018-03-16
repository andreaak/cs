using System.Collections.Generic;
using System.IO;

namespace TextConverter
{
    class CSVReader
    {
        public static readonly string[] Items = { "ru", "en", "en_tr", "de" };

        public static IList<WordItem> Read(string path)
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
    }
}
