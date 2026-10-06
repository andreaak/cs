namespace HtmlParser.Language.HTMLClients
{
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