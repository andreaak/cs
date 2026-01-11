using System;
using System.Linq;
using OpenAI.Chat;

namespace HtmlParser.Language.Extensions
{
    public class GPT
    {


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
