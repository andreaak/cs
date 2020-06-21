using System.Collections.Generic;
using HtmlAgilityPack;

namespace HtmlParser
{
    public abstract class BaseParser
    {
        protected HtmlDocument Parse(string dir, string url)
        {
            var document = GetHtml(url);

            var addedElements = new List<string>();
            var newBody = CreateHtml();
            var contents = GetContents(document.DocumentNode);
            var addedElement = AddElement(contents, newBody);
            ReplaceImages(dir, addedElement);

            foreach (HtmlNode link in addedElement.SelectNodes(".//a[@href]"))
            {
                var uri = link.Attributes["href"].Value;
                AddHtmlPage(newBody, dir, uri, link.InnerText, addedElements);
            }

            return newBody.OwnerDocument;
        }

        protected virtual HtmlNode CreateHtml()
        {
            var newDocument = new HtmlDocument();
            var html = newDocument.DocumentNode.AppendChild(newDocument.CreateElement("html"));
            var head = html.AppendChild(newDocument.CreateElement("head"));
            var body = html.AppendChild(newDocument.CreateElement("body"));
            return body;
        }

        protected static HtmlDocument GetHtmlFromText(string htmlText)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlText);
            return html;
        }

        protected abstract HtmlNode GetContents(HtmlNode element);

        protected abstract HtmlNode GetElement(HtmlNode element);

        protected virtual HtmlNode AddElement(HtmlNode element, HtmlNode root)
        {
            return root.AppendChild(element);
        }

        protected abstract void ReplaceImages(string dir, HtmlNode t);

        protected abstract HtmlDocument GetHtml(string url);

        protected virtual string GetUrI(string dir, string url)
        {
            return url;
        }

        protected virtual void AddHtmlPage(HtmlNode body, string dir, string url, string text, List<string> addedElements)
        {
            if (url.Contains("#"))
            {
                url = url.Substring(0, url.IndexOf("#"));
            }

            if (addedElements.Contains(url))
            {
                return;
            }
            addedElements.Add(url);

            var uri = GetUrI(dir, url);
            if (uri.Contains("/log_in/"))
            {
                return;
            }
            var document = GetHtml(uri);
            if (document == null)
            {
                return;
            }
            var element = GetElement(document.DocumentNode);

            if (element == null)
            {
                return;
            }
            ProcessElement(element, body, dir, uri, text);
        }

        protected virtual void ProcessElement(HtmlNode element, HtmlNode body, string dir, string uri, string text)
        {
            var addedElement = AddElement(element, body);
            ReplaceImages(dir, addedElement);
        }
    }
}