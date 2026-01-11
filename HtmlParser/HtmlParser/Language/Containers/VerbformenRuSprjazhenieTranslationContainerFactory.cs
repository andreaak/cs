using System;
using System.Linq;
using HtmlAgilityPack;
using HtmlParser.Language.Extensions;

namespace HtmlParser.Language.Containers
{
    public class VerbformenRuSprjazhenieTranslationContainerFactory
    {
        string hostUrl = "https://www.verbformen.ru/sprjazhenie/?w=";


        private string _word;
        private string _wordClass;
        private HtmlDocument _document;

        public VerbformenRuSprjazhenieTranslationContainerFactory(string word, string wordClass)
        {
            _word = word.Replace("|", "");
            _wordClass = wordClass;
        }

        public string GetLevel()
        {
            var document = GetDocument();

            var level = document?.DocumentNode.SelectSingleNode(".//span[@class='bZrt']");
            if (level == null)
            {
                return "";
            }

            return level.InnerText.Trim();
        }

        public string GetDe()
        {
            var document = GetDocument();

            var value = document?.DocumentNode.SelectSingleNode(".//span[@id='grundform']");
            if (value == null)
            {
                var ru = document?.DocumentNode.SelectSingleNode(".//span[@class='vGrnd']");
                if (ru == null)
                {
                    return "";
                }

                return (ru.ChildNodes[1].InnerText.Trim().RemoveNewLine()
                    .Replace("  ", " ")
                    .Replace("́", "")
                    .Replace("&nbsp;", "")
                    .Replace(", ", "/")
                    .Replace(",", "/"));
            }

            return value.InnerText.Trim();
        }

        public string GetTranslation()
        {
            var document = GetDocument();

            var ru = document?.DocumentNode.SelectSingleNode(".//dd[@lang='ru']");
            if (ru == null)
            {
                return "";
            }

            var t = (ru.InnerText.Trim().RemoveNewLine()
                    .Replace("  ", " ")
                    .Replace("́", "")
                    .Replace("&nbsp;", "")).Split(new [] {","}, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim())
                .Distinct().ToArray();

            
                //.Replace(", ", "/")
                //.Replace(",", "/")

            return string.Join("/", t) ;
        }

        public string GetArtikel()
        {
            var document = GetDocument();

            var ru = document.DocumentNode.SelectSingleNode(".//span[@class='vGrnd']");
            if (ru == null)
            {
                return "";
            }

            return (ru.ChildNodes[0].InnerText.Trim().RemoveNewLine()
                .Replace("  ", " ")
                .Replace("́", "")
                .Replace("&nbsp;", "")
                .Replace(", ", "/")
                .Replace(",", "/"));
        }

        public string GetSound()
        {
            var document = GetDocument();

            var trNode = document?.DocumentNode.SelectNodes(".//div[@class='rAufZu']")?.FirstOrDefault();
            var sound = trNode?.SelectSingleNode(".//span[contains(@class, 'vGrnd')]//a")?.GetAttributeValue("href", "");
            if (string.IsNullOrEmpty(sound))
            {
                return "";
            }

            return sound.Trim();
        }

        private HtmlDocument GetDocument()
        {
            return _document = _document ?? new HtmlParser().GetHtml(hostUrl + _word + GetAdd(_word));
        }

        private string GetAdd(string de)
        {
            if (string.IsNullOrEmpty(_wordClass))
            {
                return "";
            }

            de = de.Replace("ü", "u3")
                .Replace("ö", "o3")
                .Replace("ä", "a3")
                .Replace("ß", "ss");  

            switch (_wordClass)
            {
                case "konj" :
                    return $"&id=konjunktion%3A{de}";
                case "adv" :
                    return $"&id=adverb%3A{de}";
                case "adj" :
                    return $"&id=adjektiv%3A{de}";
                case "verb" :
                    return $"&id=verb%3A{de}";
                case "subst" :
                    return $"&t=substantiv";
                case "präp":
                    return $"&id=praeposition%3A{de}";
                default:
                    return "";
            }
            

        }
    }
}