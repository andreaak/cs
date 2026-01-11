using HtmlAgilityPack;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.VerbFormParsers
{
    public class PrasensVerbFormParser : BaseVerbFormParser
    {
        private const string PREFFIX = "INDIKATIV_PRAESENS";

        protected override VerbForm VerbForm => VerbForm.Prasens;

        protected override void FillValue(string type, DeVerbForm form, HtmlNode row)
        {
            var next = GetNextValues(row);

            var text = row.InnerText.RemoveNewLine().Trim()
                       + (string.IsNullOrEmpty(next) ? "" : " " + next);

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