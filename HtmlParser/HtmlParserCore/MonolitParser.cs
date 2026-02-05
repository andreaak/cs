using System.IO;
using System.Text;
using HtmlAgilityPack;

namespace HtmlParser
{
    public class MonolitParser : LinkParser
    {
        public void Parse()
        {
            string url = "https://monolith.in.ua/pdd/onlayn-pdd-ukrainyi-na-russkom-yazyike/";
            string dir = new DirectoryInfo("test").FullName;

            Parse(dir, url);
        }

        protected override string GetHost()
        {
            return "https://monolith.in.ua";
        }

        protected override string GetUrI(string dir, string url)
        {
            return url.Contains("http") ? url : GetHost() + url;
        }

        protected override HtmlNode GetContents(HtmlNode element)
        {
            var e1 = element.OwnerDocument.GetElementbyId("wrapper");
            return e1.SelectSingleNode(".//table").SelectSingleNode(".//ul").ParentNode;
        }

        protected override HtmlNode GetElement(HtmlNode element)
        {
            var e1 = element.OwnerDocument.GetElementbyId("wrap");
            if (e1 != null)
            {
                var nav = e1.SelectSingleNode(".//table[@class='nav-block-tag']");
                nav?.Remove();
                var lang = e1.SelectSingleNode(".//div[@class='lang']");
                lang?.Remove();

                RemoveElements(e1, ".//td[@class='fcol']");
                RemoveElements(e1, ".//div[@class='spoiler-link']");
                //RemoveElements(e1, ".//div[@class='img-pdd']/a");

                var nodes = e1.SelectNodes(".//div[@class='img-pdd']/a");
                if (nodes != null)
                {
                    foreach (var el in nodes)
                    {
                        el.SetAttributeValue("href", "");
                    }
                }

                return e1;
            }
            return null;
        }

        private static void RemoveElements(HtmlNode e1, string query)
        {
            var nodes = e1.SelectNodes(query);
            if (nodes != null)
            {
                foreach (var el in nodes)
                {
                    el.Remove();
                }
            }
        }

        protected override void ProcessElement(HtmlNode element, HtmlNode body, string dir, string uri, string text)
        {
            if (element == null)
            {
                return;
            }
            var addedElement = AddElement(element, body);
            ReplaceImages(dir, addedElement);
            
            //ReplaceImages(dir, element);
            //Save(element, text);
        }

        private void Save(HtmlNode element, string uri)
        {
            var  newDocument = CreateHtml().OwnerDocument;
            AddElement(element, newDocument.DocumentNode);
            newDocument.Save(Normalize(uri) + ".html", Encoding.UTF8);
        }

        protected override HtmlNode CreateHtml()
        {
            var newDocument = new HtmlDocument();
            var html = newDocument.DocumentNode.AppendChild(newDocument.CreateElement("html"));
            var head = html.AppendChild(newDocument.CreateElement("head"));
            head.InnerHtml = "<link media=\"all\" rel=\"stylesheet\" type=\"text/css\" href=\"new-style.css\">";
            var body = html.AppendChild(newDocument.CreateElement("body"));
            return body;
        }
    }
}