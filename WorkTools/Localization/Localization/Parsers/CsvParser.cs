using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Localization
{
    class CsvParser : IParser
    {
        #region IParser Members

        public bool IsEmptyLine(string line)
        {
            return string.IsNullOrEmpty(line);
        }

        public bool IsCommentLine(string line)
        {
            return line.StartsWith(Options.Comment);
        }

        public bool IsBaseLine(string line)
        {
            throw new NotSupportedException();
        }

        public bool IsLocalizedLine(string line)
        {
            throw new NotSupportedException();
        }

        public string GetBaseValue(string line)
        {
            string[] words = line.Split(Options.ExcelSeparator);
            if (words.Length != 0)
            {
                return words[0];
            }
            return string.Empty;
        }

        public string GetLocalizedValue(string line)
        {
            string[] words = line.Split(Options.ExcelSeparator);
            if (words.Length > 1)
            {
                return words[1];
            }
            return string.Empty;
        }

        #endregion
    }
}
