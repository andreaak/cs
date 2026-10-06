using System;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.HTMLClients;

namespace HtmlParser.Language
{
    public class AIProvider
    {
        private GPT gpt;

        public AIProvider()
        {
            gpt = new GPT();
        }
        
        
        public string GetGptDescription(string de, WordType _type, Language lang, string verbClass = "")
        {
            switch (_type)
            {
                case WordType.Verb:
                    return GetVerbDescription(de, verbClass, lang);
                case WordType.Adv:
                    return GetAdverbDescription(de, lang);
                case WordType.Adj:
                    return GetAdjDescription(de, lang);
                case WordType.Subst:
                    return GetSubstDescription(de, lang);
                case WordType.Konj:
                    return GetKonjDescription(de, lang);
                case WordType.Prep:
                    return GetPrepDescription(de, lang);
                case WordType.Complex:
                    return SetComplexDescription(de);
                default:

                    break;
            }

            return "";
        }

        private string GetVerbDescription(string de, string verbClass, Language lang)
        {
            de = de.Replace("|", "");

            try
            {
                string langItem;
                switch (lang)
                {
                    case Language.Deutsch:
                        langItem = "немецкого";
                        break;
                    case Language.English:
                        langItem = "английского";
                        break;
                    default:
                        return null;
                }

                string request =
                    $"значение и типы {langItem} глагола {de} {verbClass} " +
                    $"с переводом на русский, примерами, синонимами, антонимами, " +
                    $"предложное управление, транскрипция и также уровень слова";

                var res = gpt.GetResponse(request);

                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private string GetAdverbDescription(string de, Language lang)
        {
            string part = "наречия";
            return GetGPTDescription(de, part, "adv", lang);
        }

        private string GetAdjDescription(string de, Language lang)
        {
            string part = "прилагательного";
            return GetGPTDescription(de, part, "adj", lang);
        }

        private string GetSubstDescription(string de, Language lang)
        {
            string part = "существительного";
            return GetGPTDescription(de, part, "subst", lang);
        }

        private string GetKonjDescription(string de, Language lang)
        {
            string part = "союза";
            return GetGPTDescription(de, part, "konj", lang);
        }

        private string GetPrepDescription(string de, Language lang)
        {
            string part = "предлога";
            return GetGPTDescription(de, part, "präp", lang);
        }

        private string SetComplexDescription(string de)
        {
            return GetTranslationWithInfo(de);
        }

        private string GetGPTDescription(string de, string part, string wordClass, Language lang)
        {
            try
            {
                string langItem;
                switch (lang)
                {
                    case Language.Deutsch:
                        langItem = "немецкого";
                        break;
                    case Language.English:
                        langItem = "английского";
                        break;
                    default:
                        return null;
                }

                string request =
                    $"значение {langItem} {part} {de} с переводом на русский, примерами, синонимами, антонимами, предложное управление, транскрипция и также уровень слова";
                var res = gpt.GetResponse(request);

                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public string GetTranslationWithInfo(string de)
        {
            try
            {
                string request = $"перевод фразы {de} на русский с примерами, синонимами, антонимами";
                var res = gpt.GetResponse(request);

                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public string GetTranslation(string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
            {
                return "";
            }
            try
            {
                string request =
                    $"переведи с немецкого языка текст {phrase}. Выдай только перевод в одну строку без лишней информации";

                var res = gpt.GetResponse(request);
                return res;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public string GetExample(string de, WordType wordType, string verbClass = "", Language lang = Language.Deutsch)
        {
            string example = "";
            if (wordType == WordType.Verb)
            {
                var request = GetVerbExampleRequest(de, verbClass, lang);
                example = gpt.GetResponse(request);
            }
            else
            {
                example = GetExample(de, lang, wordType.GetExampleType());
            }

            return example;
        }

        private static string NormalizeGpt(string value)
        {
            int index = value.IndexOf(":");
            if (index >= 0)
            {
                return value.Substring(index + 1).Trim();
            }

            return value.Trim();
        }

        public static string GetVerbExampleRequest(string word, string verbClass, Language lang)
        {
            var value = word.Replace("|", "");

            string sr = !string.IsNullOrEmpty(verbClass)
                ? $" {GetLanguageRequest(lang)} {verbClass} глагола {value}"
                : $" {GetLanguageRequest(lang)} глагола {value}";

            string request = $"выдай пример использования {sr} в предложении и перевод предложения на русский язык.";
            request += " Значения выдать в одну строку в указанном порядке с разделителем |." +
                       " Должно быть только два предложения - сгенерированное и перевод на русский." +
                       " Не добавляй лишней информации.";
            return request;
        }

        private static string GetLanguageRequest(Language lang)
        {
            switch (lang)
            {
                case Language.Deutsch:
                    return "немецкого";
                case Language.English:
                    return "английского";
                default:
                    throw new ArgumentException("");
            }

        }

        public string GetExample(string word, Language lang, string type, string subType = null)
        {
            word = word.Replace("|", "");

            string sr = !string.IsNullOrEmpty(type)
                ? $" {GetLanguageRequest(lang)} {type} {word}"
                : $" {GetLanguageRequest(lang)} слова '{word}'";

            string request = $"выдай пример использования {sr} в предложении и перевод предложения на русский язык.";
            request += " Значения выдать в одну строку в указанном порядке с разделителем |." +
                       " Должно быть только два предложения - сгенерированное и перевод на русский." +
                       " Не добавляй лишней информации.";

            return gpt.GetResponse(request);
        }
    }
}