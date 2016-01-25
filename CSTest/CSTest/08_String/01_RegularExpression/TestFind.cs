using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CSTest._08_String._01_RegularExpression
{
    [TestClass]
    public class TestFind
    {
        private const string myText =
        @"This comprehensive compendium provides a broad and thorough investigation of all 
        aspects of programming with ASP.NET. Entirely revised and updated for the fourth 
        release of .NET, this book will give you the information you need to 
        master ASP.NET and build a dynamic, successful, enterprise Web application.";

        private const string translatePattern = @"^[\W\d]*(?<OtherWord>{0})[\W\d]*$";

        const string text = @"XML has made a major impact in almost every aspect of 
        software development. Designed as an open, extensible, self-describing 
        language, it has become the standard for data and document delivery on 
        the web. The panoply of XML-related technologies continues to develop 
        at breakneck speed, to enable validation, navigation, transformation, 
        linking, querying, description, and messaging of data.";

        [TestMethod]
        public void TestRegExp1()
        {
            const string pattern = "ion";
            MatchCollection matches = Regex.Matches(myText, pattern,
                                        RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            WriteMatches(myText, matches);
            /*
            Original text was: 

            This comprehensive compendium provides a broad and thorough investigation of all 
            ...
            master ASP.NET and build a dynamic, successful, enterprise Web application.

            No. of matches: 3
            Index: 70, 	    String: ion, 	tigation of a
            Index: 235, 	String: ion, 	ormation you 
            Index: 332, 	String: ion, 	lication.
            */
        }

        [TestMethod]
        public void TestRegExp2()
        {
            const string pattern = @"\bn";
            MatchCollection matches = Regex.Matches(myText, pattern,
                                        RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            WriteMatches(myText, matches);
            /*
            No. of matches: 4
            Index: 123, 	String: N, 	 ASP.NET. E
            Index: 194, 	String: N, 	 of .NET, t
            Index: 243, 	String: n, 	 you need t
            Index: 272, 	String: N, 	 ASP.NET an
            */
        }

        [TestMethod]
        public void TestRegExp3()
        {
            const string pattern = @"ion\b";
            MatchCollection matches = Regex.Matches(myText, pattern,
                                        RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            WriteMatches(myText, matches);
            /*
            No. of matches: 3
            Index: 70, 	String: ion, 	tigation of a
            Index: 235, 	String: ion, 	ormation you 
            Index: 332, 	String: ion, 	lication.
            */
        }

        [TestMethod]
        public void TestRegExp4()
        {
            const string pattern = @"\ba\S*ion\b";
            MatchCollection matches = Regex.Matches(myText, pattern,
                                        RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            WriteMatches(myText, matches);
            /*
            No. of matches: 1
            Index: 324, 	String: application, 	 Web application.
            */
        }

        [TestMethod]
        public void TestRegExp5()
        {
            const string pattern = @"\bn\S*ion\b";
            MatchCollection matches = Regex.Matches(text, pattern,
                                       RegexOptions.IgnoreCase |
                                       RegexOptions.IgnorePatternWhitespace |
                                       RegexOptions.ExplicitCapture);
            WriteMatches(text, matches);
            /*
            No. of matches: 1
            Index: 348, 	String: navigation, 	ion, navigation, tra
            */
        }

        [TestMethod]
        public void TestRegExp6()
        {
            string text = "Text For {0} Translate >>>";
            string textBase = "For";
            string pattern = string.Format(translatePattern, Regex.Escape(textBase));
            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
            Assert.IsTrue(matches.Count == 0, "Wrong pattern");

            textBase = "Text For {0} Translate";
            pattern = string.Format(translatePattern, Regex.Escape(textBase));
            matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
            Assert.IsFalse(matches.Count == 0, "Wrong pattern");

            text = "<<<Text For {0} Translate>>>";
            textBase = "Text For {0} Translate";
            pattern = string.Format(translatePattern, Regex.Escape(textBase));
            matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
            Assert.IsFalse(matches.Count == 0, "Wrong pattern");

            /*
            */
        }

        [TestMethod]
        public void TestRegExpIsMatch1()
        {
            string text = "<<<Text For {0} Translate >>>";
            string textBase = "Text For {0} Translate";
            string pattern = string.Format(translatePattern, Regex.Escape(textBase));

            Regex regex = new Regex(pattern);
            bool isMatch = regex.IsMatch(text);
            Debug.WriteLine("{0} ? {1}", text, isMatch ? "ok" : "bad");
            /*
            <<<Text For {0} Translate >>> ? ok
            */
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
