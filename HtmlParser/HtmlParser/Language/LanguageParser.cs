using System.Collections.Generic;

namespace HtmlParser.Language
{

    internal interface ILanguageParser
    {
        void Parse(IList<string> lines);
    }

    public class LanguageParser : HtmlParser
    {
       
        protected bool _order;
        protected WordType _type;

        public LanguageParser(bool order, WordType type)
        {
            _order = order;
            _type = type;
        }
    }

    public enum WordType
    {
        None = 0,
        Verb,
        Subst,
        Pron,
        Adv, 
        Adj,
        Konj,
        Prep,
        All,
        Complex
    }
}