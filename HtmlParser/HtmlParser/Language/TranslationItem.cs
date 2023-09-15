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

        public static IEnumerable<TranslationItem> Parse(HtmlNode node)
        {
            var romHeads = node.SelectNodes(".//div[@class='romhead']");

            return romHeads
                .Select(romHead => romHead.ParentNode)
                .Select(ParseSingleNode)
                .ToList();
        }

        public static TranslationItem ParseSingleNode(HtmlNode node)
        {
            var translation = node.SelectNodes(".//div[@class='translations first']//div[@class='target']")?.ToArray() ?? Array.Empty<HtmlNode>();
            var translations = node.SelectNodes(".//div[@class='translations']//div[@class='target']")?.ToArray() ?? Array.Empty<HtmlNode>();
            return new TranslationItem
            {
                RomHeadNode = node.SelectSingleNode(".//div[@class='romhead']"),
                TranslationNodes = translation.Concat(translations).ToArray()
            };
        }
    }
}