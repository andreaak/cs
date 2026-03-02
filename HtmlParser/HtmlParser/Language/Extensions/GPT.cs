using System;
using System.Linq;
using OpenAI.Chat;

namespace HtmlParser.Language.Extensions
{
    public class GPT
    {
        //private const string modelName = "gpt-4o-mini";
        //private const string modelName = "gpt-5-mini";
        
        private const string modelName = "gpt-4.1-mini";
        private const string key = "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";//alexandrzpua19
        //private const string key = "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";//andreazpua19@gmail.com
        //private const string key = "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";//alexandrzpua19@yahoo.com

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
