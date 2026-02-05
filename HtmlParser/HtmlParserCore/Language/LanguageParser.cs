using System;
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

    [Flags]
    public enum WordType
    {
        None = 0,
        Verb = 1,
        Subst = 2,
        Pron = 4,
        Adv = 8,
        Adj = 16,
        Konj = 32,
        Prep = 64,
        All = 128,
        Complex = 256
    }

    public static class TestFlagsEnumHelper
    {
        public static bool IsSet(this WordType options, WordType option)
        {
            if (option == 0)
            {
                return false;
                //throw new ArgumentOutOfRangeException("option", "Value must not be 0");
            }
            return (options & option) == option;
        }

        public static bool AnyFlagsSet(this WordType options, WordType option)
        {
            return ((options & option) != 0);
        }

        public static WordType Set(this WordType options, WordType option)
        {
            return options | option;
        }

        public static WordType Clear(this WordType options, WordType option)
        {
            return options & ~option;
        }
    }
}