using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeRuVerbPrefixParser : LanguageParser, ILanguageParser
    {
        private bool isCanceled;
        
        private static string[] prefixes =
        {
            "ab", "an", "auf", "aus", "bei", "ein", "fest", "her", "hin", "los", "mit", "nach", "teil", "vor", "weg", 
            "zu","zurück","zusammen","wider",
            "über","unter","durch","um","wieder","weiter",
            "be","emp","ent","er","ge","miss","ver","zer",
            "auseinander", "dran", "entgegen", "fort", "gefangen", "heim", "heran", "heraus", "herein", "herunter", "hierher",
            "hinauf", "hinaus", "hinein", "hinüber", "hinunter", "hinzu", "hoch", "krumm", "leicht", "mal", "ran", "raus",
            "rein", "ruber", "schwer", "streng", "übel", "voraus", "vorlieb", "vorweg", "wahr", "wiederauf", "wunder",
            "bevor", "statt", "verab", "voraus", "abbe", "herunter", "hoch", "vorbei", "abbe", "bean", "aufbe", "entgegen", "dar", 
            "veran","zuvor", "in", "not", "un", "nachver", "voll", "fehl", "nieder", "frei", "beur", "beauf", "nahe", "berück", "beab", "hervor", "de", "beibe", "bezu",
            "offen","verein","verei","zufrieden","fertig","beein","voran","vernach","hoch","wohl","anzu","verun", "vorüber", "befür"
        };

        private static string[] selectPrefixes =
        {
            "ab", "an", "auf", "aus", "bei", "ein", "fest", "her", "hin", "los", "mit", "nach", "teil", "vor", "weg", 
            "zu","zurück","zusammen","wider",
            "über","unter","durch","um","wieder","weiter",
            "be","emp","ent","er","ge","miss","ver","zer",
            "auseinander", "dran", "entgegen", "fort", "gefangen", "heim", "heran", "heraus", "herein", "herunter", "hierher",
            "hinauf", "hinaus", "hinein", "hinüber", "hinunter", "hinzu", "hoch", "krumm", "leicht", "mal", "ran", "raus",
            "rein", "ruber", "schwer", "streng", "übel", "voraus", "vorlieb", "vorweg", "wahr", "wiederauf", "wunder",
            "bevor", "statt", "verab", "voraus", "abbe", "herunter", "hoch", "vorbei", "abbe", "bean", "aufbe", "entgegen", "dar", 
            "veran","zuvor", "in", "not", "un", "nachver", "voll", "fehl", "nieder", "frei", "beur", "beauf", "nahe", "berück", "beab", "hervor", "de", "beibe", "bezu",
            "offen","verein","verei","zufrieden","fertig","beein","voran","vernach","hoch","wohl","anzu","verun", "vorüber", "befür"
        };

        private Parameters parameters;

        public TranslateDeRuVerbPrefixParser(Parameters parameters)
            : base(parameters.Order, parameters.WordType)
        {
            this.parameters = parameters;
        }

        public void Parse(IList<string> lines)
        {
            var existing = GetExistingVerbs();

            using (var sw = File.CreateText("notFound.txt"))
            {
                var temp = lines.Where(l => !string.IsNullOrWhiteSpace(l.Trim()))
                    .Select(l => l.Replace("|", ""))
                    .Distinct();


                temp = _order ?
                    temp.OrderBy(l => l).ToArray() :
                    temp.ToArray();


                var t = temp.Select(l => l.Trim().Split(new []{" "}, StringSplitOptions.None)[0]).Distinct().ToArray();

                var newItems = t.Where(w => !existing.Any(w.EndsWith)).ToArray();
                var withPref = newItems.Where(i => prefixes.Any(i.StartsWith)).ToArray();
                var noPref = newItems.Where(i => !prefixes.Any(i.StartsWith)).ToArray();
                var withPref2 = withPref.Where(i => !noPref.Any(i.EndsWith)).ToArray();

                using (var sw2 = File.CreateText("out.txt"))
                {
                    foreach (var line  in noPref.Concat(withPref2).OrderBy(i => i))
                    {
                        var words = Parse(line.Trim(), sw);
                        foreach (var word in words)
                        {
                            word.Write(sw2);
                        }

                        if (isCanceled)
                        {
                            return;
                        }
                    }
                }

                //var list = noPref.Concat(withPref2).OrderBy(i => i).SelectMany(l => Parse(l.Trim(), sw)).ToArray();

                //using (var sw2 = File.CreateText("out.txt"))
                //{
                //    foreach (var item in list)
                //    {
                //        item.Write(sw2);
                //    }
                //}
            }
        }

        private IList<string> GetExistingVerbs()
        {
            var names = Directory.GetFiles(Constants.VerbFolder).Select(f => f.Replace(Constants.VerbFolder + "\\verb_", "").Replace(".xml", "").DeNormalize()).ToArray();
            //return names;
            return new List<string>();
        }

        private IList<WordClass> Parse(string line, StreamWriter notFoundSw)
        {
            var deBase = line.Trim();
            Console.WriteLine(deBase);
            if (IsIgnoreItem(deBase))
            {
                return new List<WordClass>();
            }

            var list = new List<WordClass>();

            var baseWords = GetWords(deBase);
            if (baseWords.Any(w => w.Found))
            {
                baseWords.First().WrdClass += " $$1";
                deBase.SetLevel(baseWords); 
                if(parameters.GetExample)
                {
                    var task = Task.Run(() => deBase.SetExample(baseWords));

                    var res = Task.WaitAll(new [] { task }, TimeSpan.FromMinutes(3));
                    if (!res)
                    {
                        isCanceled = true;
                        return list;
                    }
                }
            }

            foreach (var prefix in selectPrefixes)
            {
                var de = prefix + deBase;

                Console.WriteLine(de);

                var words = GetWords(de);

                if (words.Any(w => w.Found))
                {
                    de.SetLevel(words);
                    if (parameters.GetExample)
                    {
                        var task = Task.Run(() => de.SetExample(words));

                        var res = Task.WaitAll(new[] { task }, TimeSpan.FromMinutes(3));
                        if (!res)
                        {
                            isCanceled = true;
                            return list;
                        }
                    }
                    list.AddRange(words);
                }

            }

            var ordered = list.OrderBy(w => w.De).ToList();
            ordered.InsertRange(0, baseWords);
            return ordered;
        }

        private IList<WordClass> GetWords(string de)
        {
            var factory = new PonsDeTranslationContainerFactory(de, _type);
            var words = factory.GetWords();

            string sound = null;
            if (!words[0].Found)
            {
                var factory3 = new VerbformenRuSprjazhenieTranslationContainerFactory(words[0].De, _type.ToString().ToLower());
                var deNew = factory3.GetDe();
                if (deNew.Replace("·", "|").ToLower() == de)
                {
                    var tr = factory3.GetTranslation();
                    if (!string.IsNullOrEmpty(tr))
                    {


                        if (_type == WordType.Subst)
                        {
                            var value = new Substantiv
                            {
                                Artikle = factory3.GetArtikel(),
                            };

                            words[0] = value;
                        }

                        words[0].Ru = tr;

                        if (_type == WordType.Verb)
                        {
                            deNew = deNew.Replace("·", "|");
                        }

                        words[0].De = deNew;
                        words[0].WrdClass = _type.ToString().ToLower();
                        words[0].Found = true;
                        words[0].Level = factory3.GetLevel();
                    }
                    sound = factory3.GetSound();
                }
            }
            else
            {
                var sounds = new List<string>();

                foreach (var word in words)
                {
                    var factory3 = new VerbformenRuSprjazhenieTranslationContainerFactory(word.De, word.WrdClass);
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


            if (words[0].Found)
            {
                factory.UploadSound(sound);

                var factoryDwds = new DWDSTranslationContainerFactory(de, _type);
                var dwds = factoryDwds.GetWords();

                foreach (var word in words)
                {
                    var res = dwds.FirstOrDefault(i => i.Type == word.WrdClass.GetDeType());
                    if (res != null)
                    {
                        word.Description = res.GetDescription();
                    }
                }

            }



            return words;
        }

        private bool IsIgnoreItem(string de)
        {
            return de.Contains("|") /*|| prefixes.Any(de.StartsWith)*/;
        }
    }
}