using System.Linq;

namespace HtmlParser.Language
{
    public static class HtmlNodeExtensions
    {
        public static string GetWordClass(this TranslationItem node)
        {
            return node.RomHeadNode.SelectSingleNode(".//span[@class='wordclass']")?.InnerText.Trim().ToLower();
        }
        
        public static string GetTranslation(this TranslationItem node)
        {
            return node.TranslationNodes.FirstOrDefault()
                ?.InnerText?.Trim().Replace("\u0341", "");
        }

        public static string GetLink(this TranslationItem node)
        {
            var a = node.TranslationNodes.FirstOrDefault().SelectSingleNode(".//a");
            
            return a.Attributes.FirstOrDefault(at => at.Name == "href")?.Value;
        }
    }
}