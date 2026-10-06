using System.IO;
using System.Net;
using System.Text;

namespace HtmlParser.Language.HTMLClients
{
    public class DefaultHtmlClient : IHtmlClient
    {
        public string ReadHtml(string url, string cookie)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.AllowAutoRedirect = true;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/151.0.0.0 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            request.Headers["Accept-Language"] = "en-US,en;q=0.9,ru;q=0.8";
            request.Headers["Cache-Control"] = "no-cache";
            request.Headers["Pragma"] = "no-cache";
            request.Headers["Upgrade-Insecure-Requests"] = "1";
            request.Headers["Sec-Fetch-Dest"] = "document";
            request.Headers["Sec-Fetch-Mode"] = "navigate";
            request.Headers["Sec-Fetch-Site"] = "none";
            request.Headers["Sec-Fetch-User"] = "?1";
            request.Headers["sec-ch-ua"] = "\"Not=A?Brand\";v=\"99\", \"Google Chrome\";v=\"151\", \"Chromium\";v=\"151\"";
            request.Headers["sec-ch-ua-mobile"] = "?0";
            request.Headers["sec-ch-ua-platform"] = "\"Windows\"";
            request.Headers["Cookie"] = cookie;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                //Console.WriteLine("Status: " + (int)response.StatusCode + " " + response.StatusDescription);
                //Console.WriteLine("Final URL: " + response.ResponseUri);
                //Console.WriteLine("CharacterSet: " + response.CharacterSet);
                //Console.WriteLine("ContentType: " + response.ContentType);

                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}