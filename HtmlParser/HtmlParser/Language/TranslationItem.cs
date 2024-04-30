using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace HtmlParser.Language
{
    public class TranslationItem
    {
        public HtmlNode RomHeadNode { get; set; }
        public IEnumerable<HtmlNode> TranslationNodes { get; set; }

        public static IEnumerable<TranslationItem> Parse(HtmlNode node, string word)
        {
            var romHeads = node.SelectNodes(".//div[@class='romhead']");

            return romHeads
                .Select(romHead => romHead.ParentNode)
                .Select(n => ParseSingleNode(n, word))
                .ToList();
        }

        public static TranslationItem ParseSingleNode(HtmlNode node, string word)
        {

            IList<HtmlNode> translation;
            try
            {
                translation = node.SelectNodes(".//div[@class='translations first']")?.SelectMany(n => n.SelectNodes(".//dl[@data-translation]"))?.ToArray() ?? Array.Empty<HtmlNode>();
            }
            catch (Exception e)
            {
                translation = Array.Empty<HtmlNode>();
            }
            
            //var translation = node.SelectNodes(".//div[@class='translations first']")?.SelectMany(n => n.SelectNodes(".//dl[@data-translation]"))?.ToArray() ?? Array.Empty<HtmlNode>();

            var translations = node.SelectNodes(".//div[@class='translations']")?.SelectMany(n => n.SelectNodes(".//dl[@data-translation]"))?.ToArray() ?? Array.Empty<HtmlNode>();
            //var translations = node.SelectNodes(".//div[@class='translations']")?.Select(n => n.SelectSingleNode(".//div[@class='target']"))?.ToArray() ?? Array.Empty<HtmlNode>();
            return new TranslationItem
            {
                RomHeadNode = node.SelectSingleNode(".//div[@class='romhead']"),
                TranslationNodes = translation.Concat(translations).ToArray()
            };
        }
    }
}