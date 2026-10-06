using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using HtmlAgilityPack;

namespace HtmlParser.Language.HTMLClients
{
    public class HtmlReader
    {
        public HtmlDocument GetHtml(string url, string cookie = null, IHtmlClient htmlClient = null, bool isRepeat = true)
        {
            int repeat = 3;
            int timeout = 2000;
            while (repeat-- > 0)
            {
                try
                {
                    if (htmlClient == null)
                    {
                        htmlClient = new DefaultHtmlClient();
                    }
                    
                    var htmlText = htmlClient.ReadHtml(url, cookie);
                    return GetHtmlFromText(htmlText);
                }
                catch (Exception e)
                {
                    Console.WriteLine(url);
                    Console.WriteLine(e);
                    if (!isRepeat)
                    {
                        break;
                    }

                    if (e.Message.Contains("The remote server returned an error: (429)"))
                    {
                        //Thread.Sleep(600000);
                    }
                    else
                    {
                        Thread.Sleep(timeout);
                    }
                }
            }

            return null;
        }

        public static string ReadHtml2(string url, string cookie)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var cookies = new CookieContainer();

            using (var handler = new HttpClientHandler())
            {
                handler.CookieContainer = cookies;
                handler.UseCookies = true;
                handler.AllowAutoRedirect = true;
                handler.AutomaticDecompression =
                    DecompressionMethods.GZip |
                    DecompressionMethods.Deflate;

                using (var client = new HttpClient(handler))
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "User-Agent",
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                        "AppleWebKit/537.36 (KHTML, like Gecko) " +
                        "Chrome/126.0.0.0 Safari/537.36");

                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "Accept",
                        "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "Accept-Language",
                        "ru-RU,ru;q=0.9,de;q=0.8,en;q=0.7");

                    //var url = "https://dict.leo.org/russisch-deutsch/" + Uri.EscapeDataString(word);

                    HttpResponseMessage response = client.GetAsync(url).Result;

                    string body = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(
                            $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}\r\n\r\n{body}");
                    }

                    return body;
                }
            }
        }

        

        private string ReadHtml3(string urlAddress, string cookie = null)
        {
            var uri = new Uri(urlAddress);
            var request = (HttpWebRequest)WebRequest.Create(uri);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            //request.ContentType = "text/html;charset=UTF-8";
            //request.AllowAutoRedirect = false;

            if (!string.IsNullOrEmpty(cookie))
            {
                request.Headers["cookie"] = cookie;
            }




            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //Console.WriteLine(response.StatusCode);
            //var location = response.Headers["Location"];
            //Console.WriteLine(location);
            //foreach (char c in location)
            //    Console.WriteLine($"{c} U+{(int)c:X4}");

            if (response.StatusCode == HttpStatusCode.OK)
            {

                Stream responseStream = response.GetResponseStream();
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                    responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

                StreamReader reader = null;
                if (string.IsNullOrWhiteSpace(response.CharacterSet) || response.CharacterSet == "ISO-8859-1")
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


        //private string ReadHtml1(string urlAddress, string cookie = null)
        //{
        //    var request = (HttpWebRequest)WebRequest.Create(urlAddress);
        //    request.Method = "GET";
        //    request.AllowAutoRedirect = false;
        //    request.ContentType = "text/html;charset=UTF-8";
        //    request.UserAgent = "Mozilla/5.0";
        //    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

        //    if (!string.IsNullOrEmpty(cookie))
        //        request.Headers["Cookie"] = cookie;

        //    using (var response = (HttpWebResponse)request.GetResponse())
        //    {
        //        int status = (int)response.StatusCode;
        //        string location = response.Headers["Location"];

        //        if (status == 301 || status == 302 || status == 303 || status == 307 || status == 308)
        //        {
        //            if (!string.IsNullOrEmpty(location))
        //            {
        //                //var redirectUri = new Uri(new Uri(urlAddress), location);
        //                //return ReadHtml(redirectUri.AbsoluteUri, cookie);



        //                if (!string.IsNullOrEmpty(location))
        //                {
        //                    if (location.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
        //                        location.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        //                    {
        //                        return ReadHtml(location, cookie);
        //                    }

        //                    var baseUri = new Uri(urlAddress);
        //                    var redirectUri = new Uri(baseUri, location);

        //                    return ReadHtml(redirectUri.ToString(), cookie);
        //                }
        //            }
        //        }

        //        using (var stream = response.GetResponseStream())
        //        using (var reader = new StreamReader(stream, Encoding.UTF8))
        //            return reader.ReadToEnd();
        //    }
        //}

        protected static HtmlDocument GetHtmlFromText(string htmlText)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlText);
            return html;
        }
    }
}