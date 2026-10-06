using HtmlParser.Language.HTMLClients;

namespace HtmlParser
{
    public class Parameters
    {
        public string Action { get; set; }
        public bool Order { get; set; }
        public string Lang { get; set; }
        public WordType WordType { get; set; }
        public bool GetExample { get; set; }
        public bool GetPreposition { get; set; }
        public bool SetLevel { get; set; }
        public bool RemoveDuplicates { get; set; }
        public bool AddOtherTranslation { get; set; }
        public bool AddDescription { get; set; }
        public bool AddAIDescription { get; set; }
        public bool AddWBDescription { get; set; }
    }
}