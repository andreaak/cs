using HtmlAgilityPack;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.VerbFormParsers
{
    public interface IVerbFormParser
    {
        DeVerbForm Parse(HtmlNode node);
    }
}