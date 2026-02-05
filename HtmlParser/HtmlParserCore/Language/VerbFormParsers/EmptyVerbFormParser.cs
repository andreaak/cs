using HtmlAgilityPack;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.VerbFormParsers
{
    public class EmptyVerbFormParser : IVerbFormParser
    {
        public DeVerbForm Parse(HtmlNode node)
        {
            return null;
        }
    }
}