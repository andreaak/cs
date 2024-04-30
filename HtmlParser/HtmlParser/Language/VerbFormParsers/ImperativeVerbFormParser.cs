using System.Linq;
using HtmlAgilityPack;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.VerbFormParsers
{
    public class ImperativeVerbFormParser : IVerbFormParser
    {
        public DeVerbForm Parse(HtmlNode node)
        {
            var rows = node.SelectSingleNode(".//table").SelectNodes(".//span[@class='flected_form' and @data-pons-variant='DEFAULT']");

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
                            form.Single2 = row.InnerText.RemoveNewLine().Trim();
                            break;
                        case "IMPERATIV_2P":
                            form.Plural2 = row.InnerText.RemoveNewLine().Trim();
                            break;
                        case "IMPERATIV_1P":
                            form.Plural1 = row.InnerText.RemoveNewLine().Trim();
                            break;
                        case "IMPERATIV_HOEFLICH":
                            form.Plural3 = row.InnerText.RemoveNewLine().Trim();
                            break;
                    }

                }
            }



            return form;
        }
    }
}