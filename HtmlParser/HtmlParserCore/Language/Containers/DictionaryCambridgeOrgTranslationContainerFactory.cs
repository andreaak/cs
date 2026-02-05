using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using HtmlParser.Language.Extensions;

namespace HtmlParser.Language.Containers
{
    public class DictionaryCambridgeOrgTranslationContainerFactory
    {

        //string hostUrl = "https://dictionary.cambridge.org/ru/%D1%81%D0%BB%D0%BE%D0%B2%D0%B0%D1%80%D1%8C/%D0%B0%D0%BD%D0%B3%D0%BB%D0%B8%D0%B9%D1%81%D0%BA%D0%B8%D0%B9/";
        string hostUrl = "https://dictionary.cambridge.org/dictionary/english-russian/";

        private string _word;
        private string _wordClass;
        private HtmlDocument _document;

        public DictionaryCambridgeOrgTranslationContainerFactory(string word, string wordClass)
        {
            _word = word;
            _wordClass = wordClass;
        }

        public string GetLevel()
        {
            var document = GetDocument();
            var t = document?.DocumentNode.SelectNodes(".//div[contains(@class, 'entry-body__el')]");

            if(t == null)
            {
                return "";
            }
            var set = new HashSet<string>();

            foreach (var n in t)
            {
                var wordCl = n.SelectSingleNode(".//span[@class='pos dpos']")?.InnerText.Trim();
                
                if (Convert(wordCl?.ToLower()) == _wordClass)
                {
                    var levels = n.SelectNodes(".//span[contains(@class, 'epp-xref')]")?.ToArray() ?? Array.Empty<HtmlNode>();


                    set.UnionWith(levels.Select(l => l.InnerText.Trim()).OrderBy(l => l)); //levels.Select(l => l.InnerText.Trim()).OrderBy(l => l).FirstOrDefault();
                }
            }
            
            
            //var levels = document.DocumentNode.SelectNodes(".//  span[contains(@class, 'epp-xref')]")?.ToArray() ?? Array.Empty<HtmlNode>();
            return set.OrderBy(l => l).FirstOrDefault() ?? ""; //levels.Select(l => l.InnerText.Trim()).OrderBy(l => l).FirstOrDefault();
        }

        public string GetDescription()
        {
            var document = GetDocument();
            var t = document?.DocumentNode.SelectNodes(".//div[contains(@class, 'entry-body__el')]");

            if (t == null)
            {
                return "";
            }

            var list = new List<string>();
            foreach (var n in t)
            {
                var wordCl = n.SelectSingleNode(".//span[@class='pos dpos']")?.InnerText.Trim();

                if (Convert(wordCl?.ToLower()) == _wordClass)
                {
                    var descriptions = n.SelectNodes(".//div[contains(@class, 'pr dsense')]")?.ToArray() ?? Array.Empty<HtmlNode>();


                    foreach (var description in descriptions)
                    {
                        var sense = description.SelectSingleNode(".//h3[@class='dsense_h']/span[@class='guideword dsense_gw']")?.InnerText.PonsNormalize();
                        var dsc = description.SelectSingleNode(".//div[@class='def ddef_d db']")?.InnerText.PonsNormalize();
                        var ru = description.SelectSingleNode(".//div[@class='def-body ddef_b']/span[@lang='ru']")?.InnerText.PonsNormalize();
                        var en = description.SelectSingleNode(".//div[@class='def-body ddef_b']/div[@class='examp dexamp']")?.InnerText.PonsNormalize();

                        list.Add((!string.IsNullOrEmpty(sense) ? sense + ": " : "") + $"{dsc} -- {en} - {ru}");
                    }
                    
                }
            }

            return string.Join(" ## ", list);
        }

        private string Convert(string input)
        {
            switch (input)
            {
                case "verb":
                    return "verb";
                case "adjective":
                    return "adj";
                case "adverb":
                    return "adv";
                case "preposition":
                    return "prep";
                case "noun":
                    return "subst";
                case "conjunction":
                    return "konj";
                case "phrasal verb":
                    return "verb";
                default:
                    return input;
            }
        }

        public string GetSoundEn()
        {
            var document = GetDocument();
            var sound = document?.DocumentNode.SelectSingleNode(".//span[@class='uk dpron-i ']//source")?.GetAttributeValue("src", "");
            if (string.IsNullOrEmpty(sound))
            {
                return "";
            }

            return "https://dictionary.cambridge.org" + sound.Trim();
        }

        public string GetSoundUs()
        {
            var document = GetDocument();
            var sound = document?.DocumentNode.SelectSingleNode(".//span[@class='us dpron-i ']//source")?.GetAttributeValue("src", "");
            if (string.IsNullOrEmpty(sound))
            {
                return "";
            }

            return "https://dictionary.cambridge.org" + sound.Trim();
        }

        private HtmlDocument GetDocument()
        {
            return _document = _document ?? new HtmlParser().GetHtml(hostUrl + _word);
        }
    }
}