using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.HTMLClients;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeRuParser : LanguageParser, ILanguageParser
    {

        private Parameters parameters;
        private AIProvider ai;

        public TranslateDeRuParser(Parameters parameters)
            : base(parameters.Order, parameters.WordType)
        {
            this.parameters = parameters;
            ai = new AIProvider();
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
                var de_ = factory3.GetDe().Replace("·", "");
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
                    var tr2 = ai.GetTranslation(de);
                    if (!string.IsNullOrEmpty(tr2))
                    {
                        words[0].Ru = tr2;
                        words[0].WrdClass = _type.ToString().ToLower();
                    }

                    Console.WriteLine($"Not found {de}");
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

                sound = sounds.FirstOrDefault();



            }

            if (words[0].Found)
            {
                factory.UploadSound(sound);


                if (parameters.AddDescription)
                {
                    var factoryDwds = new DWDSTranslationContainerFactory(de, _type, true);
                    var dwds = factoryDwds.GetWords();
                    int quantity = factoryDwds.GetQuantity();
                    foreach (var word in words)
                    {
                        var cl = word.WrdClass.GetDeType() == WordType.Complex ? _type : word.WrdClass.GetDeType();


                        var res = dwds.FirstOrDefault(i => i.Type == cl);
                        if (res != null)
                        {
                            word.Description = res.GetDescription();
                        }

                        word.Quantity = quantity;
                    }
                }


                if (parameters.GetPreposition)
                {
                    var parser = new TranslateDicLeoParser(parameters);
                    parser.Parse(de, words);
                }

                if (parameters.GetExample)
                {
                    foreach (var word in words)
                    {
                        word.Example = ai.GetExample(word.De, _type, _type == WordType.Verb ? (word as Verb)?.VerbClass : "", Language.Deutsch);
                    }
                }

                if (parameters.AddAIDescription)
                {
                    foreach (var word in words)
                    {
                        word.GptDescription = ai.GetGptDescription(word.De, _type, Language.Deutsch);
                    }
                }

                if (parameters.AddWBDescription)
                {
                    foreach (var word in words)
                    {
                        var factory2 = new WikiTranslationContainerFactory(de, _type);
                        var item = factory2.GetDe();
                        if (item != null)
                        {
                            word.WBDescription = item.GetDescription();
                        }
                    }
                }
            }



            return words;
        }
    }
}