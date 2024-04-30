using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using HtmlAgilityPack;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{

    internal interface ILanguageParser
    {
        void Parse(IList<string> lines);
    }

    public class LanguageParser : HtmlParser
    {
        private const string SoundsFolder = "D:\\Temp\\Deutch\\DeutschAndroid\\Sounds\\de";

        private static readonly ReplaceItem[] NORMALIZATION = { new ReplaceItem("ä", "!a") ,
            new ReplaceItem("ö", "!o"),
            new ReplaceItem("ü", "!u"),
            new ReplaceItem("ß", "!s")};
        
        protected bool _order;
        protected WordType _type;

        public LanguageParser(bool order, WordType type)
        {
            _order = order;
            _type = type;
        }

        protected void UploadSound(string hostUrl, string word, string ext, string parentPath)
        {
            try
            {
                var normalized = Normalize(word);
                string folder = parentPath + "\\" + (normalized.StartsWith("!") ? normalized.Substring(0, 2) : normalized.Substring(0, 1));
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string path = $"{folder}\\{Normalize(word)}.{ext}";

                //if (!File.Exists(path))
                {
                    //Console.WriteLine($"Upload sound {hostUrl}");

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hostUrl);
                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                    if (resp.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream output = File.OpenWrite(path))
                        using (Stream input = resp.GetResponseStream())
                        {
                            if (input != null)
                            {
                                input.CopyTo(output);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected IList<HtmlNode> GetVerbFormsContainer(HtmlDocument document)
        {
            return document?.DocumentNode.SelectNodes(".//div[@class='ft-single-table']");
        }

        protected IList<TranslationContainer> GetTranslationContainer(HtmlDocument document, string word)
        {
            return GetTranslationContainer(document, word, _type);
        }

        protected IList<TranslationContainer> GetTranslationContainer(HtmlDocument document, string word, WordType type)
        {
            var trNodes = GetTranslationItems(document, word);

            if (trNodes == null)
            {
                return null;
            }

            if (type == WordType.Other)
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

        protected IEnumerable<TranslationItem> GetTranslationItems(HtmlDocument document, string word) {
            var trNodes = document.DocumentNode.SelectNodes(".//div[@rel]");

            var resNodes = new List<HtmlNode>();
            
            if (trNodes != null)
            {
                foreach (var node in trNodes)
                {
                    var rel = node.Attributes.FirstOrDefault(a => a.Name == "rel");
                    var value = rel?.Value.Trim().Replace("\u0301", "").ToLower();
                    var sourceWord = word.ToLower();

                    if (value != null && (value == sourceWord || value.IndexOf(sourceWord) == 0  && value.Length > sourceWord.Length && value[sourceWord.Length] == '('))
                    {
                        resNodes.Add(node);
                    }
                }
            }

            return resNodes.Count != 0 ? resNodes.SelectMany(n => TranslationItem.Parse(n, word)).ToArray() : null;
        }

        protected IList<WordClass> GetWords(IList<TranslationContainer> trContainers, string de)
        {
            return trContainers.Select(trContainer =>
            {
                var node = trContainer.Node;
                if (node == null)
                {
                    return new WordClass
                    {
                        De = de
                    };
                }

                var word = GetWord(trContainer, de);
                if (trContainer.Node != null)
                {
                    word.Ru = string.Join("/", trContainer.Node.GetTranslations(de).Distinct());
                }
                return word;
            }).ToArray();
        }

        private WordClass GetWord(TranslationContainer trContainer, string de)
        {
            var node = trContainer.Node;
            var allNodes = trContainer.AllNodes;
            
            var wordClass = node.GetWordClass();

            WordClass word;

            switch (wordClass)
            {
                case "verb":
                    word = GetVerb(node, de, allNodes);
                    break;
                case "subst":
                    word = GetSubstantiv(node, de, allNodes);
                    break;
                default:
                    word = GetWordClass(node, de, allNodes);
                    break;
            }

            return word;
        }

        protected void UploadSound(TranslationItem trNode, string de)
        {
            //var soundNode = trNode.SelectSingleNode(".//div[@class='translations first']//dl");
            var soundNode = trNode?.RomHeadNode;
            if (soundNode == null)
            {
                return;
            }
            string hostUrl = $"https://sounds.pons.com/audio_tts/de/{soundNode.Id}?target=mp3";
            UploadSound(hostUrl, de, "mp3", SoundsFolder);
        }
        
        protected IrrVerb GetIrregularVerb(TranslationItem trItem, string de, string part2Verb)
        {
            var word = new IrrVerb
            {
                De = de,
                Part2Verb = part2Verb
            };

            var attributes = GetAttributes(trItem);
            if (attributes == null)
            {
                return word;
            }

            word.WrdClass = trItem.GetWordClass();
            word.Info = attributes.Info;
            word.DeTranscription = attributes.Phonetics;
            word.VerbClass = attributes.VerbClass;

            return word;
        }

        //private string GetTranscription(string language, string word)// lang = "deu"
        //{
        //    var hostUrl = "https://www.artlebedev.ru/tools/transcriptor/ajax.html";
        //    var values = new Dictionary<string, string>
        //    {
        //        { "lang", language },
        //        { "text", word }
        //    };

        //    var content = new FormUrlEncodedContent(values);
        //    HttpClient client = new HttpClient();
        //    var response = client.PostAsync(hostUrl, content).Result;

        //    var responseString = response.Content.ReadAsStringAsync().Result;

        //    TranscriptionResponse deptObj = JsonSerializer.Deserialize<TranscriptionResponse>(responseString, new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    });

        //    return deptObj.Text;
        //}

        protected Verb GetVerb(TranslationItem trItem, string de, IEnumerable<TranslationItem> allItems)
        {
            var word = new Verb
            {
                De = de
            };

            var attributes = GetAttributes(trItem);
            if (attributes == null)
            {
                return word;
            }

            word.WrdClass = trItem.GetWordClass();
            word.DeTranscription = attributes.Phonetics;
            word.Info = attributes.Info;
            word.VerbClass = attributes.VerbClass;
            if (!string.IsNullOrEmpty(attributes.HeadWord) && attributes.HeadWord.Contains("|") )
            {
                word.De = attributes.HeadWord;
            }
            if (string.IsNullOrEmpty(word.DeTranscription))
            {
                word.DeTranscription = GetTranscription(trItem, allItems);
            }
            return word;
        }

        private WordClass GetWordClass(TranslationItem trItem, string de, IEnumerable<TranslationItem> allItems)
        {
            var word = new WordClass
            {
                De = de
            };

            var attributes = GetAttributes(trItem);
            if (attributes == null)
            {
                return word;
            }

            word.WrdClass = trItem.GetWordClass();
            word.DeTranscription = attributes.Phonetics.RemoveNewLine();

            if (string.IsNullOrEmpty(word.DeTranscription))
            {
                word.DeTranscription = GetTranscription(trItem, allItems);
            }
            return word;
        }

        private Substantiv GetSubstantiv(TranslationItem trItem, string de, IEnumerable<TranslationItem> allItems)
        {
            var word = new Substantiv
            {
                De = de
            };

            var attributes = GetAttributes(trItem);
            if (attributes == null)
            {
                return word;
            }

            word.WrdClass = trItem.GetWordClass();
            word.DeTranscription = attributes.Phonetics;
            word.Flexion = attributes.Flexion;
            word.Genus = attributes.Genus;
            word.Artikle = attributes.Artikle;

            if (string.IsNullOrEmpty(word.DeTranscription))
            {
                word.DeTranscription = GetTranscription(trItem, allItems);
            }
            return word;
        }

        private TranslationAttributes GetAttributes(TranslationItem trItem)
        {
            var nodes = trItem.RomHeadNode.SelectNodes(".//span")?.ToArray() ?? Array.Empty<HtmlNode>();

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

        protected static string GetArtikle(string genus)
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

        protected string GetTranscription(TranslationItem trNode, IEnumerable<TranslationItem> allNodes)
        {
            if (allNodes == null)
            {
                return null;
            }

            foreach (var node in allNodes.Where(node => node.RomHeadNode.Id != trNode?.RomHeadNode?.Id))
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

        protected string Normalize(string word)
        {
            word = word.ToLower();
            foreach (var symbol in NORMALIZATION)
            {
                if (word.Contains(symbol.Source))
                {
                    word = word.Replace(symbol.Source, symbol.Dest);
                }
            }
            return word;
        }
    }

    public enum WordType
    {
        Other = 0,
        Verb,
        Subst,
        Adv, 
        Adj,
        Konj,
        All
    }
}