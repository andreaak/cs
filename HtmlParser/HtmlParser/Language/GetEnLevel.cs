using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class GetEnLevel : LanguageParser, ILanguageParser
    {
        public GetEnLevel(bool order, WordType type)
            : base(order, type)
        { }

        public void Parse(IList<string> lines)
        {
            var temp = lines.Where(l => !string.IsNullOrEmpty(l))
                .Select(l => Parse(l.Trim()))
                .Where(l => l != null);

            var list = _order ?
                temp.OrderBy(l => l.De) :
                temp;

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    item.Write(sw);
                    sw.Flush();
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

            var model = de.GetEnLevelAndSounds(_type);

            if (string.IsNullOrEmpty(model.Level))
            {
                return null;
            }

            var wc = _type.GetDEStringType();

            return new WordClass
            {
                De = de,
                Level = model.Level,
                WrdClass = wc
            };
        }
    }
}