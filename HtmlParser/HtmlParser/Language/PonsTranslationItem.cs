using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class PonsTranslationItem : ITranslationItem
    {
        public HtmlNode RomHeadNode { get; set; }
        public IEnumerable<HtmlNode> TranslationNodes { get; set; }

        public static PonsTranslationItem ParseSingleNode(HtmlNode node)
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
            return new PonsTranslationItem
            {
                RomHeadNode = node.SelectSingleNode(".//div[@class='romhead']"),
                TranslationNodes = translation.Concat(translations).ToArray()
            };
        }

        public TranslationAttributes GetAttributes()
        {
            var nodes = RomHeadNode.SelectNodes(".//span")?.ToArray() ?? Array.Empty<HtmlNode>();

            if (!nodes.Any())
            {
                return null;
            }

            var attributes = new TranslationAttributes();

            foreach (var span in nodes.Where(n => n.HasAttributes))
            {
                if (span.Attributes.Any(a => a.Value == "phonetics"))
                {
                    attributes.Phonetics = span.InnerText.Trim();
                }
                else if (span.Attributes.Any(a => a.Value == "flexion"))
                {
                    attributes.Flexion = HttpUtility.HtmlDecode(span.InnerText.Trim().RemoveNewLine());
                }
                else if (span.Attributes.Any(a => a.Value == "genus"))
                {
                    attributes.Genus = span.InnerText.Trim();
                    attributes.Artikle = GetArtikle(attributes.Genus);
                }
                else if (span.Attributes.Any(a => a.Value == "info"))
                {
                    attributes.Info = HttpUtility.HtmlDecode(span.InnerText.Trim().RemoveNewLine());
                }
                else if (span.Attributes.Any(a => a.Value == "verbclass"))
                {
                    attributes.VerbClass = span.InnerText.Trim();
                }
                else if (span.Attributes.Any(a => a.Value == "headword_attributes"))
                {
                    attributes.HeadWord = span.InnerText.Trim();
                }
            }

            return attributes;
        }

        public string GetTranscription(IEnumerable<ITranslationItem> allNodes)
        {
            if (allNodes == null)
            {
                return null;
            }

            foreach (var node in allNodes.Where(node => node.RomHeadNode.Id != RomHeadNode?.Id))
            {
                foreach (var span in node.RomHeadNode.SelectNodes(".//span"))
                {
                    if (span.HasAttributes && span.Attributes.Any(a => a.Value == "phonetics"))
                    {
                        return span.InnerText.Trim();
                    }
                }
            }

            return null;
        }

        public static string GetArtikle(string genus)
        {
            switch (genus?.ToLower())
            {
                case "m":
                    return "der";
                case "f":
                    return "die";
                case "nt":
                    return "das";
                case "m(f)":
                    return "der(die)";
                case "f(m)":
                    return "die(der)";
            }

            return null;
        }

        public string GetWordClass()
        {
            return RomHeadNode.SelectSingleNode(".//span[@class='wordclass']")?.InnerText.Trim().ToLower();
        }

        public IList<string> GetTranslations(string word)
        {
            return Filter(word).Select(GetNodeTranslation).Distinct().ToArray();
        }

        public string GetTranslation(string word)
        {
            var translations = string.Join("/", Filter(word).Select(GetNodeTranslation).Distinct());
            return translations;
        }

        private IEnumerable<HtmlNode> Filter(string word)
        {
            var filtered = TranslationNodes.Where(node => Filter(node, word)).ToArray();
            if (filtered.Length == 0 && TranslationNodes.Count() != 0)
            {
                filtered = new[] { TranslationNodes.First() };
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
            var transl = (/*transNode.SelectNodes(".//a")?.ToArray() ?? */new[] { transNode }).Select(GetTranslation).ToArray();
            return string.Join(" ", transl);
        }

        private static string GetTranslation(HtmlNode node)
        {
            return node?.InnerText?.Trim().Replace("\n", " ").Replace("\t", " ").Replace("\u0341", "").Replace("  ", " ");
        }

        public string GetLink()
        {
            var a = TranslationNodes.FirstOrDefault().SelectSingleNode(".//a");

            return a.Attributes.FirstOrDefault(at => at.Name == "href")?.Value;
        }
    }
}