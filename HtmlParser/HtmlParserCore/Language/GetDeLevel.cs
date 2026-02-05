using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class GetDeLevel : LanguageParser, ILanguageParser
    {

        public GetDeLevel(bool order, WordType type)
            : base(order, type)
        { }

        public void Parse(IList<string> lines)
        {
            var temp = lines.Where(l => !string.IsNullOrEmpty(l))
                .Select(l => Parse(l.Trim()));

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

        private WordClass Parse(string de)
        {
            Console.WriteLine(de);

            if (_type == WordType.Verb)
            {
                de = de.Replace("|", "");
            }

            var (level, sound) = de.GetLevelAndSound(_type.ToString().ToLower());

            return new WordClass
                {
                    De = de,
                    Level = level
                };
        }
    }
}