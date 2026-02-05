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
            var temp = lines
                .Select(l => l.Trim().Trim('-' ))
                .SelectMany(l => l.Split(new[] {"-"}, StringSplitOptions.RemoveEmptyEntries))
                .Where(l => !string.IsNullOrEmpty(l))
                .Distinct()
                .ToArray();


            var list = temp.SelectMany(l => Parse(l.Trim()));

            list = _order ?
                list.OrderBy(l => l.De) :
                list;

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
                var de_ = factory3.GetDe();
                if (de.Equals(de_, StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(tr))
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
                    sound = factory3.GetSound();
                }
                else
                {
                    Console.WriteLine($"Not found {de}");
                    return words;
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

                    if (parameters.AddOtherTranslation && word.Ru.IsOther(ru) )
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

            if (words[0].Found && parameters.AddDescription)
            {
                factory.UploadSound(sound);

                var factoryDwds = new DWDSTranslationContainerFactory(de, _type);
                var dwds = factoryDwds.GetWords();

                foreach (var word in words)
                {
                    var cl = word.WrdClass.GetDeType() == WordType.Complex ? _type : word.WrdClass.GetDeType();


                    var res = dwds.FirstOrDefault(i => i.Type == cl);
                    if (res != null)
                    {
                        word.Description = res.GetDescription();
                    }
                }

            }

            if (words[0].Found && parameters.GetPreposition)
            {
                var parser = new TranslateDicLeoParser(parameters);
                parser.Parse(de, words.Where(w => w.WrdClass == "verb").Cast<Verb>().ToArray());
            }

            if (words[0].Found && parameters.GetExample)
            {
                de.SetExample(words);
            }

            return words;
        }
    }
}