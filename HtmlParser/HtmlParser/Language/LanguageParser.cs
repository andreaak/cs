using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
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

        protected TranslationNode GetTranslationNode(HtmlDocument document, string word)
        {
            var trNodes = document.DocumentNode.SelectNodes($".//div[translate(@rel,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz') = '{word.ToLower()}']");

            if (trNodes == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(_type))
            {
                return new TranslationNode
                {
                    Node = trNodes.FirstOrDefault(),
                    AllNodes = trNodes
                };
            }

            foreach (var node in trNodes)
            {
                var wordClass = node.SelectSingleNode(".//span[@class='wordclass']")?.InnerText.Trim().ToLower();
                if (wordClass == _type.ToLower())
                {
                    return new TranslationNode
                    {
                        Node = node,
                        AllNodes = trNodes
                    };
                }
            }

            return new TranslationNode
            {
                AllNodes = trNodes
            };
        }

        protected WordClass GetWord(TranslationNode trNode, string de)
        {
            var deNode = trNode.Node.SelectSingleNode(".//div[@class='romhead']");

            var word = GetWord(deNode, de, trNode.AllNodes);

            var ruNode = trNode.Node.SelectSingleNode(".//div[@class='translations first']//div[@class='target']//a");
            word.Ru = ruNode.InnerText.Trim().Replace("\u0341", "");
            return word;
        }

        private WordClass GetWord(HtmlNode deNode, string de, IEnumerable<HtmlNode> allNodes)
        {
            var wordClass = deNode.SelectSingleNode(".//span[@class='wordclass']").InnerText.Trim().ToLower();

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

        protected void UploadSound(HtmlNode trNode, string de)
        {
            //var soundNode = trNode.SelectSingleNode(".//div[@class='translations first']//dl");
            var soundNode = trNode.SelectSingleNode(".//div[@class='romhead']");
            if (soundNode == null)
            {
                return;
            }
            string hostUrl = $"https://sounds.pons.com/audio_tts/de/{soundNode.Id}?target=mp3";
            UploadSound(hostUrl, de, "mp3", "D:\\Temp\\Sounds\\");
        }
        
        protected IrrVerb GetIrregularVerb(HtmlNode trNode, string de, string part2Verb)
        {
            var word = new IrrVerb
            {
                De = de,
                Part2Verb = part2Verb
            };

            foreach (var span in trNode.SelectNodes(".//span"))
            {
                if (span.HasAttributes && span.Attributes.Any(a => a.Value == "info"))
                {
                    word.Info = HttpUtility.HtmlDecode(span.InnerText.Trim());
                }
                else if (span.HasAttributes && span.Attributes.Any(a => a.Value == "phonetics"))
                {
                    word.DeTranscription = span.InnerText.Trim();
                }
                else if (span.HasAttributes && span.Attributes.Any(a => a.Value == "verbclass"))
                {
                    word.VerbClass = span.InnerText.Trim();
                }
            }

            return word;
        }

        private string GetTranscription(string language, string word)// lang = "deu"
        {
            var hostUrl = "https://www.artlebedev.ru/tools/transcriptor/ajax.html";
            var values = new Dictionary<string, string>
            {
                { "lang", language },
                { "text", word }
            };

            var content = new FormUrlEncodedContent(values);
            HttpClient client = new HttpClient();
            var response = client.PostAsync(hostUrl, content).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            TranscriptionResponse deptObj = JsonSerializer.Deserialize<TranscriptionResponse>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return deptObj.Text;
        }

        protected Verb GetVerb(HtmlNode trNode, string de, IEnumerable<HtmlNode> allNodes)
        {
            var word = new Verb
            {
                De = de
            };

            var nodes = trNode.SelectNodes(".//span");

            if (nodes == null)
            {
                return word;
            }

            foreach (var span in nodes)
            {
                if (span.HasAttributes && span.Attributes.Any(a => a.Value == "info"))
                {
                    word.Info = HttpUtility.HtmlDecode(span.InnerText.Trim());
                }
                else if (span.HasAttributes && span.Attributes.Any(a => a.Value == "phonetics"))
                {
                    word.DeTranscription = span.InnerText.Trim();
                }
                else if (span.HasAttributes && span.Attributes.Any(a => a.Value == "verbclass"))
                {
                    word.VerbClass = span.InnerText.Trim();
                }
            }
            if (string.IsNullOrEmpty(word.DeTranscription))
            {
                word.DeTranscription = GetTranscription(trNode, allNodes);
            }
            return word;
        }

        private WordClass GetWordClass(HtmlNode trNode, string de, IEnumerable<HtmlNode> allNodes)
        {
            var word = new WordClass
            {
                De = de
            };

            foreach (var span in trNode.SelectNodes(".//span"))
            {
                if (span.HasAttributes && span.Attributes.Any(a => a.Value == "phonetics"))
                {
                    word.DeTranscription = span.InnerText.Trim();
                }
            }

            if (string.IsNullOrEmpty(word.DeTranscription))
            {
                word.DeTranscription = GetTranscription(trNode, allNodes);
            }
            return word;
        }

        private Substantiv GetSubstantiv(HtmlNode trNode, string de, IEnumerable<HtmlNode> allNodes)
        {
            var word = new Substantiv
            {
                De = de
            };

            foreach (var span in trNode.SelectNodes(".//span"))
            {
                if (span.HasAttributes && span.Attributes.Any(a => a.Value == "flexion"))
                {
                    word.Flexion = HttpUtility.HtmlDecode(span.InnerText.Trim());
                }
                else if (span.HasAttributes && span.Attributes.Any(a => a.Value == "phonetics"))
                {
                    word.DeTranscription = span.InnerText.Trim();
                }
                else if (span.HasAttributes && span.Attributes.Any(a => a.Value == "genus"))
                {
                    word.Genus = span.InnerText.Trim();

                    switch (word.Genus.ToLower())
                    {
                        case "m":
                            word.Artikle = "der";
                            break;
                        case "f":
                            word.Artikle = "die";
                            break;
                        case "nt":
                            word.Artikle = "das";
                            break;
                    }
                }
            }
            if (string.IsNullOrEmpty(word.DeTranscription))
            {
                word.DeTranscription = GetTranscription(trNode, allNodes);
            }
            return word;
        }

        private string GetTranscription(HtmlNode trNode, IEnumerable<HtmlNode> allNodes)
        {
            if (allNodes == null)
            {
                return null;
            }

            foreach (var node in allNodes.Where(node => node.Id != trNode.Id))
            {
                foreach (var span in node.SelectNodes(".//span"))
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

    public class TranslationNode
    {
        public HtmlNode Node { get; set; }
        public IEnumerable<HtmlNode>  AllNodes { get; set; }
    }
}