using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Web;
using HtmlAgilityPack;
using HtmlParser.Language.Extensions;

namespace HtmlParser.Language.Containers
{
    public class DWDSTranslationContainerFactory
    {
        protected string HostUrl => "https://www.dwds.de/wb/";

        protected readonly string _word;
        protected readonly WordType _type;
        protected IList<DWDSItem> _containers;

        public DWDSTranslationContainerFactory(string de, WordType type)
        {
            _word = de;
            _type = type;
        }

        public IList<DWDSItem> GetWords(string separator = "/")
        {
            var trContainers = GetTranslationContainer();

            if (trContainers == null || trContainers.Count == 0)
            {
                Console.WriteLine($"Not found {_word}");
                return new List<DWDSItem>();
            }

            _containers = trContainers;
            return trContainers;
        }

        protected IList<DWDSItem> GetTranslationContainer()
        {
            var document = new HtmlParser().GetHtml(HostUrl + _word);
            if (document == null)
            {
                return null;
            }

            var list = new List<DWDSItem>();

            var artikelNodes = document.DocumentNode.SelectNodes(".//div[@class='dwdswb-artikel']");
            if (artikelNodes == null)
            {
                return new List<DWDSItem>();
            }

            foreach (var artikelNode in artikelNodes)
            {
                var other = artikelNode.SelectNodes(".//div[@class='dwdswb-ft-blocks']/div[@class='dwdswb-ft-block']");
                string otherData = null;

                WordType wt = WordType.All;
                foreach (var o in other)
                {
                    var spans = o.SelectNodes("./span");
                    if (spans != null && spans[0].InnerText.PonsNormalize().ToLowerInvariant() == "grammatik")
                    {
                        var t = spans[1].SelectSingleNode("./span").InnerText.PonsNormalize();
                        wt = GetWordType(t);
                        if (wt == WordType.Adj)
                        {

                            otherData = string.Join(" ",
                                    spans[1].ChildNodes.Where(n => n.Name != "span").Select(n => n.InnerText))
                                .PonsNormalize(); 
                        }
                        break;
                    }
                }


                //if (_type != WordType.All &&  wt != _type)
                //{
                //    continue;
                //}

                var items = GetTranslationItems(artikelNode, _word, wt, otherData);

                list.Add(new DWDSItem
                {
                    De = _word,
                    InnenItems = items,
                    Type = wt,
                    OtherData = otherData
                });
            }

            var result = list.Where(l => l.Type == _type).ToArray();

            return result.Any() ? (IList<DWDSItem>)result : list;
        }

        private WordType GetWordType(string value)
        {
            var type = value.ToLowerInvariant();
            
            switch (type)
            {
                case string s when s.Contains("substantiv") :
                    return WordType.Subst;
                case string s when s.StartsWith("verb") :
                    return WordType.Verb;
                case string s when s.Contains("adjektiv") :
                    return WordType.Adj;
                case string s when s.StartsWith("konjunktion") :
                    return WordType.Konj;
                case string s when s.StartsWith("adverb") :
                    return WordType.Adv;
                case string s when s.Contains("präposition") :
                    return WordType.Prep;
                case string s when s.Contains("pronomen") :
                    return WordType.Pron;

                default:
                    return WordType.All;
            }
        }

        private List<DWDSItem> GetTranslationItems(HtmlNode mainNode, string word, WordType wt, string otherData)
        {
            
            
            
            var descriptionNodes = mainNode.SelectNodes(".//div[@data-content-piece='Bedeutungsteil']/div[@class='dwdswb-lesart']");
            var res = new List<DWDSItem>();

            if (descriptionNodes != null)
            {
                foreach (var node in descriptionNodes)
                {
                    var item = GetDWDSItem(node, wt, otherData);
                    res.Add(item);
                }
            }

            return res;
        }

        private DWDSItem GetDWDSItem(HtmlNode node, WordType wt, string otherData)
        {
            var index = node.SelectSingleNode("./div[@class='dwdswb-lesart-n']").InnerText.PonsNormalize();

            var definitionNode = node.SelectSingleNode("./div[@class='dwdswb-lesart-content']/div[@class='dwdswb-lesart-def']");
            var defPreff = definitionNode.SelectSingleNode(".//span[@class='dwdswb-diasystematik']")?.InnerText.PonsNormalize();
            if (string.IsNullOrEmpty(defPreff))
            {
                defPreff = definitionNode.SelectSingleNode(".//span[@class='dwdswb-syntagmatik']")?.InnerText.PonsNormalize();
            }

            var definition = definitionNode.SelectSingleNode(".//span[@class='dwdswb-definitionen']")?.InnerText.PonsNormalize();
            
            var definitionGrammatik = node.SelectSingleNode("./div[@class='dwdswb-lesart-content']/div[@class='dwdswb-ft-la']")?.InnerText.PonsNormalize();
            
            
            var examples = node.SelectNodes("./div[@class='dwdswb-lesart-content']/div[@class='dwdswb-verwendungsbeispiele']//span[@class='dwdswb-belegtext']")?
                .Select(i => i.InnerText.PonsNormalize()).ToArray() ?? Array.Empty<string>();

            var res = new DWDSItem
            {
                De = _word,
                Index = index,
                Definition = GetDefinition(defPreff, definition, definitionGrammatik, otherData),
                Example = string.Join("; ", examples),
                InnenItems = new List<DWDSItem>(),
                Type = wt
            };

            var innen = node.SelectNodes(".//div[@class='dwdswb-lesart']");
            if (innen != null)
            {
                foreach (var node2 in innen)
                {
                    var item = GetDWDSItem(node2, wt, otherData);
                    res.InnenItems.Add(item);
                }
            }

            return res;
        }

        private string GetDefinition(string defPreff, string definition, string definitionGrammatik, string otherData)
        {
            if (string.IsNullOrEmpty(defPreff) && string.IsNullOrEmpty(definition) && string.IsNullOrEmpty(definitionGrammatik))
            {
                return "";
            }

            if (!string.IsNullOrEmpty(definition))
            {
                var tr = new PonsDeTranslationContainerFactory(definition, _type).GetTranslation();

                if (!string.IsNullOrEmpty(tr))
                {
                    definition = $"{definition} [Tr]: {tr}";
                }
            }

            if (!string.IsNullOrEmpty(defPreff))
            {
                defPreff = defPreff.Replace("⟨", "").Replace("⟩", "");

                var tr = new PonsDeTranslationContainerFactory(defPreff, _type).GetTranslation();

                if (!string.IsNullOrEmpty(tr))
                {
                    defPreff = $"{defPreff} [Tr]: {tr}";
                }
            }

            return (string.IsNullOrEmpty(defPreff) ? "" : $"({defPreff}): ") + $"{definition} {definitionGrammatik}".Trim();
        }
    }
}