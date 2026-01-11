using System.Collections.Generic;

namespace HtmlParser.Language.Containers 
{
    public class TranslationContainer
    {
        public ITranslationItem Node { get; set; }
        public IEnumerable<ITranslationItem>  AllNodes { get; set; }
    }
}