using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.Containers
{
    public abstract class BasePonsTranslationContainerFactory
    {
        protected readonly string _word;
        protected readonly WordType _type;
        protected IList<TranslationContainer> _containers;

        protected abstract string HostUrl { get; }
        protected abstract string HostUrl2 { get; }
        
        protected abstract WordType GetWordType(string wordClass);

        protected BasePonsTranslationContainerFactory(string de, WordType type)
        {
            _word = de;
            _type = type;
        }

        public IList<WordClass> GetWords(string separator = "/")
        {
            var trContainers = GetTranslationContainer();

            if (trContainers == null || trContainers.Count == 0)
            {
                Console.WriteLine($"Not found {_word}");
               
                var wordsNotFound = new List<WordClass>()
                {
                    new WordClass
                    {
                        De = _word,
                        Found = false,
                        WrdClass = _type.ToString().ToLower()
                    }
                };
                return wordsNotFound;
            }

            _containers = trContainers;
            return GetWords(trContainers, _word, dei => new WordClass
            {
                De = dei,
                
            }, separator);
        }

        protected IList<WordClass> GetWords(IList<TranslationContainer> trContainers, string de, Func<string, WordClass> emptyCreator, string separator = "/")
        {
            return trContainers.Select(trContainer =>
            {
                var node = trContainer.Node;
                if (node == null)
                {
                    return emptyCreator(de);
                }

                var word = GetWord(trContainer, de);
                if (trContainer.Node != null)
                {
                    word.Ru = GetWordRu(de, separator, trContainer);
                }
                return word;
            }).Where(w => w != null).ToArray();
        }

        protected virtual string GetWordRu(string de, string separator, TranslationContainer trContainer)
        {
            return string.Join(separator, trContainer.Node.GetTranslations(de).Distinct())
                .Replace(" m ", "")
                .Replace(" f", "")
                .Replace(" nt ", "")
                .Replace(" m/", "/")
                .Replace(" f/", "/")
                .Replace(" nt/", "/");

        }

        protected string GetRequestUrl(string soundNode, string link, string reserveLink)
        {
            if (soundNode != null)
            {
                return link;
            }
            return string.IsNullOrEmpty(reserveLink) ? null : reserveLink;
        }

        private WordClass GetWord(TranslationContainer trContainer, string de)
        {
            var node = trContainer.Node;
            var allNodes = trContainer.AllNodes;

            var wordClass = node.Attributes.WordClass;

            WordClass word;

            switch (wordClass)
            {
                case "verb":
                case "vb":
                    word = GetVerb(node, de, allNodes);
                    break;
                case "subst":
                case "n":
                case "noun":
                    word = GetSubstantiv(node, de, allNodes);
                    break;
                default:
                    word = GetWordClass(node, de, allNodes);
                    break;
            }

            

            return word;
        }

        private Verb GetVerb(ITranslationItem trItem, string de, IEnumerable<ITranslationItem> allItems)
        {
            var word = new Verb
            {
                De = de
            };

            var attributes = trItem.Attributes;
            if (attributes == null)
            {
                return word;
            }

            word.WrdClass = trItem.Attributes.WordClass;
            word.DeTranscription = attributes.Phonetics;
            word.Info = attributes.Info;
            word.VerbClass = attributes.VerbClass;
            if (!string.IsNullOrEmpty(attributes.HeadWord) && attributes.HeadWord.Contains("|"))
            {
                word.De = attributes.HeadWord;
            }
            if (string.IsNullOrEmpty(word.DeTranscription))
            {
                word.DeTranscription = trItem.GetTranscription(allItems);
            }

            word.Found = true;
            return word;
        }

        private WordClass GetWordClass(ITranslationItem trItem, string de, IEnumerable<ITranslationItem> allItems)
        {
            var word = new WordClass
            {
                De = de
            };

            var attributes = trItem.Attributes;
            if (attributes == null)
            {
                return word;
            }

            word.WrdClass = trItem.Attributes.WordClass;
            word.DeTranscription = attributes.Phonetics.RemoveNewLine();
            word.Info = attributes.Info;

            if (string.IsNullOrEmpty(word.DeTranscription))
            {
                word.DeTranscription = trItem.GetTranscription(allItems);
            }
            word.Found = true;
            return word;
        }

        private Substantiv GetSubstantiv(ITranslationItem trItem, string de, IEnumerable<ITranslationItem> allItems)
        {
            var word = GetSubstantivItem(de);

            var attributes = trItem.Attributes;
            if (attributes == null)
            {
                return word;
            }

            word.WrdClass = trItem.Attributes.WordClass;
            word.DeTranscription = attributes.Phonetics;
            word.Flexion = attributes.Flexion;
            word.Genus = attributes.Genus;
            word.Artikle = attributes.Artikle;
            word.Info = attributes.Info;

            if (string.IsNullOrEmpty(word.DeTranscription))
            {
                word.DeTranscription = trItem.GetTranscription(allItems);
            }
            word.Found = true;
            return word;
        }

        protected virtual Substantiv GetSubstantivItem(string de)
        {
            var word = new Substantiv
            {
                De = de[0].ToString().ToUpper() + de.Substring(1)
            };
            return word;
        }

        protected IList<TranslationContainer> GetTranslationContainer()
        {
            var document = new HtmlParser().GetHtml(HostUrl + "/" + _word);
            if (document == null)
            {
                return null;
            }
            var trNodes = GetTranslationItems(document, _word);

            
            
            if (trNodes == null)
            {
                return null;
            }

            if (_type == WordType.None)
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
                var wordClass = node.Attributes.WordClass;
                var wordType = GetWordType(wordClass);
                if (wordType == _type || _type == WordType.All || wordType == WordType.Complex)
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

        private string GetTranslation(HtmlDocument document)
        {
            var trNode = document.DocumentNode?.SelectSingleNode(".//div[@class='editor target-editor-container']");
            if (trNode != null)
            {
                return "---" + trNode.InnerText.Trim();
            }

            return null;
        }

        //private IEnumerable<ITranslationItem> GetTranslationItems(HtmlDocument document, string word)
        //{
        //    var trNodes = document.DocumentNode?.SelectNodes(".//div[@rel]");

        //    var resNodes = new List<HtmlNode>();

        //    if (trNodes != null)
        //    {
        //        foreach (var node in trNodes)
        //        {
        //            var rel = node.Attributes.FirstOrDefault(a => a.Name == "rel");
        //            var value = rel?.Value.Trim().Replace("\u0301", "").ToLower();
        //            var sourceWord = word.ToLower();

        //            if (value != null && (value == sourceWord || value.IndexOf(sourceWord) == 0 && value.Length > sourceWord.Length && value[sourceWord.Length] == '('))
        //            {
        //                resNodes.Add(node);
        //            }
        //        }
        //    }

        //    return resNodes.Count != 0 ? resNodes.SelectMany(Parse).ToArray() : null;
        //}

        private IEnumerable<ITranslationItem> GetTranslationItems(HtmlDocument document, string word)
        {
            if (document.DocumentNode == null)
            {
                return null;
            }
            
            
            var notFoundNode = document.DocumentNode.SelectSingleNode(".//div[@data-e2e='translation-result-suggestions']");
            if (notFoundNode != null)
            {
                return null;
            }

            var trNode = document.DocumentNode.SelectSingleNode(".//div[@id='dictionary']");
            if (trNode == null)
            {
                return null;
            }

            var resNodes = new List<HtmlNode>();
            var trNodes = trNode.SelectNodes(".//div[@class='bg-background flex max-h-full max-w-full flex-col']");
            if (trNodes != null)
            {
                return Parse(trNodes);
            }
            else
            {
                
                var document2 = new HtmlParser().GetHtml(HostUrl2 + word);
                if (document2 != null)
                {
                    var text = document2.DocumentNode.SelectSingleNode(".//div[@name='targetText']").InnerText.PonsNormalize();
                    if (!string.IsNullOrEmpty(text))
                    {
                        var item = new PonsItem(word);
                        item.Attributes = new TranslationAttributes()
                        {
                            WordClass = "complex"
                        };
                        item.TranslationItems = new List<TranslationComplexItem>()
                        {
                            new TranslationComplexItem()
                            {
                                Values = new List<TranslationSimpleItem>()
                                {
                                    new TranslationSimpleItem()
                                    {
                                        Source = word,
                                        Target = text
                                    }
                                }
                            }
                        };

                        return new [] { item };
                    }

                }
            }

            return null;
        }

        protected virtual IEnumerable<PonsItem> Parse(IList<HtmlNode> nodes)
        {
            return nodes.Select(i => PonsItem.ParseNode(i, _word)).ToArray();
        }
    }
}