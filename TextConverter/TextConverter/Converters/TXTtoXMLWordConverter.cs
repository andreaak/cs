using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TextConverter.Models;
using static TextConverter.DownloadHelper;
using static TextConverter.Models.WordItem;

namespace TextConverter
{
    public class TXTtoXMLWordConverter : WordConverter, IWordConverter
    {
        public string InExtension => ".txt";
        public string OutExtension => ".xml";

        public async Task ConvertFileAsync(string filePath, string outputFilePath, WordData data)
        {
            var words = File.ReadAllLines(data.Path);

            List<WordItem> wordItems = new List<WordItem>();
            for (int i = 0; i < words.Length - 1;)
            {
                string en = words[i++];
                string ru = words[i++];
                string normalized = GetNormalized(en);
                var item = new WordItem();
                item.AddItem(Ru, ru);
                item.AddItem(En, en);
                wordItems.Add(item);
            }

            await ProcessWordsAsync(wordItems, data);

            XMLHelper.WriteWords(outputFilePath, wordItems);
            CSVHelper.WriteWords(outputFilePath + ".csv", wordItems);
        }
    }
}
