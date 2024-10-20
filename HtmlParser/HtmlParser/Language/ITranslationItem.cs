using System.Collections.Generic;
using HtmlAgilityPack;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public interface ITranslationItem
    {
        HtmlNode RomHeadNode { get; set; }
        IEnumerable<HtmlNode> TranslationNodes { get; set; }
        TranslationAttributes GetAttributes();
        string GetTranscription(IEnumerable<ITranslationItem> allNodes);
        string GetWordClass();
        IList<string> GetTranslations(string word);
        string GetTranslation(string word);
        string GetLink();
    }
}