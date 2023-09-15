using System.Collections.Generic;

namespace HtmlParser.Language
{
    public class TranslationContainer
    {
        public TranslationItem Node { get; set; }
        public IEnumerable<TranslationItem>  AllNodes { get; set; }
    }
}