using System.Collections.Generic;
using HtmlAgilityPack;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public interface ITranslationItem
    {
        //HtmlNode RomHeadNode { get; set; }
        TranslationAttributes Attributes { get; set; }
        string SoundId { get; }
        string GetTranscription(IEnumerable<ITranslationItem> allNodes);
        IList<string> GetTranslations(string word);
        
    }
}