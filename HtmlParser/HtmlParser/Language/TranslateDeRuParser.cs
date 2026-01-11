using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeRuParser : LanguageParser, ILanguageParser
    {

        private Parameters parameters;

        public TranslateDeRuParser(Parameters parameters)
            : base(parameters.Order, parameters.WordType)
        {
            this.parameters = parameters;
        }

        public void Parse(IList<string> lines)
        {
            var temp = lines.Where(l => !string.IsNullOrEmpty(l))
                .Select(l => l.Trim())
                .Distinct()
                .SelectMany(l => Parse(l.Trim()));

            var list = _order ?
                temp.OrderBy(l => l.De).ToArray() :
                temp.ToArray();

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    item.Write(sw);
                }
            }
        }

        private IList<WordClass> Parse(string de)
        {
            Console.WriteLine(de);

            if (_type == WordType.Verb)
            {
                de = de.Replace("|", "");
            }

            de = de.RemoveArtikles();

            var factory = new PonsDeTranslationContainerFactory(de, _type);
            var words = factory.GetWords();

            string sound = null;
            if (!words[0].Found)
            {
                var factory3 = new VerbformenRuSprjazhenieTranslationContainerFactory(words[0].De, _type.ToString().ToLower());
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
                    var deNew = factory3.GetDe();
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


                factory.UploadSound();
            }

            //if (parameters.SetLevel)
            //{
            //    var sounds = new List<string>();

            //    foreach (var word in words)
            //    {
            //        var factory3 = new VerbformenRuSprjazhenieTranslationContainerFactory(word.De, word.WrdClass);
            //        var ru = factory3.GetTranslation();
            //        word.Level = factory3.GetLevel();

            //        if (string.IsNullOrEmpty(word.Ru))
            //        {
            //            word.Ru = ru;
            //        }
            //        else if (word.Ru.IsOther(ru))
            //        {
            //            word.Ru += $"(---): {word.Ru.AnotherTranslation(ru)}-!-";
            //        }
            //        sounds.Add(factory3.GetSound());
            //    }

            //    sound = sounds.FirstOrDefault(string.IsNullOrEmpty);
            //}

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


            if (parameters.GetExample)
            {
                de.SetExample(words);
            }

            return words;
        }
    }
}