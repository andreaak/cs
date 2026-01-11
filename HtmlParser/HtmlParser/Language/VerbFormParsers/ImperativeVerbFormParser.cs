using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.VerbFormParsers
{
    public class ImperativeVerbFormParser : IVerbFormParser
    {
        public DeVerbForm Parse(HtmlNode node)
        {
            var rows = node.SelectNodes(".//span[@class='flected_form' and @data-pons-variant='DEFAULT']");

            var form = new DeVerbForm
            {
                Form = VerbForm.Imperativ
            };

            if (rows != null)
            {
                foreach (var row in rows)
                {
                    var type = row.Attributes.First(a => a.Name == "data-pons-flection-id").Value.Trim();
                    switch (type)
                    {
                        case "IMPERATIV_2S":
                            form.Single2 = GetText(row);
                            break;
                        case "IMPERATIV_2P":
                            form.Plural2 = GetText(row);
                            break;
                        case "IMPERATIV_1P":
                            form.Plural1 = GetText(row);
                            break;
                        case "IMPERATIV_HOEFLICH":
                            form.Plural3 = GetText(row);
                            break;
                    }

                }
            }



            return form;
        }

        private static string GetText(HtmlNode row)
        {
            var verb = row.ParentNode.InnerText.RemoveNewLine().Trim();

            var other = string.Join(" ", row.GetNextValues().Skip(1));

            return string.IsNullOrEmpty(other) ? verb  : verb + " " + other;
        }
    }
}