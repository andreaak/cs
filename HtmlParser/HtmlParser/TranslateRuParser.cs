using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;
using HtmlAgilityPack;

namespace HtmlParser
{
    public class Word
    {
        public string Ru { get; set; }
        public string De { get; set; }
        public string DeArtikle { get; set; }
        public string DeTranscription { get; set; }
    }

    class TranscriptionResponse
    {
        public string Text { get; set; }
    }

    public class TranslateRuParser : LinkParser
    {
        private static readonly ReplaceItem[] NORMALIZATION = { new ReplaceItem("ä", "!a") ,
            new ReplaceItem("ö", "!o"),
            new ReplaceItem("ü", "!u")};

        private static readonly HttpClient client = new HttpClient();

        public void Parse()
        {
            string file = "list.txt";

            var lines = File.ReadLines(file);

            var list = lines.Select(l => new Word
            {
                Ru = l.Trim().ToLower()
            }).ToList();

            foreach (var item in list)
            {
                Parse(item);
            }

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    sw.WriteLine($"{item.Ru}");
                    sw.WriteLine($"{item.DeArtikle} {item.De}");
                    sw.WriteLine($"[{item.DeTranscription}]");
                }
            }
        }

        protected void Parse(Word word)
        {
            var hostUrl = "https://www.translate.ru/перевод/русский-немецкий/";


            var document = GetHtml(hostUrl + word.Ru);

            var trNode = document.DocumentNode.SelectSingleNode(".//div[@class='translation-item']").SelectSingleNode(".//a");
            foreach (var span in trNode.SelectNodes(".//span"))
            {
                if (span.HasAttributes && span.Attributes.Any(a => a.Value == "result_only sayWord"))
                {
                    word.De = span.InnerText.Trim();
                }
                else
                {
                    word.DeArtikle = span.InnerText.Trim();
                }
            }

            hostUrl = "https://www.artlebedev.ru/tools/transcriptor/ajax.html";
            var values = new Dictionary<string, string>
            {
                { "lang", "deu" },
                { "text", $"{word.De}" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = client.PostAsync(hostUrl, content).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            TranscriptionResponse deptObj = JsonSerializer.Deserialize<TranscriptionResponse>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            word.DeTranscription = deptObj.Text;
            
            hostUrl = $"https://api.lingvolive.com/sounds?uri=Universal%20(De-Ru)%2F{word.De}.wav";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hostUrl);
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string folder = word.De.ToLower()[0].ToString();
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    using (Stream output = File.OpenWrite($"{folder}/{Normalize(word.De)}.wav"))
                    using (Stream input = resp.GetResponseStream())
                    {
                        if (input != null)
                        {
                            input.CopyTo(output);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }



        protected override string GetHost()
        {
            return "https://www.translate.ru";
        }

        protected override HtmlNode GetContents(HtmlNode element)
        {
            throw new NotImplementedException();
        }

        protected override HtmlNode GetElement(HtmlNode element)
        {
            throw new NotImplementedException();
        }

        protected override string GetUrI(string dir, string url)
        {
            return url.Contains("http") ? url : GetHost() + url;
        }



        private new string Normalize(string word)
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
    class ReplaceItem
    {
        public string Source;
        public string Dest;

        public ReplaceItem(string source, string dest)
        {
            Source = source;
            Dest = dest;
        }
    }
}