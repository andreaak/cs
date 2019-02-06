using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Localization
{
    public class PoParser : IParser
    {
        bool isBaseLineFound;
        bool isLocalizeLineFound;
        const string baseLinePattern = "msgid \"";
        const string localizedLinePattern = "msgstr \"";
        const string separator = "\"";

        public bool IsBaseLine(string line)
        {
            if (!isBaseLineFound && line.StartsWith(baseLinePattern)
                || isBaseLineFound)
            {
                isBaseLineFound = true;
                isLocalizeLineFound = false;
                return true;
            }
            return false;
        }

        public bool IsEmptyLine(string line)
        {
            return string.IsNullOrEmpty(line);
        }

        public bool IsLocalizedLine(string line)
        {
            if (isBaseLineFound && line.StartsWith(localizedLinePattern)
                || isLocalizeLineFound)
            {
                isBaseLineFound = false;
                isLocalizeLineFound = true;
                return true;
            }
            return false;
        }

        public bool IsCommentLine(string line)
        {
            return line.StartsWith(Options.Comment);
        }

        public string GetBaseValue(string line)
        {
            return GetValue(line, baseLinePattern);
        }

        public string GetLocalizedValue(string line)
        {
            return GetValue(line, localizedLinePattern);
        }

        private string GetValue(string line, string linePattern)
        {
            int index = line.IndexOf(linePattern);
            if (index == -1)
            {
                linePattern = separator;
                index = line.IndexOf(linePattern);
            }
            int start = index + linePattern.Length;
            int end = line.LastIndexOf(separator) - start;
            return line.Substring(start, end);
        }
    }
}
