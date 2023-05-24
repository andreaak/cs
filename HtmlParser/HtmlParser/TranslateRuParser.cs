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
    
    public class TranslateRuParser : LinkParser
    {
        public void Normalize()
        {
            string dir = @"D:\Projects\My\cs\HtmlParser\HtmlParser\bin\Debug";

            var files = new DirectoryInfo(dir).EnumerateFiles("*.html", SearchOption.AllDirectories).ToArray();

            foreach (var file in files)
            {
                var htmlText = File.ReadAllText(file.FullName, Encoding.GetEncoding(1251));
                var doc = GetHtmlFromText(htmlText);
                var grouped = doc.DocumentNode.SelectNodes(".//div[@class='innercontent']/a[@href]")
                    .GroupBy(link => link.Attributes["href"].Value).ToArray();

                var nodes = grouped
                    .Where(gr => gr.Count() > 1).ToArray();
                var indexes = grouped.Select(group => group.Key).Distinct().ToList();
                if (nodes.Length == 0)
                {
                    continue;
                }

                foreach (var group in nodes)
                {
                    var id = group.Key.Trim().TrimStart('#');

                    var headers = doc.DocumentNode.SelectNodes($".//*[@id={id}]")
                        .Where(n => n.Name.StartsWith("h", StringComparison.OrdinalIgnoreCase)).ToArray();
                    if (headers.Length != group.Count())
                    {

                    }

                    for (int i = 0; i < group.Count(); i++)
                    {
                        var link = group.ElementAt(i);
                        var linkText = link.InnerText.Trim();
                        foreach (var header in headers)
                        {
                            var headerText = header.InnerText.Trim();
                            if (linkText.Trim() == headerText.Trim())
                            {
                                int numericId = GetId(indexes);

                                link.Attributes["href"].Value = $"#{numericId}";
                                header.Attributes["id"].Value = $"{numericId}";
                                break;
                            }
                        }


                        //var header = headers[i];
                        //var link = group.ElementAt(i);
                        //var text = link.InnerText.Trim();
                        //var text2 = header.InnerText.Trim();
                        //if (text != text2)
                        //{

                        //}

                        //int numericId = GetId(indexes);

                        //link.Attributes["href"].Value = $"#{numericId}";
                        //header.Attributes["id"].Value = $"{numericId}";
                    }
                }

                doc.Save(file.DirectoryName + Path.DirectorySeparatorChar
                                            + file.Name.Substring(0, file.Name.IndexOf(file.Extension)) + "_converted"
                                            + file.Extension);
            }
        }

        public void Normalize2()
        {
            string dir = @"D:\Projects\My\cs\HtmlParser\HtmlParser\bin\Debug";

            var files = new DirectoryInfo(dir).EnumerateFiles("*.html", SearchOption.AllDirectories).ToArray();

            foreach (var file in files)
            {
                var htmlText = File.ReadAllText(file.FullName, Encoding.GetEncoding(1251));
                var doc = GetHtmlFromText(htmlText);
                var links = doc.DocumentNode.SelectNodes(".//div[@class='innercontent']/a[@href]").ToArray();

                var indexes = new List<string>();

                foreach (var link in links)
                {
                    var linkText = link.InnerText.Trim();
                    if ("Çàïèòàííÿ äëÿ ñàìîêîíòðîëþ" == linkText)
                    {
                        continue;
                    }
                    //var header = doc.DocumentNode.SelectNodes($".//*[contains(text(),'{linkText}')]");
                    var header = doc.DocumentNode.SelectNodes($".//*[normalize-space(text()) = '{linkText}']");
                    if (header == null || header.Count == 1)
                    {
                        continue;
                    }
                    int numericId = GetId(indexes);

                    link.Attributes["href"].Value = $"#{numericId}";
                    var h = header[1];
                    while (!h.Name.StartsWith("h"))
                    {
                        h = h.ParentNode;
                    }
                    h.Attributes["id"].Value = $"{numericId}";
                }
                doc.Save(file.DirectoryName + Path.DirectorySeparatorChar
                                            + file.Name.Substring(0, file.Name.IndexOf(file.Extension)) + "_converted"
                                            + file.Extension);
            }
        }


        private int GetId(IList<string> indexes)
        {
            int i = 1;
            while (true)
            {
                if (!indexes.Contains($"#{i}"))
                {
                    indexes.Add($"#{i}");
                    return i;
                }

                i++;
            }
        }

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

        private static readonly HttpClient client = new HttpClient();

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
                    using (Stream output = File.OpenWrite($"{folder}/{word.De}.wav"))
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

        class TranscriptionResponse
        {
            public string Text { get; set; }
        }

        protected override string GetHost()
        {
            return "https://www.translate.ru";
        }

        protected override string GetUrI(string dir, string url)
        {
            return url.Contains("http") ? url : GetHost() + url;
        }

        protected override HtmlNode GetContents(HtmlNode element)
        {
            return element.SelectSingleNode("//article");
        }

        protected override HtmlNode GetElement(HtmlNode element)
        {
            return element.SelectSingleNode("//article");
        }

        protected override void ProcessElement(HtmlNode element, HtmlNode body, string dir, string uri, string text)
        {
            if (element == null)
            {
                return;
            }
            var addedElement = AddElement(element, body);
            ReplaceImages(dir, addedElement);

            //Save(element, text);
        }

        private void Save(HtmlNode element, string uri)
        {
            var newDocument = CreateHtml().OwnerDocument;
            AddElement(element, newDocument.DocumentNode);
            newDocument.Save(Normalize(uri) + ".html", Encoding.UTF8);
        }

        protected override HtmlNode CreateHtml()
        {
            var newDocument = new HtmlDocument();
            var html = newDocument.DocumentNode.AppendChild(newDocument.CreateElement("html"));
            var head = html.AppendChild(newDocument.CreateElement("head"));
            head.InnerHtml = "<link rel=\"stylesheet\" type=\"text/css\" href=\"template_css.css\">";
            var body = html.AppendChild(newDocument.CreateElement("body"));
            return body;
        }
    }
}