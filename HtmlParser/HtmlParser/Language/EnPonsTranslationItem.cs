using System;
using System.Linq;
using HtmlAgilityPack;
using HtmlParser.Language.Extensions;

namespace HtmlParser.Language
{
    public class EnPonsItem : PonsItem
    {
        public EnPonsItem(string word) : base(word)
        {
        }

        protected override TranslationComplexItem ParseTranslationItem(HtmlNode node)
        {
            var sense = node.SelectSingleNode("./span[@class='sense']|./span[@class='topic']")?.InnerText.PonsNormalize();
            if (string.IsNullOrEmpty(sense))
            {
                sense = node.SelectSingleNode(".//h4[@class='text-p2']/span[@class='sense']|.//h4[@class='text-p2']/span[@class='topic']")?.InnerText.PonsNormalize();
            }
            
            var trItems = node.SelectNodes(".//dl[@data-e2e='translation']")?.ToArray() ?? Array.Empty<HtmlNode>();

            var res = new TranslationComplexItem
            {
                Sense = sense
            };

            TranslationSimpleItem first = null;
            
            foreach (var trItem in trItems)
            {
                var source = GetSource(trItem);
                var description = GetDescription(trItem);
                var target = trItem.SelectSingleNode(".//div[@data-e2e='translation-target']")?.InnerText.PonsNormalize();
                var translationId = trItem.SelectSingleNode(".//button[@data-language='en']")?.GetAttributeValue("data-translation-id", "");

                if (first == null)
                {
                    first = new TranslationSimpleItem
                    {
                        Source = source,
                        Target = string.IsNullOrEmpty(description) ? target : $"[{description}] {target}",
                        TranslationId = translationId
                    };
                }
                
                if (Word.Equals(source.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    res.Values.Add(new TranslationSimpleItem
                    {
                        Source = source,
                        Target = string.IsNullOrEmpty(description) ? target : $"[{description}] {target}",
                        TranslationId = translationId
                    });
                }
            }

            if (res.Values.Count == 0 && first != null)
            {
                res.Values.Add(first);
            }

            return res;
        }

        public static EnPonsItem ParseNode(HtmlNode node, string word)
        {
            var trNodes = node.SelectNodes(".//div[@class='flex flex-col']/div[@class='flex flex-col']");

            var nodes = node.SelectNodes(".//h3/span")?.ToArray() ?? Array.Empty<HtmlNode>();

            foreach (var wordNode in nodes)
            {
                if (wordNode.ChildNodes.Any(i => i.Name == "span"))
                {
                    var text = wordNode.ChildNodes[0].InnerText.PonsNormalize();
                    if (!text.Equals(word, StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }
                }
            }

            var item = new EnPonsItem(word);
            item.Attributes = item.GetAttributes(node);
            item.TranslationItems = trNodes.Select(item.ParseTranslationItem).Where(t => t.Values.Count != 0 || !string.IsNullOrEmpty(t.Sense)).ToArray();

            return item;
        }

        public override string GetWordClass(HtmlNode node)
        {
            var value = node?.InnerText.Trim().ToLower();
            switch (value)
            {
                case "vb":
                    return "verb";
                case "n":
                    return "noun";
                default:
                    return value;
            }
        }

        protected override string GetSource(HtmlNode trItem)
        {
            var source = base.GetSource(trItem);
            if (!string.IsNullOrEmpty(source))
            {
                return source;
            }

            source = trItem.SelectSingleNode(".//div[@data-e2e='translation-source']")?.InnerText.PonsNormalize();

            return source; // == Word ? source : null;
        }
    }
}