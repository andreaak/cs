using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Web;
using HtmlAgilityPack;

namespace HtmlParser.Language
{
    public class LanguageParser : HtmlParser
    {
        private static readonly ReplaceItem[] NORMALIZATION = { new ReplaceItem("ä", "!a") ,
            new ReplaceItem("ö", "!o"),
            new ReplaceItem("ü", "!u"),
            new ReplaceItem("ß", "!s")};


        protected void UploadSound(string hostUrl, string word, string ext, string parentPath)
        {
            try
            {
                string folder = parentPath + word.ToLower()[0];
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string path = $"{folder}/{Normalize(word)}.{ext}";

                if (!File.Exists(path))
                {
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

        protected WordClass GetWordClass(HtmlNode trNode, string de)
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

            return word;
        }

        protected Substantiv GetSubstantiv(HtmlNode trNode, string de)
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
                }
            }

            return word;
        }

        protected Verb GetVerb(HtmlNode trNode, string de)
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

            return word;
        }
        
        protected IrrVerb GetVerb(HtmlNode trNode, string de, string part2Verb)
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
    }
}