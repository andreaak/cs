using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Cache;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using HtmlAgilityPack;

namespace HtmlParser
{
    public class HtmlParser
    {
        public HtmlDocument GetHtml(string url, string cookie = null)
        {
            int repeat = 3;
            int timeout = 2000;
            while (repeat-- > 0)
            {
                try
                {
                    var htmlText = ReadHtml(url, cookie);
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

        private string ReadHtml(string urlAddress, string cookie = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            request.ContentType = "text/html;charset=UTF-8";

            if (!string.IsNullOrEmpty(cookie))
            {
                request.Headers["cookie"] = cookie;// "uniqueUser=6d90f3efb9c8900bfd1479adb978010868492683fc575f62312ef881542b4947; cto_bidid=ZTKtf19VRXNvbE1TeFVPam84RmJseFFjQWs3bmV5b2FWNUtOc3hpekFXa3I4cGprWUdyd1lJcXNZOFZBaUJFQ0d4QlBxVmlYOXZyZTFMVTE0RGJqdHE2V1NheFBSbzRhaXd2RjFBODhqMGdjNUolMkZJJTNE; __gads=ID=92a61025abb948de:T=1751483432:RT=1751483432:S=ALNI_MZ1NcG5hE1l1S8CwVzIcYVVzjhx1A; euconsent-v2=CQYqYUAQYqYUAAGABCENB-FsAP_gAAAAAAYgLJAB5C7cTWFhcDhXAaMAaIwc1xABJkAAAgKAASABSBIAcIQEkiACMAyAAAACAAAAIABAAAAgAABAAQAAAIgAAAAEAAAEAAAIICAEAAMRQgAACAAICAAAAQAIAAABAgEAiACAQQKEQFAAgIAgBAAAAIAgAIABAAMAAAAgAAAAAAAAAgAAgQAAAAAAAAACABAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAeEgNgALAAqABcADgAHgAQQAyADUAHgATAA3gB-AEJAIYAiQBHACaAGGAO6AfgB-gG0ATSAo8BeYDJAGXANYAbmBBMIAJABIAEcAP4A5wCUgE7AR6AuoBkIgACACQUABAR6MAAgI9HQHgAFgAVAA4ACCAGQAagA8ACYAF0AMQAbwA_QCGAIkATQAw4B-AH6ARYAjoBtAEXgJkATSAo8BeYDJIGWAZcA00BrADiwIAgR2HAEQALgAkAB-AEcAKAAfwBHQDkAOcAdwBCACUgE7AR6AmIBdQDIQG5kIBIACwAagBiADeAH4AdwBKQDaAJpIABQA_wDkAOcBHoCYgIskoBwACwAOAA8ACYAGKAQwBEgCOAH4Ai8BR4C8wGSANYAgCSAEgAXABIACoAI4A7gDtgI9ATEAywpAXAAWABUADgAIIAZABoADwAJgAYgA_QCGAImAfgB-gEWAI6AbQBF4CaQF5gMkgZYBlwDWAHigQTAjsUAKAAKAAuACQAI4AUAAtgBtAEdAOQA5wB3AEpALqAa8A7YCPQExAKyAZCA3MCLJaAEADUAdxYAGAj0BMQDIQ.YAAAAAAAAAAA; consentUUID=26fc6c4a-0f13-42b5-b334-e4412c35f16b_44_46_48; cto_bundle=puilBl8zcFhHSUVPZ1JLbTclMkJGRjVyc2lIOVowejFxQzNrbEl5eVdBZ0RtVXJzJTJCaHNJQWwyOHhMZDdzYnQ1MGZ2RU9Oak0xYm9iOFg4QkV0V2o2RCUyQnJnJTJGdiUyRiUyQjllQmxQdXpDeU1hRSUyRldpY0clMkI3cEZTeFV4M2x2aFU1Y2JvMnFSdmRXYjVnRlp2NSUyRmJCNk5WTkZHWjNGOVpGd1EwdSUyQmFWTVFPV291VTlleEdmSERmVSUzRA; jsok=1; zgrf_id=0cc6af43-0867-4f4e-8a67-1ca0c666d31d; _gid=GA1.2.1195438955.1768160164; com.netzverb.setting.translationLanguage=ru; JSESSIONID=F0C5FB5349E327B7527447FD033E9699; g_state={\"i_l\":0,\"i_ll\":1768390339065,\"i_b\":\"0UmUFg / uDyh4THiq4HSFLWBJHpUGYAos2VqQ19lnqUg\",\"i_e\":{\"enable_itp_optimization\":0}}; _ga_8XG72KX9YD=GS2.1.s1768390154$o330$g1$t1768390743$j58$l0$h0; _ga=GA1.1.378734771.1749207043";
            }


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