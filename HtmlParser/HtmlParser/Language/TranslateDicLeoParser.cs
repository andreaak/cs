using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;

namespace HtmlParser.Language
{
    public class TranslateDicLeoParser : LanguageParser, ILanguageParser
    {
        public TranslateDicLeoParser(Parameters parameters)
            : base(parameters.Order, parameters.WordType)
        { }

        public void Parse(IList<string> lines)
        {
            
            using (var sw = File.CreateText("notFound.txt"))
            {
                var temp = lines.Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(i => i.Replace("|", "").Trim())
                    .Distinct()
                    .SelectMany(l => Parse(l.Trim(), sw));


                //using (var sw2 = File.CreateText("out.txt"))
                //{
                //    foreach (var line in list)
                //    {
                //        var item = Parse(line);
                //        item.Write(sw2);
                //        sw.Flush();
                //        if (break_)
                //        {
                //            break;
                //        }
                //    }
                //}


                using (var sw2 = File.CreateText("out.txt"))
                {
                    foreach (var item in temp)
                    {
                        item.Write(sw2);
                    }
                }
            }
        }

        private IList<DicLeoContainerItem> Parse(string de, StreamWriter sw)
        {
            Console.WriteLine(de);

            if (_type == WordType.Subst)
            {
                de = de.RemoveArtikles();
            }
            
            var factory = new DicLeoContainerFactory(de, _type);

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

            Thread.Sleep(1000);

            return res;
        }
    }
}