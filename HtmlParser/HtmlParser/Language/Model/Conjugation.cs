using HtmlAgilityPack;

namespace HtmlParser.Language.Model
{
    public class Conjugation
    {
        public string Id { get; }
        public string Info { get; }
        
        public Conjugation(HtmlNode node)
        {
            Id = node.SelectSingleNode(".//input").GetAttributeValue("id", null);
            Info = node.InnerText.Trim();
        }
    }
}