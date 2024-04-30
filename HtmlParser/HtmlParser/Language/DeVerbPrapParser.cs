using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using HtmlAgilityPack;
using HtmlParser.Language.Model;
using HtmlParser.Language.VerbFormParsers;

namespace HtmlParser.Language
{
    public class DeVerbPrapParser : LanguageParser, ILanguageParser
    {
        public DeVerbPrapParser(bool order, WordType type)
            : base(order, type)
        { }

        public void Parse(IList<string> lines)
        {
            var temp = lines.Where(l => !string.IsNullOrEmpty(l))
                .SelectMany(l => Parse(l.Trim()));

            var list = _order ?
                temp.OrderBy(l => l.De).ToArray() :
                temp.ToArray();

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    item.Write(sw);
                }
            }
        }

        private IList<WordClass> Parse(string de)
        {
            Console.WriteLine(de);

            if (_type == WordType.Verb)
            {
                de = de.Replace("|", "");
            }

            var hostUrl = "https://dict.leo.org/russisch-deutsch/";

            var document = GetHtml(hostUrl + de);

            var trNodes = document.DocumentNode.SelectNodes(".//div[@data-dz-name='verb']//table//tr[@class='is-clickable']");

            var list = new List<WordClass>();

            StringBuilder sb = new StringBuilder();

            foreach (var node in trNodes)
            {
                var ru = GetRu(node);
                var des = GetDe(node);

                sb.Append(ru).Append("\t").Append(des).Append("\r\n");
            }

            list.Add(new Verb
            {
                De = de,
                Ru = sb.ToString()
            });

            return list;
        }

        private static string GetRu(HtmlNode node)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var n in node.SelectSingleNode(".//td[@lang='ru']/samp").ChildNodes)
            {
                if (n.Name == "a" || n.Name == "#text")
                {
                    sb.Append(n.InnerText.Trim() + " ");
                } 
                else if (n.Name == "br")
                {
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("/");
                }
            }
            return sb.ToString().Trim().Replace("́", "").Replace("  ", " ");
        }

        private static string GetDe(HtmlNode node)
        {
            StringBuilder sb = new StringBuilder();

            var t1 = node.SelectSingleNode(".//td[@lang='de']/samp");
            var t2 = t1.SelectSingleNode(".//a[@title='Verbtabelle öffnen']");
            if (t2 != null)
            {
                t2.Remove();
            }

            foreach (var n in t1.ChildNodes)
            {
                sb.Append(n.InnerText + " ");
            }


            return sb.ToString().Trim().Replace("\u0341", "").Replace("  ", " ");
        }
    }
}