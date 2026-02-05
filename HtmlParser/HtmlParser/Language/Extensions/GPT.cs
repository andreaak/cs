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
        //private const string key = "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";//alexandrzpua19
        //private const string key = "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";//andreazpua19@gmail.com
        //private const string key = "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"; //andreazpua19tr@gmail.com
        //private const string key = "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";//petrpetrovskiy19@gmail.com
        //private const string key = "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";//alexandrzpua19web
        //private const string key = "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";//andreaak19
        //private const string key = "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";//andrii.kulikov19@gmail.com
        private const string key = "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";//kirushaphoto2018@gmail.com
        //private const string key = "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";//kiryazpua16@gmail.com
        //private const string key = "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";//kirushazpua19@gmail.com
        //private const string key = "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";//kirushazpua19@gmail.com
        //private const string key = "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";//kirushazpua19@gmail.com
        //private const string key = "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";//kirushazpua19@yahoo.com

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
