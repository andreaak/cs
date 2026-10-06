using System.Collections.Generic;

namespace HtmlParser.Language.HTMLClients
{

    internal interface ILanguageParser
    {
        void Parse(IList<string> lines);
    }

    public class LanguageParser : HtmlReader
    {
       
        protected bool _order;
        protected WordType _type;

        public LanguageParser(bool order, WordType type)
        {
            _order = order;
            _type = type;
        }
    }
}