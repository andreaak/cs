using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.Extensions
{
    public static class FileExtensions
    {
        private static readonly ReplaceItem[] NORMALIZATION = { new ReplaceItem("ä", "!a") ,
            new ReplaceItem("ö", "!o"),
            new ReplaceItem("ü", "!u"),
            new ReplaceItem("ß", "!s")};

        public static string Normalizee(this string word)
        {
            word = word.ToLower();
            foreach (var symbol in NORMALIZATION)
            {
                if (word.Contains(symbol.Source))
                {
                    word = word.Replace(symbol.Source, symbol.Dest);
                }
            }
            return word;
        }

        public static string DeNormalize(this string word)
        {
            word = word.ToLower();
            foreach (var symbol in NORMALIZATION)
            {
                if (word.Contains(symbol.Dest))
                {
                    word = word.Replace(symbol.Dest, symbol.Source);
                }
            }
            return word;
        }

        public static IList<Verb> ParseFile(this string file)
        {
            var xml = XDocument.Load(file);

            // Query the data and write out a subset of contacts
            var verbs = xml.Root.Descendants("word").Select(c => new Verb
            {
                De = c.Element("de").Value,
                Ru = (c.Element("ru")?.Value ?? "")
                    .Replace("\r", " ")
                    .Replace("\n", " ")
                    .Replace("  ", " ")
                    .Replace("/ ", "/"),
                DeTranscription = c.Element("de_tr")?.Value,
                WrdClass = c.Element("de_wordclass")?.Value,
                VerbClass = c.Element("de_info")?.Value,
                Level = c.Element("de_level")?.Value
            }).ToArray();

            return verbs;
        }

        public static IList<WordClass> ParseFirstEntity(this string file)
        {
            var xml = XDocument.Load(file);

            // Query the data and write out a subset of contacts
            var c = xml.Root.Descendants("word").First();


            var verb = new Verb
            {
                De = c.Element("de").Value,
                Ru = (c.Element("ru")?.Value ?? "").Replace("\r", " ").Replace("\n", " "),
                DeTranscription = c.Element("de_tr")?.Value,
                WrdClass = c.Element("de_wordclass")?.Value,
                VerbClass = c.Element("de_info")?.Value,
                Level = c.Element("de_level")?.Value
            };

            return new WordClass[] { verb };
        }

        public static string GetVerbRequest(this Verb word, string lang)
        {
            var value = word.De.Replace("|", "");
            string request;

            request = $"переводы на русский \r\n" +
                      $"пример использования слова в предложении на немецком, \r\n" +
                      $"перевод предложения на русский,\r\n" +
                      $"уровень слова, транскрипция";
            
            
            if (string.IsNullOrEmpty(word.VerbClass))
            {
                request += $", \r\nтип {GetLanguageRequest(lang)} глагола {value}.";
            }
            else
            {
                request += $" {GetLanguageRequest(lang)} {word.VerbClass} глагола {value}.";
            }

            request += "Значения выдать в одну строку в указанном порядке с разделителем |.";
            return request;
        }

        public static string GetExampleRequest(this Verb word, string lang)
        {
            var value = word.De.Replace("|", "");
            string request;

            request = $"пример использования слова в предложении на немецком, \r\n" +
                      $"перевод предложения на русский";


            if (string.IsNullOrEmpty(word.VerbClass))
            {
                request += $" {GetLanguageRequest(lang)} глагола {value}.";
            }
            else
            {
                request += $" {GetLanguageRequest(lang)} {word.VerbClass} глагола {value}.";
            }

            request += "Значения выдать в одну строку в указанном порядке с разделителем |.";
            return request;
        }

        private static string GetLanguageRequest(string lang)
        {
            switch (lang)
            {
                case "de":
                    return "немецкого";
                case "en":
                    return "английского";
                default:
                    throw new ArgumentException("");
            }
            
        }

        public static string GetExample(this string word, string lang, string type, string subType = null)
        {
            var gpt = new GPT();
            word = word.Replace("|", "");

            string request;
            if (lang == "en")
            {
                request = string.IsNullOrEmpty(type) ?
                    $"1 простой пример без описания со словом '{word}' на английском с переводом на русский" :
                    $"1 простой пример без описания с {type} '{word}' на английском с переводом на русский";
            }
            else
            {
                request = string.IsNullOrEmpty(type) ? 
                    $"1 простой пример без описания со словом '{word}' на немецком с переводом на русский" : 
                    $"1 простой пример без описания с {type} '{word}' {subType.GetSubType()} на немецком с переводом на русский";
            }

            return gpt.GetResponse(request);
        }

        public static string GetExample(this string request)
        {
            var gpt = new GPT();
            return gpt.GetResponse(request);
        }

        public static void SaveToFile(this string verb, IList<Verb> verbs)
        {
            var fileName = Constants.Verb1Folder + "\\verb_" + verb.Trim().Normalizee() + ".xml";
            var el = new XElement("words",
                verbs.Select(v => new XElement("word",
                    string.IsNullOrEmpty(v.Ru) ? null : new XElement("ru", v.Ru.SplitString()),
                    new XElement("de", v.De),
                    string.IsNullOrEmpty(v.DeTranscription) ? null : new XElement("de_tr", v.DeTranscription),
                    string.IsNullOrEmpty(v.VerbClass) ? null : new XElement("de_info", v.VerbClass),
                    new XElement("de_wordclass", v.WrdClass),
                    string.IsNullOrEmpty(v.Level) ? null : new XElement("de_level", v.Level)
                ))
            );

            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                using (var swOut = new StreamWriter(stream, new UTF8Encoding(false)))
                {
                    el.Save(swOut);
                }
            }
        }
    }
}