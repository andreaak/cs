using System.IO;
using System.Text;
using HtmlAgilityPack;

namespace HtmlParser
{
    public class FileParser : BaseParser
    {
        public void Parse()
        {
            string path =
                @"D:\Downloads\Законы\Законы\CHM\pidruchniki.ws\1540030550075\pravo\naukovo-praktichniy_komentar_kodeksu_ukrayini_pro_adm~1.htm";
            string dir = Path.GetDirectoryName(path);
            Parse(dir, path);
        }

        protected override HtmlDocument GetHtml(string url)
        {
            var htmlText = File.ReadAllText(url, Encoding.GetEncoding(1251));
            return GetHtmlFromText(htmlText);
        }

        protected override string GetUrI(string dir, string url)
        {
            return dir + Path.DirectorySeparatorChar + url;
        }

        protected override HtmlNode GetContents(HtmlNode element)
        {
            throw new System.NotImplementedException();
        }

        protected override HtmlNode GetElement(HtmlNode element)
        {
            throw new System.NotImplementedException();
        }

        protected override void ReplaceImages(string dir, HtmlNode t)
        {
            var images = t.SelectNodes(".//img[@src]");

            if (images != null)
            {
                foreach (HtmlNode img in images)
                {
                    var filePath = img.Attributes["src"].Value;
                    var fullPath = dir + Path.DirectorySeparatorChar + filePath;

                    if (!Directory.Exists("images"))
                    {
                        Directory.CreateDirectory("images");
                    }

                    var fileName = "images" + Path.DirectorySeparatorChar + Path.GetFileName(fullPath);
                    File.Copy(fullPath, fileName);
                    img.Attributes["src"].Value = fileName;
                }
            }
        }
    }
}