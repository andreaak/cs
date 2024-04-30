using System.Linq;
using HtmlAgilityPack;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.VerbFormParsers
{
    public abstract class BaseVerbFormParser : IVerbFormParser
    {
        protected abstract VerbForm VerbForm { get; }
        protected abstract void FillValue(string type, DeVerbForm form, HtmlNode row);

        public DeVerbForm Parse(HtmlNode node)
        {
            var rows = node.SelectSingleNode(".//table").SelectNodes(".//span[@class='flected_form' and @data-pons-variant='DEFAULT']");

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
    }
}