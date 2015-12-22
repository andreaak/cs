using System.Collections.Generic;
using System.Text;
using Utils;

namespace Localization
{
    class CsvReader : IReader
    {
        private IParser parser;
        private string filePath;
        public CsvReader(IParser parser, string filePath)
        {
            this.parser = parser;
            this.filePath = filePath;
        }

        public SortedDictionary<string, string> Read()
        {
            ReadInfo.Open(filePath);
            SortedDictionary<string, string> words = new SortedDictionary<string, string>();
            string line;
            while ((line = ReadInfo.ReadLine()) != null)
            {
                if (parser.IsEmptyLine(line)
                    || parser.IsCommentLine(line))
                {
                    continue;
                }
                AddWord(words, line);
            }
            ReadInfo.Close();
            return words;
        }

        private void AddWord(SortedDictionary<string, string> words, string line)
        {
            string key = parser.GetBaseValue(line);
            string value = parser.GetLocalizedValue(line);

            if (!string.IsNullOrEmpty(key))
            {
                words[key] = value.ToString();
            }
        }
    }
}
