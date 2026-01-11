using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.VerbFormParsers
{
    public abstract class BaseVerbFormParser : IVerbFormParser
    {
        protected abstract VerbForm VerbForm { get; }
        protected abstract void FillValue(string type, DeVerbForm form, HtmlNode row);

        public DeVerbForm Parse(HtmlNode node)
        {
            var rows = node.SelectNodes(".//span[@class='flected_form' and @data-pons-variant='DEFAULT']");

            var form = new DeVerbForm
            {
                Form = VerbForm
            };

            foreach (var row in rows)
            {
                var type = row.Attributes.FirstOrDefault(a => a.Name == "data-pons-flection-id")?.Value.Trim();
                if (!string.IsNullOrEmpty(type))
                {
                    FillValue(type, form, row);
                }
            }

            return form;
        }

        protected string GetPrevValue(HtmlNode row)
        {
            var startNode = row.ParentNode.ParentNode;

            var list = new List<string>
            {
                row.ParentNode.InnerText.RemoveNewLine().Trim()
            };

            var node = startNode;
            while ((node = node.PreviousSibling) != null)
            {
                if (node.InnerHtml.Contains("data-pons-flection-id"))
                {
                    break;
                }

                list.Add(node.InnerText.RemoveNewLine().Trim());
            }

            list.Reverse();

            return string.Join(" ", list);
        }

        //protected string GetNextValue(HtmlNode row)
        //{
        //    var startNode = row.ParentNode.ParentNode;
        //    return startNode.NextSibling?.InnerText.RemoveNewLine().Trim();
        //}

        protected string GetNextValues(HtmlNode row)
        {
            var list = new List<string>();
            
            var node = row.ParentNode.ParentNode;
            while (node.NextSibling != null)
            {
                list.Add(node.NextSibling.InnerText.RemoveNewLine().Trim());

                node = node.NextSibling;
            }
            
            return string.Join(" ", list);
        }
    }
}