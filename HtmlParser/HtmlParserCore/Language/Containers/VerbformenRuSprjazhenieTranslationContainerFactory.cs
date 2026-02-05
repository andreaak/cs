using System;
using System.Linq;
using HtmlAgilityPack;
using HtmlParser.Language.Extensions;

namespace HtmlParser.Language.Containers
{
    public class VerbformenRuSprjazhenieTranslationContainerFactory
    {
        string hostUrl = "https://www.verbformen.ru/sprjazhenie/?w=";
        public const string Cookie = "uniqueUser=6d90f3efb9c8900bfd1479adb978010868492683fc575f62312ef881542b4947; cto_bidid=ZTKtf19VRXNvbE1TeFVPam84RmJseFFjQWs3bmV5b2FWNUtOc3hpekFXa3I4cGprWUdyd1lJcXNZOFZBaUJFQ0d4QlBxVmlYOXZyZTFMVTE0RGJqdHE2V1NheFBSbzRhaXd2RjFBODhqMGdjNUolMkZJJTNE; __gads=ID=92a61025abb948de:T=1751483432:RT=1751483432:S=ALNI_MZ1NcG5hE1l1S8CwVzIcYVVzjhx1A; euconsent-v2=CQYqYUAQYqYUAAGABCENB-FsAP_gAAAAAAYgLJAB5C7cTWFhcDhXAaMAaIwc1xABJkAAAgKAASABSBIAcIQEkiACMAyAAAACAAAAIABAAAAgAABAAQAAAIgAAAAEAAAEAAAIICAEAAMRQgAACAAICAAAAQAIAAABAgEAiACAQQKEQFAAgIAgBAAAAIAgAIABAAMAAAAgAAAAAAAAAgAAgQAAAAAAAAACABAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAeEgNgALAAqABcADgAHgAQQAyADUAHgATAA3gB-AEJAIYAiQBHACaAGGAO6AfgB-gG0ATSAo8BeYDJAGXANYAbmBBMIAJABIAEcAP4A5wCUgE7AR6AuoBkIgACACQUABAR6MAAgI9HQHgAFgAVAA4ACCAGQAagA8ACYAF0AMQAbwA_QCGAIkATQAw4B-AH6ARYAjoBtAEXgJkATSAo8BeYDJIGWAZcA00BrADiwIAgR2HAEQALgAkAB-AEcAKAAfwBHQDkAOcAdwBCACUgE7AR6AmIBdQDIQG5kIBIACwAagBiADeAH4AdwBKQDaAJpIABQA_wDkAOcBHoCYgIskoBwACwAOAA8ACYAGKAQwBEgCOAH4Ai8BR4C8wGSANYAgCSAEgAXABIACoAI4A7gDtgI9ATEAywpAXAAWABUADgAIIAZABoADwAJgAYgA_QCGAImAfgB-gEWAI6AbQBF4CaQF5gMkgZYBlwDWAHigQTAjsUAKAAKAAuACQAI4AUAAtgBtAEdAOQA5wB3AEpALqAa8A7YCPQExAKyAZCA3MCLJaAEADUAdxYAGAj0BMQDIQ.YAAAAAAAAAAA; consentUUID=26fc6c4a-0f13-42b5-b334-e4412c35f16b_44_46_48; cto_bundle=puilBl8zcFhHSUVPZ1JLbTclMkJGRjVyc2lIOVowejFxQzNrbEl5eVdBZ0RtVXJzJTJCaHNJQWwyOHhMZDdzYnQ1MGZ2RU9Oak0xYm9iOFg4QkV0V2o2RCUyQnJnJTJGdiUyRiUyQjllQmxQdXpDeU1hRSUyRldpY0clMkI3cEZTeFV4M2x2aFU1Y2JvMnFSdmRXYjVnRlp2NSUyRmJCNk5WTkZHWjNGOVpGd1EwdSUyQmFWTVFPV291VTlleEdmSERmVSUzRA; jsok=1; zgrf_id=0cc6af43-0867-4f4e-8a67-1ca0c666d31d; _gid=GA1.2.1195438955.1768160164; com.netzverb.setting.translationLanguage=ru; JSESSIONID=F0C5FB5349E327B7527447FD033E9699; g_state={\"i_l\":0,\"i_ll\":1768390339065,\"i_b\":\"0UmUFg / uDyh4THiq4HSFLWBJHpUGYAos2VqQ19lnqUg\",\"i_e\":{\"enable_itp_optimization\":0}}; _ga_8XG72KX9YD=GS2.1.s1768390154$o330$g1$t1768390743$j58$l0$h0; _ga=GA1.1.378734771.1749207043";


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

            return _document = _document ?? new HtmlParser().GetHtml(hostUrl + _word + GetAdd(_word), Cookie);
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