using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CSTest._08_String._01_RegularExpression
{
    [TestClass]
    public class TestReplace
    {
        string translatePattern = @"^[\W\d]*(?<OtherWord>{0})[\W\d]*$";

        [TestMethod]
        public void TestRegExpReplace1()
        {
            string text = "Text For {0} Translate >>>";

            string textBase = "Text for {0} Translate";
            string translation = "Текст {0} перевода";
            string pattern = string.Format(translatePattern,  Regex.Escape(textBase));

            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
            if (matches.Count != 0)
            {
                Debug.WriteLine("Text: {0}{3}Base: {1}{3}Translation: {2}", text, textBase,
                                text.Replace(matches[0].Groups["OtherWord"].Value, translation), Environment.NewLine);
            }
            else
            {
                Debug.WriteLine("No matches");
            }

            /*
            Text: Text For {0} Translate >>>
            Base: Text for {0} Translate
            Translation: Текст {0} перевода >>>
            */
        }

        [TestMethod]
        public void TestRegExpReplace2()
        {
            string text = "<<<Text For {0} Translate >>>";

            string textBase = "Text For {0} Translate";
            //string textBase = "For";
            string translation = "Текст {0} перевода";
            string pattern = string.Format(translatePattern, Regex.Escape(textBase));

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            string result = regex.Replace(text, Translate);

            Debug.WriteLine("Text: {0}{3}Base: {1}{3}Translation: {2}", text, textBase,
                            result, Environment.NewLine);

            /*
            Text: Text For {0} Translate >>>
            Base: Text For {0} Translate
            Translation: Текст {0} перевода >>>
            */
        }

        private static string Translate(Match match)
        {
            return match.Groups["OtherWord"].Value + 1;
        }

        private static void WriteMatches(string text, MatchCollection matches)
        {
            Debug.WriteLine("Original text was: \n\n" + text + "\n");
            Debug.WriteLine("No. of matches: " + matches.Count);
            foreach (Match nextMatch in matches)
            {
                int index = nextMatch.Index;
                string result = nextMatch.ToString();
                int charsBefore = (index < 5) ? index : 5;
                int fromEnd = text.Length - index - result.Length;
                int charsAfter = (fromEnd < 5) ? fromEnd : 5;
                int charsToDisplay = charsBefore + charsAfter + result.Length;

                Debug.WriteLine("Index: {0}, \tString: {1}, \t{2}",
                   index, result,
                   text.Substring(index - charsBefore, charsToDisplay));

            }
        }
    }
}
