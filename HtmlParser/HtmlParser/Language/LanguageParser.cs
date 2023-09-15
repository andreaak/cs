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
        private static readonly ReplaceItem[] NORMALIZATION = { new ReplaceItem("ä", "!a") ,
            new ReplaceItem("ö", "!o"),
            new ReplaceItem("ü", "!u"),
            new ReplaceItem("ß", "!s")};
        
        protected bool _order;
        protected string _type;

        public LanguageParser(bool order, string type)
        {
            _order = order;
            _type = type;
        }

        protected void UploadSound(string hostUrl, string word, string ext, string parentPath)
        {
            try
            {
                string folder = parentPath + Normalize(word)[0];
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string path = $"{folder}\\{Normalize(word)}.{ext}";

                //if (!File.Exists(path))
                {
                    Console.WriteLine($"Upload sound {hostUrl}");

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

        protected TranslationContainer GetTranslationContainer(HtmlDocument document, string word)
        {
            return GetTranslationContainer(document, word, _type);
        }

        protected TranslationContainer GetTranslationContainer(HtmlDocument document, string word, string type)
        {
            var trNodes = GetTranslationItems(document, word);

            if (trNodes == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(type))
            {
                return new TranslationContainer
                {
                    Node = trNodes.FirstOrDefault(),
                    AllNodes = trNodes
                };
            }

            foreach (var node in trNodes)
            {
                var wordClass = node.GetWordClass();
                if (wordClass == type.ToLower())
                {
                    return new TranslationContainer
                    {
                        Node = node,
                        AllNodes = trNodes
                    };
                }
            }

            return new TranslationContainer
            {
                AllNodes = trNodes
            };
        }

        protected IEnumerable<TranslationItem> GetTranslationItems(HtmlDocument document, string word)
        {
            var trNodes = document.DocumentNode.SelectNodes(".//div[@rel]");
            if (trNodes != null)
            {
                foreach (var node in trNodes)
                {
                    var rel = node.Attributes.FirstOrDefault(a => a.Name == "rel");
                    if (rel?.Value.Trim().Replace("\u0301", "") == word.ToLower())
                    {
                        return trNodes.SelectMany(TranslationItem.Parse).ToArray();
                    }
                }
            }

            return null;
        }

        protected WordClass GetWord(TranslationContainer trContainer, string de)
        {
            var word = GetWord(trContainer.Node, de, trContainer.AllNodes);
            word.Ru = trContainer.Node.GetTranslation();
            return word;
        }

        private WordClass GetWord(TranslationItem deNode, string de, IEnumerable<TranslationItem> allNodes)
        {
            var wordClass = deNode.GetWordClass();

            WordClass word;

            switch (wordClass)
            {
                case "verb":
                    word = GetVerb(deNode, de, allNodes);
                    break;
                case "subst":
                    word = GetSubstantiv(deNode, de, allNodes);
                    break;
                default:
                    word = GetWordClass(deNode, de, allNodes);
                    break;
            }

            return word;
        }

        protected void UploadSound(TranslationItem trNode, string de)
        {
            //var soundNode = trNode.SelectSingleNode(".//div[@class='translations first']//dl");
            var soundNode = trNode.RomHeadNode;
            if (soundNode == null)
            {
                return;
            }
            string hostUrl = $"https://sounds.pons.com/audio_tts/de/{soundNode.Id}?target=mp3";
            UploadSound(hostUrl, de, "mp3", "D:\\Temp\\Sounds\\");
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
            word.DeTranscription = attributes.Phonetics;

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
                    attributes.Flexion = HttpUtility.HtmlDecode(span.InnerText.Trim());
                }
                else if (span.Attributes.Any(a => a.Value == "genus"))
                {
                    attributes.Genus = span.InnerText.Trim();
                    attributes.Artikle = GetArtikle(attributes.Genus);
                }
                else if (span.Attributes.Any(a => a.Value == "info"))
                {
                    attributes.Info = HttpUtility.HtmlDecode(span.InnerText.Trim());
                }
                else if (span.Attributes.Any(a => a.Value == "verbclass"))
                {
                    attributes.VerbClass = span.InnerText.Trim();
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

        private string Normalize(string word)
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
}