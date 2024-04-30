using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace HtmlParser.Language
{
    public static class HtmlNodeExtensions
    {
        public static string GetWordClass(this TranslationItem node)
        {
            return node?.RomHeadNode.SelectSingleNode(".//span[@class='wordclass']")?.InnerText.Trim().ToLower();
        }

        public static IList<string> GetTranslations(this TranslationItem node, string word)
        {
            return node.TranslationNodes.Filter(word).Select(GetNodeTranslation).Distinct().ToArray();
        }

        public static string GetTranslation(this TranslationItem node, string word)
        {
            var translations = string.Join("/", node.TranslationNodes.Filter(word).Select(GetNodeTranslation).Distinct());
            return translations;
        }

        private static IEnumerable<HtmlNode> Filter(this IEnumerable<HtmlNode> nodes, string word)
        {
            var filtered = nodes.Where(node => Filter(node, word)).ToArray();
            if (filtered.Length == 0 && nodes.Count() != 0)
            {
                filtered = new[] { nodes.First() };
            }
            return filtered;
        }

        private static bool Filter(HtmlNode node, string word)
        {
            var source = node.SelectSingleNode(".//div[@class='source']");

            var s = GetTranslation(source);
            return word.Equals(s, StringComparison.CurrentCultureIgnoreCase)
                   || (word + "(r, s)").Equals(s, StringComparison.CurrentCultureIgnoreCase)
                   || s.StartsWith("sich " + word, StringComparison.CurrentCultureIgnoreCase);
        }

        private static string GetNodeTranslation(HtmlNode node)
        {
            var transNode = node.SelectSingleNode(".//div[@class='target']");
            var transl = (/*transNode.SelectNodes(".//a")?.ToArray() ?? */new [] { transNode }).Select(GetTranslation).ToArray();
            return string.Join(" ", transl);
        }

        private static string GetTranslation(HtmlNode node)
        {
            return node?.InnerText?.Trim().Replace("\u0341", "").Replace("  ", " ");
        }

        public static string GetLink(this TranslationItem node)
        {
            var a = node.TranslationNodes.FirstOrDefault().SelectSingleNode(".//a");
            
            return a.Attributes.FirstOrDefault(at => at.Name == "href")?.Value;
        }
    }
    
    public static class StringExtensions
    {
        public static string RemoveNewLine(this string data)
        {
            return data?.Replace("\n", "").Replace("\t", "");
        }
    }
}