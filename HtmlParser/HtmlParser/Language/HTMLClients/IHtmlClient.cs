namespace HtmlParser.Language.HTMLClients
{
    public interface IHtmlClient
    {
        string ReadHtml(string url, string cookie);
    }
}