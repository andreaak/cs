using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using HtmlAgilityPack;

namespace HtmlParser
{
    public abstract class LinkParser : BaseParser
    {
        protected override HtmlDocument GetHtml(string url)
        {
            int repeat = 3;
            int timeout = 15000;
            while (repeat-- > 0)
            {
                Thread.Sleep(timeout);
                try
                {
                    var htmlText = ReadHtml(url);
                    return GetHtmlFromText(htmlText);
                }
                catch (Exception e)
                {
                }
            }

            return null;
        }

        private string ReadHtml(string urlAddress)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {

                Stream responseStream = response.GetResponseStream();
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                    responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

                StreamReader reader = null;
                if (string.IsNullOrWhiteSpace(response.CharacterSet))
                {
                    reader = new StreamReader(responseStream);
                }
                else
                {
                    string encoding = response.CharacterSet;
                    reader = new StreamReader(responseStream, Encoding.GetEncoding(encoding));
                }

                string html = reader.ReadToEnd();

                response.Close();
                responseStream.Close();

                return html;
            }
            return null;
        }

        protected override void ReplaceImages(string dir, HtmlNode t)
        {
            var images = t.SelectNodes(".//img[@src]");

            if (images != null)
            {
                string path = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + dir + Path.DirectorySeparatorChar + "images";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                foreach (HtmlNode img in images)
                {
                    try
                    {
                        var url = img.Attributes["rel"]?.Value;
                        if (string.IsNullOrEmpty(url))
                        {
                            url = img.Attributes["src"].Value;
                        }

                        if (!url.Contains("http"))
                        {
                            url = GetHost() + url;
                        }
                        var uri = new Uri(url);
                        var name = "images" + Path.DirectorySeparatorChar + DateTime.Now.Ticks + uri.Segments.LastOrDefault();
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(uri, name);
                        }

                        img.Attributes["src"].Value = name;
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }

        protected virtual string GetHost()
        {
            return "";
        }

        protected string Normalize(string str)
        {
            return str.Replace(":", "--");
        }
    }
}