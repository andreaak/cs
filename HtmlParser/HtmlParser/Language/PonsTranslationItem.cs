using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslationComplexItem
    {
        public string Sense { get; set; }
        public IList<TranslationSimpleItem> Values { get; set; } = new List<TranslationSimpleItem>();
    }

    public class TranslationSimpleItem
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public string TranslationId { get; set; }
    }

    public class PonsItem : ITranslationItem
    {
        public TranslationAttributes Attributes { get; set; }
        public string Word { get; set; }

        public string SoundId => TranslationItems?.SelectMany(tr => tr?.Values)
            .FirstOrDefault(si => string.IsNullOrEmpty(si?.TranslationId))
            ?.TranslationId;
        public IList<TranslationComplexItem> TranslationItems { get; set; }

        public PonsItem(string word)
        {
            Word = word;
        }

        public static PonsItem ParseNode(HtmlNode node, string word)
        {
            var trNodes = node.SelectNodes(".//div[@class='flex flex-col']/div[@class='flex flex-col']");

            var item = new PonsItem(word);
            item.Attributes = item.GetAttributes(node);
            item.TranslationItems = trNodes.Select(item.ParseTranslationItem).ToArray();

            return item;
        }

        public TranslationAttributes GetAttributes(HtmlNode node)
        {
            var nodes = node.SelectNodes(".//h3//span")?.ToArray() ?? Array.Empty<HtmlNode>();



            if (!nodes.Any())
            {
                return null;
            }

            var attributes = new TranslationAttributes();

            foreach (var span in nodes.Where(n => n.HasAttributes))
            {
                if (span.Attributes.Any(a => a.Value == "phonetics"))
                {
                    attributes.Phonetics = span.InnerText.Trim().RemoveNewLine();
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
                else if (span.Attributes.Any(a => a.Value == "wordclass"))
                {
                    attributes.WordClass = GetWordClass(span);
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

        public virtual string GetWordClass(HtmlNode node)
        {
            var value = node?.InnerText.Trim().ToLower();
            return value;
        }

        protected virtual TranslationComplexItem ParseTranslationItem(HtmlNode node)
        {
            var sense = node.SelectSingleNode("./span[@class='sense']|./span[@class='topic']")?.InnerText.PonsNormalize();
            var trItems = node.SelectNodes(".//dl[@data-e2e='translation']")?.ToArray() ?? Array.Empty<HtmlNode>();

            var res = new TranslationComplexItem
            {
                Sense = sense
            };

            foreach (var trItem in trItems)
            {
                var source = GetSource(trItem);
                var description = GetDescription(trItem);
                var target = trItem.SelectSingleNode(".//div[@data-e2e='translation-target']")?.InnerText.PonsNormalize();
                var translationId = trItem.SelectSingleNode(".//button[@data-language='de']")?.GetAttributeValue("data-translation-id", "");

                if (source != null)
                {
                    res.Values.Add(new TranslationSimpleItem
                    {
                        Source = source,
                        Target = string.IsNullOrEmpty(description) ? target : $"[{description}] {target}",
                        TranslationId = translationId
                    });
                }
            }

            return res;
        }

        protected virtual string GetDescription(HtmlNode trItem)
        {
            var source = trItem.SelectSingleNode(".//div[@data-e2e='translation-source']/span[@class='sense']|.//div[@data-e2e='translation-source']/span[@class='topic']|.//div[@data-e2e='translation-source']/span[@class='collocator']")?
                .InnerText.PonsNormalize();
            return source;
        }

        protected virtual string GetSource(HtmlNode trItem)
        {
            var source = trItem.SelectSingleNode(".//div[@data-e2e='translation-source']/strong[@class='headword']")?.InnerText.PonsNormalize();
            return source;
        }

        public string GetTranscription(IEnumerable<ITranslationItem> allNodes)
        {
            if (allNodes == null)
            {
                return null;
            }

            foreach (var node in allNodes)
            {
                if (!string.IsNullOrEmpty(node.Attributes?.Phonetics))
                {
                    return node.Attributes?.Phonetics;
                }
            }

            return null;
        }

        public IList<string> GetTranslations(string word)
        {
            var list = new List<string>();

            foreach (var item in TranslationItems)
            {
                var trs = item.Values.Where(v => IsProperNode(word, v.Source)).Select(i => i.Target).Distinct().ToArray();
                if (trs.Any())
                {
                    list.Add(GetSense(item.Sense) + string.Join("/", trs));
                }
                else if (!string.IsNullOrEmpty(item.Sense))
                {
                    var tr = item.Values.FirstOrDefault();
                    list.Add(GetSense(item.Sense) + $"{tr?.Source} {tr?.Target}" );
                }
            }

            if (!list.Any() && TranslationItems.Count != 0)
            {
                var val = TranslationItems[0].Values.FirstOrDefault()?.Target;
                if (val != null)
                {
                    list.Add(val);
                }
            }

            return list;
        }

        private string GetSense(string sense)
        {
            if (string.IsNullOrEmpty(sense))
            {
                return "";
            }

            string formatted = sense
                .Replace(":", "--")
                .Replace("(", "")
                .Replace(")", "")
                .Trim();

            return $"({formatted}): ";
        }

        private static bool IsProperNode(string word, string source)
        {
            return word.Equals(source, StringComparison.CurrentCultureIgnoreCase)
                   || (word + "(r, s)").Equals(source, StringComparison.CurrentCultureIgnoreCase)
                   || source.StartsWith("sich " + word, StringComparison.CurrentCultureIgnoreCase);
        }
    }

    //public class PonsTranslationItem : ITranslationItem
    //{
    //    public TranslationAttributes Attributes { get; set; }
    //    public string SoundId { get; set; }

    //    public HtmlNode RomHeadNode { get; set; }
    //    //public IEnumerable<HtmlNode> TranslationNodes { get; set; }
    //    public IList<TranslationComplexItem> TranslationItems { get; set; }

    //    protected readonly string word;

    //    public PonsTranslationItem(string word)
    //    {
    //        this.word = word;
    //    }

    //    public PonsTranslationItem ParseSingleNode(HtmlNode node)
    //    {
    //        IList<HtmlNode> translations;
    //        try
    //        {
    //            translations = node.SelectNodes(".//dl[@data-e2e='translation']")?.ToArray() ?? Array.Empty<HtmlNode>();
    //        }
    //        catch (Exception e)
    //        {
    //            translations = Array.Empty<HtmlNode>();
    //        }

            
    //        var translationItems = translations.Select(ParseTranslationItem).ToArray();
            
    //        return CreateItem(node, translationItems);
    //    }

    //    protected virtual PonsTranslationItem CreateItem(HtmlNode node, TranslationComplexItem[] translationItems)
    //    {
    //        return new PonsTranslationItem(word)
    //        {
    //            RomHeadNode = node.SelectSingleNode(".//div[@class='romhead']"),
    //            //TranslationNodes = translationNodes,
    //            TranslationItems = translationItems
    //        };
    //    }

    //    protected TranslationComplexItem ParseTranslationItem(HtmlNode node)
    //    {
    //        var sense = node.SelectSingleNode(".//span[@class='sense']|.//span[@class='topic']")?.InnerText.PonsNormalize();
    //        var trItems = node.SelectNodes(".//dl[@data-translation]")?.ToArray() ?? Array.Empty<HtmlNode>();

    //        var res = new TranslationComplexItem
    //        {
    //            Sense = sense
    //        };

    //        foreach (var trItem in trItems)
    //        {
    //            var source = GetSource(trItem);
    //            var description = GetDescription(trItem);
    //            var target = trItem.SelectSingleNode(".//div[@class='target']")?.InnerText.PonsNormalize();

    //            if (source != null)
    //            {
    //                res.Values.Add(new TranslationSimpleItem
    //                {
    //                    Source = source,
    //                    Target = string.IsNullOrEmpty(description) ? target : $"[{description}] {target}" 
    //                });
    //            }
    //        }

    //        return res;
    //    }

    //    protected virtual string GetDescription(HtmlNode trItem)
    //    {
    //        var source = trItem.SelectSingleNode(".//div[@class='source']/span[@class='collocator']")?
    //            .InnerText.PonsNormalize();
    //        return source;
    //    }

    //    protected virtual string GetSource(HtmlNode trItem)
    //    {
    //        var source = trItem.SelectSingleNode(".//div[@class='source']")?.InnerText.PonsNormalize();
    //        return source;
    //    }

    //    public TranslationAttributes GetAttributes(HtmlNode node)
    //    {
    //        var nodes = RomHeadNode.SelectNodes(".//span")?.ToArray() ?? Array.Empty<HtmlNode>();

    //        if (!nodes.Any())
    //        {
    //            return null;
    //        }

    //        var attributes = new TranslationAttributes();

    //        foreach (var span in nodes.Where(n => n.HasAttributes))
    //        {
    //            if (span.Attributes.Any(a => a.Value == "phonetics"))
    //            {
    //                attributes.Phonetics = span.InnerText.Trim().RemoveNewLine();
    //            }
    //            else if (span.Attributes.Any(a => a.Value == "flexion"))
    //            {
    //                attributes.Flexion = HttpUtility.HtmlDecode(span.InnerText.Trim().RemoveNewLine());
    //            }
    //            else if (span.Attributes.Any(a => a.Value == "genus"))
    //            {
    //                attributes.Genus = span.InnerText.Trim();
    //                attributes.Artikle = GetArtikle(attributes.Genus);
    //            }
    //            else if (span.Attributes.Any(a => a.Value == "info"))
    //            {
    //                attributes.Info = HttpUtility.HtmlDecode(span.InnerText.Trim().RemoveNewLine());
    //            }
    //            else if (span.Attributes.Any(a => a.Value == "verbclass"))
    //            {
    //                attributes.VerbClass = span.InnerText.Trim();
    //            }
    //            else if (span.Attributes.Any(a => a.Value == "headword_attributes"))
    //            {
    //                attributes.HeadWord = span.InnerText.Trim();
    //            }
    //        }

    //        return attributes;
    //    }

    //    public string GetTranscription(IEnumerable<ITranslationItem> allNodes)
    //    {
    //        if (allNodes == null)
    //        {
    //            return null;
    //        }

    //        //foreach (var node in allNodes.Where(node => node.RomHeadNode.Id != RomHeadNode?.Id))
    //        //{
    //        //    foreach (var span in node.RomHeadNode.SelectNodes(".//span"))
    //        //    {
    //        //        if (span.HasAttributes && span.Attributes.Any(a => a.Value == "phonetics"))
    //        //        {
    //        //            return span.InnerText.Trim();
    //        //        }
    //        //    }
    //        //}

    //        return null;
    //    }

    //    public static string GetArtikle(string genus)
    //    {
    //        switch (genus?.ToLower())
    //        {
    //            case "m":
    //                return "der";
    //            case "f":
    //                return "die";
    //            case "nt":
    //                return "das";
    //            case "m(f)":
    //                return "der(die)";
    //            case "f(m)":
    //                return "die(der)";
    //        }

    //        return null;
    //    }

    //    public virtual string GetWordClass()
    //    {
    //        return RomHeadNode.SelectSingleNode(".//span[@class='wordclass']")?.InnerText.Trim().ToLower();
    //    }

    //    public IList<string> GetTranslations(string word)
    //    {
    //        var list = new List<string>();

    //        foreach (var item in TranslationItems)
    //        {
    //            var trs = item.Values.Where(v => IsProperNode(word, v.Source)).Select(i => i.Target).Distinct().ToArray();
    //            if (trs.Any())
    //            {
    //                list.Add(GetSense(item.Sense) + string.Join("/", trs));
    //            }
    //            else if (!string.IsNullOrEmpty(item.Sense))
    //            {
    //                list.Add(GetSense(item.Sense));
    //            }
    //        }

    //        if (!list.Any() && TranslationItems.Count != 0)
    //        {
    //            var val = TranslationItems[0].Values.FirstOrDefault()?.Target;
    //            if (val != null)
    //            {
    //                list.Add(val);
    //            }
    //        }

    //        return list;
    //    }


    //    private string GetSense(string sense)
    //    {
    //        if (string.IsNullOrEmpty(sense))
    //        {
    //            return "";
    //        }

    //        string formatted = sense
    //            .Replace(":", "--")
    //            .Replace("(", "")
    //            .Replace(")", "")
    //            .Trim();

    //        return $"({formatted}): ";
    //    }


    //    //private IEnumerable<HtmlNode> Filter(string word)
    //    //{
    //    //    var filtered = TranslationNodes.Where(node => Filter(node, word)).ToArray();
    //    //    if (filtered.Length == 0 && TranslationNodes.Count() != 0)
    //    //    {
    //    //        filtered = new[] { TranslationNodes.First() };
    //    //    }
    //    //    return filtered;
    //    //}

    //    //private static bool Filter(HtmlNode node, string word)
    //    //{
    //    //    var source = node.SelectSingleNode(".//div[@class='source']");

    //    //    var s = Normalize(source);
    //    //    return IsProperNode(word, s);
    //    //}

    //    private static bool IsProperNode(string word, string source)
    //    {
    //        return word.Equals(source, StringComparison.CurrentCultureIgnoreCase)
    //               || (word + "(r, s)").Equals(source, StringComparison.CurrentCultureIgnoreCase)
    //               || source.StartsWith("sich " + word, StringComparison.CurrentCultureIgnoreCase);
    //    }

    //    //private static string GetNodeTranslation(HtmlNode node)
    //    //{
    //    //    var transNode = node.SelectSingleNode(".//div[@class='target']");
    //    //    var transl = (/*transNode.SelectNodes(".//a")?.ToArray() ?? */new[] { transNode }).Select(Normalize).ToArray();
    //    //    return string.Join(" ", transl);
    //    //}

    //    //private static string Normalize(HtmlNode node)
    //    //{
    //    //    return node?.InnerText?.PonsNormalize();
    //    //}

    //    //public string GetLink()
    //    //{
    //    //    var a = TranslationNodes.FirstOrDefault().SelectSingleNode(".//a");

    //    //    return a.Attributes.FirstOrDefault(at => at.Name == "href")?.Value;
    //    //}
    //}
}