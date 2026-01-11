using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeRuVocabularyCornelseParser : LanguageParser, ILanguageParser
    {
        
        
        public TranslateDeRuVocabularyCornelseParser(bool order)
            : base(order, WordType.All)
        { }

        public void Parse(IList<string> lines)
        {
            
            using (var sw = File.CreateText("notFound.txt"))
            {
                var temp = lines.Where(l => !string.IsNullOrWhiteSpace(l))
                    .SelectMany(l => Parse(l.Trim(), sw));


                var list = _order ?
                    temp.OrderBy(l => l.De).ToArray() :
                    temp.ToArray();
                
                using (var sw2 = File.CreateText("out.txt"))
                {
                    foreach (var item in list)
                    {
                        item.Write(sw2);
                    }
                }
            }
        }

        private IList<WordClass> Parse(string line, StreamWriter sw)
        {
            var item = GetItem(line);
            Console.WriteLine(item.De);

            item.De = item.De.Replace("_", " ");

            if (item.Type == WordType.Verb)
            {
                item.De = item.De.Replace("|", "");
            }


            var factory = new PonsDeTranslationContainerFactory(item.De, item.Type);
            var words = factory.GetWords();

            string sound = null;
            if (!words.Any(w => w.Found))
            {
                var factory3 = new VerbformenRuSprjazhenieTranslationContainerFactory(item.De, words[0].WrdClass);

                if (item.Type == WordType.Subst)
                {
                    var value = new Substantiv
                    {
                        De = item.De,
                        Artikle = factory3.GetArtikel(),
                        WrdClass = "subst"
                    };
                    if (string.IsNullOrEmpty(value.Artikle))
                    {
                        value.Artikle = item.Artikel;
                    }

                    words[0] = value;
                }
                else if (item.Type == WordType.Verb)
                {
                    words[0].De = string.IsNullOrEmpty(item.Sich) ? words[0].De : item.Sich + " " + words[0].De;
                }
                words[0].Ru = factory3.GetTranslation();
                words[0].Level = factory3.GetLevel();
                sound = factory3.GetSound();
            }
            else
            {
                var sounds = new List<string>();
                
                foreach (var word in words)
                {
                    var factory3 = new VerbformenRuSprjazhenieTranslationContainerFactory(item.De, word.WrdClass);
                    var ru = factory3.GetTranslation();
                    word.Level = factory3.GetLevel();

                    if (word.Ru.IsOther(ru))
                    {
                        word.Ru += $"(---): {word.Ru.AnotherTranslation(ru)}-!-";
                    }
                    sounds.Add(factory3.GetSound());
                }

                sound = sounds.FirstOrDefault(string.IsNullOrEmpty);
            }

            factory.UploadSound(sound);

            return words;
        }

        private ListItem GetItem(string de)
        {
            de = de.Trim();


            IList<string> items;
            ListItem listItem;
            if (char.IsUpper(de[0]) || SiteExtensions.Artikles.Any(a => de.StartsWith(a)))
            {
                var artikel = de.GetArtikel();

                de = de.RemoveArtikles();
                var value = de;
                if (value.EndsWith("/in"))
                {
                    value = value.Replace("/in", "(in)");
                } else if (value.EndsWith("r/-e"))
                {
                    value = value.Replace("r/-e", "(r)");
                }
                listItem = new ListItem
                {
                    De = value,
                    Type = WordType.Subst,
                    Artikel = artikel
                };
            }
            else if(de.StartsWith("sich"))
            {
                listItem = new ListItem
                {
                    De = de.Replace("sich", "").Trim(),
                    Type = WordType.Verb,
                    Sich = "sich"
                };
            }
            else if (de.Contains("|") || de.Contains("verb"))
            {
                listItem = new ListItem
                {
                    De = de,
                    Type = WordType.Verb
                };
            }
            else
            {
                listItem = new ListItem
                {
                    De = de,
                    Type = WordType.All
                };
            }

            return listItem;
        }
    }

    public class ListItem
    {
        public string De { get; set; }
        public WordType Type { get; set; }
        public string Artikel { get; set; }
        public string Sich { get; set; }
    }
}