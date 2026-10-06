using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.HTMLClients;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class GetDeWortBedeutungParser : LanguageParser, ILanguageParser
    {

        private Parameters parameters;
        private AIProvider ai;

        public GetDeWortBedeutungParser(Parameters parameters)
            : base(parameters.Order, parameters.WordType)
        {
            this.parameters = parameters;
            ai = new AIProvider();
        }

        public void Parse(IList<string> lines)
        {
            var temp = lines
                .Select(l => l.Trim())
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
                    sw.Flush();
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
            de = de
                .Replace("(in)", "")
                .Replace("(r)", "")
                .Replace("(", "")
                .Replace(")", "");
            var factory = new WikiTranslationContainerFactory(de, _type);
            Thread.Sleep(2000);
            var item = factory.GetDe();
            if (item == null)
            {
                return new List<WordClass>();
            }

            return new List<WordClass>
            {
                new WordClass()
                {
                    De = de,
                    WrdClass = _type.GetDEStringType(),
                    WBDescription = item.GetDescription()
                }
            };
        }
    }
}