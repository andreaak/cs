using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.Extensions
{
    public class SiteLangModel
    {
        public string Level { get; set; }
        public string Description { get; set; }
        public string Example { get; set; }
        public string SoundEn { get; set; }
        public string SoundUs { get; set; }
    }


    public static class SiteExtensions
    {
        public static string[] Artikles = { "der ", "die ", "das ", "der(die) " };


        public static (string, string) GetLevelAndSound(this string word, string wordClass)
        {
            var factory = new VerbformenRuSprjazhenieTranslationContainerFactory(word, wordClass);

            string level = factory.GetLevel();
            string sound = factory.GetSound();

            return (level, sound);
        }


        public static (string, string) SetLevel(this string de, IEnumerable<WordClass> words)
        {
            if (!words.Any())
            {
                return ("", "");
            }

            var (level, sound) = ("", "");
            foreach (var w in words)
            {
                (level, sound) = GetLevelAndSound(w.De, w.WrdClass);
                w.Level = level;
            }

            return (level, sound);
        }

        public static SiteLangModel GetEnLevelAndSounds(this string word, WordType wordClass)
        {
            var factory = new DictionaryCambridgeOrgTranslationContainerFactory(word, wordClass.ToString().ToLower());

            string level = factory.GetLevel();
            string desc = factory.GetDescription();
            string soundEn = factory.GetSoundEn();
            string soundUs = factory.GetSoundUs();

            return new SiteLangModel
            {
                Level = level,
                Description = desc,
                SoundEn = soundEn,
                SoundUs = soundUs
            };
        }

        public static SiteLangModel SetEnLevel(this string de, IEnumerable<WordClass> words)
        {
            if (!words.Any())
            {
                return new SiteLangModel();
            }

            SiteLangModel res = null;
            foreach (var w in words)
            {
                var model = GetEnLevelAndSounds(de, w.WrdClass.GetEnType());
                if (res == null)
                {
                    res = model;
                }
                w.Level = model.Level;
                w.Description = model.Description;
            }

            return res;
        }

        public static void SetExample(this string de, IEnumerable<WordClass> words, string lang = "de")
        {
            if (!words.Any())
            {
                return;
            }

            foreach (var w in words)
            {
                string example;
                if (w is Verb verb)
                {
                    var request = verb.GetExampleRequest(lang);
                    var t = request.GetExample();
                    var items = t.Split(new []{"|"}, StringSplitOptions.None);
                    //int i = 0;
                    //if (items[0].Trim() == verb.De)
                    //{
                    //    i++;
                    //}
                    example = $"{NormalizeGpt(items[0])} - {NormalizeGpt(items[1])}"  ;
                    //example = w.De.GetExample(lang, WordType.Verb.GetExampleType(), verb.VerbClass);
                }
                else
                {
                    example = w.De.GetExample(lang, w.WrdClass.GetType(lang).GetExampleType());
                }

                w.Example = example;
                //Thread.Sleep(60000);
            }
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


        public static WordType GetType(this string wordClass, string lang)
        {
            switch (lang)
            {
                case "en":
                    return wordClass.GetEnType();
                case "de":
                    return wordClass.GetDeType();
                default:
                    throw new Exception("Type is not defined");
            }
        }

        public static WordType GetDeType(this string wordClass)
        {
            wordClass = wordClass?.ToLower();
            switch (wordClass)
            {
                case "subst":
                    return WordType.Subst;
                case "adj":
                    return WordType.Adj;
                case "verb":
                    return WordType.Verb;
                case "adv":
                    return WordType.Adv;
                case "präp":
                    return WordType.Prep;
                case "konj":
                    return WordType.Konj;
                case "complex":
                    return WordType.Complex;
                default:
                    return WordType.None;
            }
        }

        public static string GetDEStringType(this WordType wordClass)
        {
            switch (wordClass)
            {
                case WordType.Subst:
                    return "subst";
                case WordType.Adj:
                    return "adj";
                case WordType.Verb:
                    return "verb";
                case WordType.Adv:  
                    return "adv";
                case WordType.Prep:
                    return "präp";
                case WordType.Konj:
                    return "konj";
                default:
                    return "";
            }
        }

        public static WordType GetEnType(this string wordClass)
        {
            wordClass = wordClass?.ToLower();
            switch (wordClass)
            {
                case "n":
                case "subst":
                case "noun":
                    return WordType.Subst;
                case "adj":
                    return WordType.Adj;
                case "verb":
                case "vb":
                    return WordType.Verb;
                case "adv":
                    return WordType.Adv;
                case "prep":
                case "präp":
                    return WordType.Prep;
                case "konj":
                case "conj":
                    return WordType.Konj;
                default:
                    return WordType.None;
            }
        }

        public static string GetExampleType(this WordType type)
        {
            switch (type)
            {
                case WordType.Verb:
                    return "глагол";
                case WordType.Subst:
                    return "существительное";
                case WordType.Adv:
                    return "наречие";
                case WordType.Adj:
                    return "прилагательное";
                case WordType.Konj:
                    return "союз";
                case WordType.Prep:
                    return "предлог";
                case WordType.None:
                    return "";

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static string GetSubType(this string subType)
        {
            switch (subType?.ToLower())
            {
                case "intr":
                    return "intransitiv";
                case "trans":
                    return "transitiv";
                case "refl":
                    return "reflexiv";

                default:
                    return subType;
            }
        }

        public static void UploadSound(this string hostUrl, string word, string ext, string parentPath, bool rewrite = true)
        {
            try
            {
                var normalized = word.Normalizee();
                string folder = parentPath + "\\" + (normalized.StartsWith("!") ? normalized.Substring(0, 2) : normalized.Substring(0, 1));
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string path = $"{folder}\\{word.Normalizee()}.{ext}";

                if (!File.Exists(path) || rewrite)
                {
                    //Console.WriteLine($"Upload sound {hostUrl}");

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hostUrl);
                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                    if (resp.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream output = File.OpenWrite(path))
                        using (Stream input = resp.GetResponseStream())
                        {
                            if (input != null)
                            {
                                input.CopyTo(output);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static IList<string> GetNextValues(this HtmlNode row)
        {
            var list = new List<string>();

            var node = row.ParentNode.ParentNode;
            while (node.NextSibling != null)
            {
                list.Add(node.NextSibling.InnerText.RemoveNewLine().Trim());

                node = node.NextSibling;
            }

            return list;
        }

        public static string RemoveArtikles(this string de)
        {
            foreach (var art in Artikles)
            {
                de = de.Replace(art, "").Trim();
            }

            return de;
        } 
        
        public static string GetArtikel(this string de)
        {
            foreach (var art in Artikles)
            {
                if (de.StartsWith(art))
                {
                    return art.Trim();
                }
            }

            return "";
        }
    }
}
