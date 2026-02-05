using HtmlAgilityPack;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.VerbFormParsers
{
    public class KonjunktivIIFuturIVerbFormParser : BaseVerbFormParser
    {
        private const string PREFFIX = "KONJUNKTIV_II_FUTUR_I";

        protected override VerbForm VerbForm => VerbForm.KonjunktivIIFuturI;

        protected override void FillValue(string type, DeVerbForm form, HtmlNode row)
        {
            var text = GetPrevValue(row);

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