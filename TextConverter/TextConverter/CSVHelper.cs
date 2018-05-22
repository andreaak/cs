using System.Collections.Generic;
using System.IO;
using TextConverter.Models;
using System.Linq;

namespace TextConverter
{
    class CSVHelper
    {
        public const string Us = "us";
        public const string Uk = "uk";

        public const string Ru = "ru";
        public const string En = "en";
        public const string EnTr = "en_tr";
        public const string De = "de";
        public static readonly string[] Items = { Ru, En, EnTr, De };

        public static IList<WordItem> ReadWords(string path)
        {
            var lines = File.ReadAllLines(path);
            IList<WordItem> words = new List<WordItem>();

            foreach (string line in lines)
            {
                WordItem word = new WordItem();
                var items = line.Split(',', ';');
                for (int i = 0; i < Items.Length && i < items.Length; i++)
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
                var items = line.Split(new char[] { ',', ';' });
                EnVerbItem word = new EnVerbItem
                {
                    Infinitive = items.ElementAtOrDefault(0)?.Trim(),
                    InfinitiveTranscription = items.ElementAtOrDefault(1)?.Trim(),
                    PastSimple = items.ElementAtOrDefault(2)?.Trim(),
                    PastSimpleTranscription = items.ElementAtOrDefault(3)?.Trim(),
                    PastPaticiple = items.ElementAtOrDefault(4)?.Trim(),
                    PastPaticipleTranscription = items.ElementAtOrDefault(5)?.Trim(),
                    Translation = items.ElementAtOrDefault(6)?.Trim(),
                };
                words.Add(word);
            }

            return words;
        }

        public static void WriteVerbs(string path, IList<EnVerbItem> items)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            string separator = ",";
            foreach (var item in items)
            {

                string output = item.Infinitive.Trim().ToLowerInvariant() + separator
                    + item.InfinitiveTranscription + separator
                    + item.PastSimple.Trim().ToLowerInvariant() + separator
                    + item.PastSimpleTranscription + separator
                    + item.PastPaticiple.Trim().ToLowerInvariant() + separator
                    + item.PastPaticipleTranscription + separator
                    + item.Translation.Trim().ToLowerInvariant() + "\n";
                File.AppendAllText(path, output);
            }
        }

        public static void WriteWords(string path, IList<WordItem> items)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            string separator = ",";
            foreach (var item in items)
            {

                string output = "";

                foreach (var language in Items)
                {
                    string value = item.GetItem(language).Trim().ToLowerInvariant();
                    output += (value + separator);
                }
                output += "\n";
                File.AppendAllText(path, output);
            }
        }
    }
}
