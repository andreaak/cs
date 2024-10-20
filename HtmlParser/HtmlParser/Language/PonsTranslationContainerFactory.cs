using System.Collections.Generic;
using System.Linq;

using HtmlAgilityPack;

namespace HtmlParser.Language
{
    public class PonsTranslationContainerFactory
    {
        public IList<TranslationContainer> GetTranslationContainer(HtmlDocument document, string word, WordType type)
        {
            var trNodes = GetTranslationItems(document, word);

            if (trNodes == null)
            {
                return null;
            }

            if (type == WordType.First)
            {
                return new[] { new TranslationContainer
                {
                    Node = trNodes.FirstOrDefault(),
                    AllNodes = trNodes
                }};
            }

            var list = new List<TranslationContainer>();

            foreach (var node in trNodes)
            {
                var wordClass = node.GetWordClass();
                if (wordClass == type.ToString().ToLower() || type == WordType.All)
                {
                    list.Add(new TranslationContainer
                    {
                        Node = node,
                        AllNodes = trNodes
                    });
                }
            }

            if (list.Count != 0)
            {
                return list;
            }

            return new[] { new TranslationContainer
            {
                AllNodes = trNodes
            }};
        }

        private IEnumerable<ITranslationItem> GetTranslationItems(HtmlDocument document, string word)
        {
            var trNodes = document.DocumentNode.SelectNodes(".//div[@rel]");

            var resNodes = new List<HtmlNode>();

            if (trNodes != null)
            {
                foreach (var node in trNodes)
                {
                    var rel = node.Attributes.FirstOrDefault(a => a.Name == "rel");
                    var value = rel?.Value.Trim().Replace("\u0301", "").ToLower();
                    var sourceWord = word.ToLower();

                    if (value != null && (value == sourceWord || value.IndexOf(sourceWord) == 0 && value.Length > sourceWord.Length && value[sourceWord.Length] == '('))
                    {
                        resNodes.Add(node);
                    }
                }
            }

            return resNodes.Count != 0 ? resNodes.SelectMany(n => Parse(n, word)).ToArray() : null;
        }

        private IEnumerable<PonsTranslationItem> Parse(HtmlNode node, string word)
        {
            var romHeads = node.SelectNodes(".//div[@class='romhead']");

            return romHeads.Select(romHead => romHead.ParentNode)
                .Select(PonsTranslationItem.ParseSingleNode)
                .ToList();
        }
    }
}