using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TextConverter.Models;
using static TextConverter.DownloadHelper;
using static TextConverter.Models.WordItem;

namespace TextConverter
{
    public class TXTWithRanktoXMLWordConverter : WordConverter, IWordConverter
    {
        public string InExtension => ".txt";
        public string OutExtension => ".xml";

        public async Task ConvertFileAsync(string filePath, string outputFilePath, WordData data)
        {
            var wordsLines = File.ReadAllLines(filePath);

            List<WordItem> wordItems = new List<WordItem>();
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                for (int i = 0; i < wordsLines.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(wordsLines[i]))
                    {
                        continue;
                    }
                    var word = wordsLines[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    string normalized = GetNormalized(word[0]);

                    data.InfoAction?.Invoke(normalized);

                    string html = await DownloadHtmlAsync(web, normalized, data.ErrorAction);
                    string tr = ParseTranscription(html, normalized, data.Region);
                    string ru = ParseRu(html, normalized);

                    var item = new WordItem();
                    if (word.Length > 1)
                    {
                        item.Rank = int.Parse(word[1]);
                    }
                    else
                    {
                        item.Rank = ParseRank(html);
                    }
                    if (data.DownloadTranslation)
                    {
                        item.AddItem(Ru, ru);
                    }
                    item.AddItem(En, normalized);
                    if (data.DownloadTranscription && !string.IsNullOrEmpty(tr))
                    {
                        item.AddItem(EnTr, tr);
                    }
                    wordItems.Add(item);
                }
            }

            if (data.Separate)
            {
                for (int i = 0; i < 26; i++)
                {
                    char ch = (char)((int)'a' + i);
                    var words = wordItems.Where(item => item.GetItem(En).StartsWith(ch.ToString())).ToArray();
                    string path = Path.GetDirectoryName(outputFilePath) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(outputFilePath) + "_" + ch + ".xml";
                    XMLHelper.WriteWords(path, words);
                    CSVHelper.WriteWords(path + ".csv", words);
                }
            }
            else
            {
                XMLHelper.WriteWords(outputFilePath, wordItems);
                CSVHelper.WriteWords(outputFilePath + ".csv", wordItems);
            }
        }
    }
}
