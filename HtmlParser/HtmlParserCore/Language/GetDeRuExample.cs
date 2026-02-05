using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class GetDeRuExample : LanguageParser, ILanguageParser
    {
        private bool break_;
        private string lang;


        public GetDeRuExample(bool order, WordType type, string lang)
            : base(order, type)
        {
            this.lang = lang;
        }

        public void Parse(IList<string> lines)
        {
            var temp = lines.Where(l => !string.IsNullOrEmpty(l));
            var list = _order ?
                temp.OrderBy(l => l).ToArray() :
                temp.ToArray();

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var line in list)
                {
                    var item = Parse(line);
                    item.Write(sw);
                    sw.Flush();
                    if (break_)
                    {
                        break;
                    }
                }
            }
        }

        private WordClass Parse(string de)
        {
            Console.WriteLine(de);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            WordClass word = null;
            if (_type == WordType.Verb)
            {
                word = SetVerbExample(de);
            }
            else
            {
                de.SetExample(new List<WordClass>
                {
                    new WordClass
                    {
                        De = de,
                    }
                }, lang);
            }

            sw.Stop();
            if (sw.Elapsed > TimeSpan.FromMinutes(3))
            {
                break_ = true;
            }

            if (word != null)
            {
                return word;
            }


            var wordNotFound = new WordClass
            {
                De = de
            };

            sw.Stop();
            return wordNotFound;
        }

        private Verb SetVerbExample(string de)
        {
            de = de.Replace("|", "");
            var i = de.Split(new[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            de = i[0];

            var verb = new Verb
            {
                De = i[0],

            };

            if (i.Length > 1)
            {
                verb.VerbClass = i[1].Replace("{", "").Replace("}", "");
            }

            try
            {
                de.SetExample(new List<Verb> { verb }, lang);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                break_ = true;
                return null;
            }

            return verb;
        }
    }
}