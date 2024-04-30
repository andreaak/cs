using HtmlAgilityPack;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.VerbFormParsers
{
    public class KonjunktivIPrasensVerbFormParser : BaseVerbFormParser
    {
        private const string PREFFIX = "KONJUNKTIV_I_PRAESENS";

        protected override VerbForm VerbForm => VerbForm.KonjunktivIPrasens;

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