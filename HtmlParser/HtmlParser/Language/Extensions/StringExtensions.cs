using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlParser.Language.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveNewLine(this string data)
        {
            return data?.Replace("\n", "").Replace("\t", "");
        }

        public static string SplitString(this string inText, int chankSize = 30)
        {

            if (string.IsNullOrEmpty(inText))
            {
                return null;
            }

            var texts = inText.Split(new[] { '/' }, StringSplitOptions.None);

            var text = "";
            var tempText = "";

            foreach (var str in texts)
            {
                if (tempText.Length != 0 && tempText.Length + str.Length > chankSize)
                {
                    if (text.Length != 0)
                    {
                        text += "\r\n";
                    }
                    text += tempText + "/";
                    tempText = str;
                }
                else
                {
                    if (tempText.Length != 0)
                    {
                        tempText += "/";
                    }
                    tempText += str;
                }
            }
            if (tempText.Length != 0)
            {
                if (text.Length != 0)
                {
                    text += "\r\n";
                }
                text += tempText;
            }
            return text;
        }

        public static string PonsNormalize(this string text)
        {
            var testString =  text?.Trim()
                .Replace("\n", " ").Replace("\t", " ");

            var text2 = Regex.Replace(testString, @"[\u0301-\u0341]", "");
            text2 = Regex.Replace(text2, @"\s+", " ");
            return text2;
        }

        public static bool IsOther(this string first, string second)
        {
            var items = second.Replace(",", "/").Replace(" ", "").ToLower()
                .Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries).ToArray();


            var pons = first.Replace(",", "/").Replace("  ", " ").ToLower();

            return items.Any(i => !pons.Contains(i));
        }

        private static Regex regExp = new Regex("\\(.*?\\)\\:");

        public static string AnotherTranslation(this string first, string second)
        {
            var items = second
                .Replace(",", "/")
                .ToLower()
                .Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries).ToArray();



            var pons = first
                .Replace(",", "/")
                .Replace("  ", " ")
                .Replace("impf", "")
                .Replace("perf", " ")
                .ToLower();
                
            pons = regExp.Replace(pons, "")   ;   
                
            var ponsItems = pons.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim()).ToArray();

            return string.Join("/", items.Except(ponsItems));
        }
    }
}