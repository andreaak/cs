using HtmlParser.Language;

namespace HtmlParser
{
    public class Parameters
    {
        public string Action { get; set; }
        public bool Order { get; set; }
        public string Lang { get; set; }
        public WordType WordType { get; set; }
        public bool GetExample { get; set; }
        public bool SetLevel { get; set; }
        public bool RemoveDuplicates { get; set; }
    }
}