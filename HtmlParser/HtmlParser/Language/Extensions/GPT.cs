using System;
using System.Linq;
using OpenAI.Chat;

namespace HtmlParser.Language.Extensions
{
    public class GPT
    {
        //private const string modelName = "gpt-4o-mini";
        //private const string key = "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";
        private const string modelName = "gpt-4.1-mini";
       // private const string key = "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";
        private const string key = "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";

        //private const string modelName = "gpt-5-mini";
        //private const string key = "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";


        public string GetResponse(string request)
        {
            try
            {
                var client = new ChatClient(modelName, key);

                var response = client.CompleteChat(request);


                var text = response.Value.Content.FirstOrDefault()?.Text
                    .Replace("Пример:", "")
                    .Replace("Перевод:", "")
                    .Replace("**Русский:**", "")
                    .Replace("**Deutsch:**", "")
                    .Replace("\"", "")
                    .Replace("\r\n", " - ")
                    .Replace("\r", " - ")
                    .Replace("\n", " - ")
                    .Replace("  ", " ")
                    .Replace("  ", " ")
                    .Trim();

                return text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }

            return "";

        }
    }
}
