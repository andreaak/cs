using HtmlAgilityPack;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.VerbFormParsers
{
    public class PrateritumVerbFormParser : BaseVerbFormParser
    {
        private const string PREFFIX = "INDIKATIV_PRAETERITUM";

        protected override VerbForm VerbForm => VerbForm.Prateritum;

        protected override void FillValue(string type, DeVerbForm form, HtmlNode row)
        {
            switch (type)
            {
                case PREFFIX + "_1S":
                    form.Single1 = row.InnerText.RemoveNewLine().Trim();
                    break;
                case PREFFIX + "_2S":
                    form.Single2 = row.InnerText.RemoveNewLine().Trim();
                    break;
                case PREFFIX + "_3S":
                    form.Single3 = row.InnerText.RemoveNewLine().Trim();
                    break;
                case PREFFIX + "_1P":
                    form.Plural1 = row.InnerText.RemoveNewLine().Trim();
                    break;
                case PREFFIX + "_2P":
                    form.Plural2 = row.InnerText.RemoveNewLine().Trim();
                    break;
                case PREFFIX + "_3P":
                    form.Plural3 = row.InnerText.RemoveNewLine().Trim();
                    break;
            }
        }
    }
}