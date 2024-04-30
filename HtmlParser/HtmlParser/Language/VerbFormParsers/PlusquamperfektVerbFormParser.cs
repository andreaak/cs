using HtmlAgilityPack;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.VerbFormParsers
{
    public class PlusquamperfektVerbFormParser : BaseVerbFormParser
    {
        private const string PREFFIX = "INDIKATIV_PLUSQUAMPERFEKT";

        protected override VerbForm VerbForm => VerbForm.Plusquamperfekt;

        protected override void FillValue(string type, DeVerbForm form, HtmlNode row)
        {
            var text = row.ParentNode.ParentNode.PreviousSibling.InnerText.RemoveNewLine().Trim()
                       + " "
                       + row.InnerText.RemoveNewLine().Trim();

            switch (type)
            {
                case PREFFIX + "_1S":
                    form.Single1 = text;
                    break;
                case PREFFIX + "_2S":
                    form.Single2 = text;
                    break;
                case PREFFIX + "_3S":
                    form.Single3 = text;
                    break;
                case PREFFIX + "_1P":
                    form.Plural1 = text;
                    break;
                case PREFFIX + "_2P":
                    form.Plural2 = text;
                    break;
                case PREFFIX + "_3P":
                    form.Plural3 = text;
                    break;
            }
        }
    }
}