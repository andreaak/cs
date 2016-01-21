using System.Collections.Generic;
using System.Text;
using Utils;

namespace Localization.Readers
{
    class PoReader : IReader
    {
        private IParser parser;
        private string filePath;
        public PoReader(IParser parser, string filePath)
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
                if (parser.IsBaseLine(line))
                {
                    AddWord(words, line);
                }
            }
            ReadInfo.Close();
            return words;
        }

        private void AddWord(SortedDictionary<string, string> words, string line)
        {
            StringBuilder key = new StringBuilder(parser.GetBaseValue(line));
            StringBuilder value = new StringBuilder();
            while ((line = ReadInfo.ReadLine()) != null)
            {
                if (parser.IsEmptyLine(line))
                {
                    break;
                }
                else if (parser.IsCommentLine(line))
                {
                    continue;
                }
                else if (!parser.IsLocalizedLine(line))
                {
                    key.Append(parser.GetBaseValue(line));
                }
                else
                {
                    value.Append(parser.GetLocalizedValue(line));
                }
            }
            string temp = key.ToString();
            if (!string.IsNullOrEmpty(temp))
            {
                words[temp] = value.ToString();
            }
        }
    }
}
