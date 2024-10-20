namespace HtmlParser.Language
{
    public static class StringExtensions
    {
        public static string RemoveNewLine(this string data)
        {
            return data?.Replace("\n", "").Replace("\t", "");
        }
    }
}