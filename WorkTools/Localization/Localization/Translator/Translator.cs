using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Localization
{
    class Translator
    {
        private const string TranslatePattern = @"^[\W\d]*(?<OtherWord>{0})[\W\d]*$";

        private SortedDictionary<string, string> translation;
        public SortedDictionary<string, string> Translations
        {
            get
            {
                return translation;
            }
            set
            {
                translation = value;
            }
        }

        public string Translate(string text)
        {
            foreach (var translation in Translations)
	        {
                string pattern = string.Format(TranslatePattern, Regex.Escape(translation.Key));
                MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
                if (matches.Count != 0)
                {
                    return text.Replace(matches[0].Groups["OtherWord"].Value, translation.Value);
                }
	        }
            return null;
        }
    }
}
