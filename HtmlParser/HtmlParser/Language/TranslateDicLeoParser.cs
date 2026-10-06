using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.HTMLClients;
using HtmlParser.Language.Model;
using HtmlParser.Language.Model.Words;

namespace HtmlParser.Language
{
    public class TranslateDicLeoParser : LanguageParser, ILanguageParser
    {
        private LeoHtmlClient leo;

        public TranslateDicLeoParser(Parameters parameters)
            : base(parameters.Order, parameters.WordType)
        {

            leo = new LeoHtmlClient();
        }

        public void Parse(IList<string> lines)
        {
            using (var sw = File.CreateText("notFound.txt"))
            {
                var temp = lines.Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(i => i.Replace("|", "").Trim())
                    .Distinct()
                    .SelectMany(l => Parse(l.Trim(), sw));


                using (var sw2 = File.CreateText("out.txt"))
                {
                    foreach (var item in temp)
                    {
                        item.Write(sw2);
                        sw2.Flush();
                    }
                }
            }
        }

        public void Parse(string de, IEnumerable<WordClass> vb)
        {
            var factory = new DicLeoContainerFactory(de, WordType.Verb, leo);

            var words = factory.GetWords();

            if (words == null || words.Count == 0)
            {
                return;
            }

            string description;
            foreach (var word in vb)
            { 
                if (IsUndefined(word))
                {
                    description = new DicLeoContainerItem
                    {
                        De = de,
                        Items = words,
                    }.GetDescription();
                }
                else if (IsReflexive(word))
                {
                    description = new DicLeoContainerItem
                    {
                        De = de,
                        Items = words.Where(w => w.IsSich).ToArray(),
                        IsSich = true
                    }.GetDescription();
                }
                else
                {
                    description = new DicLeoContainerItem
                    {
                        De = de,
                        Items = words.Where(w => !w.IsSich).ToArray()
                    }.GetDescription();
                }
                word.Prep = description;
            }

            //if (items.Any())
            //{
            //    var description = new DicLeoContainerItem
            //    {
            //        De = de,
            //        Items = items
            //    }.GetDescription();

            //    foreach (var word in vb.Where(v => !IsReflexive(v)))
            //    {
            //        word.Prep = description;
            //    }
            //}

            //if (sichItems.Any())
            //{
            //    var description = new DicLeoContainerItem
            //    {
            //        De = de,
            //        Items = sichItems,
            //        IsSich = true
            //    }.GetDescription();

            //    foreach (var word in vb.Where(v => !IsReflexive(v)))
            //    {
            //        word.Prep = description;
            //    }
            //}

            Thread.Sleep(1000);
        }

        private bool IsReflexive(WordClass v)
        {
            return v.WrdClass.GetDeType() == WordType.Verb && ((v as Verb)?.VerbClass.Contains("refl") ?? false);
        }

        private bool IsUndefined(WordClass v)
        {
            return v.WrdClass.GetDeType() == WordType.Verb && string.IsNullOrEmpty(((v as Verb)?.VerbClass));
        }

        private IList<DicLeoContainerItem> Parse(string de, StreamWriter sw)
        {
            Console.WriteLine(de);

            if (_type == WordType.Subst)
            {
                de = de.RemoveArtikles();
            }
            
            var factory = new DicLeoContainerFactory(de, _type, leo);

            var words = factory.GetWords();
            var items = words.Where(w => !w.IsSich).ToArray();
            var sichItems = words.Where(w => w.IsSich).ToArray();

            var res = new List<DicLeoContainerItem>();
            if (items.Any())
            {
                res.Add(new DicLeoContainerItem
                {
                    De = de,
                    Items = items
                });
            }

            if (sichItems.Any())
            {
                res.Add(new DicLeoContainerItem
                {
                    De = de,
                    Items = sichItems,
                    IsSich = true
                });
            }

            Thread.Sleep(3000);

            return res;
        }
    }
}