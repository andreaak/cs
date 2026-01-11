using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using HtmlAgilityPack;

namespace HtmlParser
{
    public class HtmlParser
    {
        public HtmlDocument GetHtml(string url)
        {
            int repeat = 3;
            int timeout = 2000;
            while (repeat-- > 0)
            {
                try
                {
                    var htmlText = ReadHtml(url);
                    return GetHtmlFromText(htmlText);
                }
                catch (Exception e)
                {
                    Console.WriteLine(url);
                    Console.WriteLine(e);
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

        private string ReadHtml(string urlAddress)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);

            //request.ContentType = "text/html; charset=utf-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

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

        protected static HtmlDocument GetHtmlFromText(string htmlText)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlText);
            return html;
        }
    }
}