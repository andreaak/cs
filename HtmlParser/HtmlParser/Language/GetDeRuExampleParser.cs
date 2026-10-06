using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.HTMLClients;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class GetDeRuExampleParser : LanguageParser, ILanguageParser
    {
        private bool break_;
        private Language lang;
        private AIProvider ai;



        public GetDeRuExampleParser(bool order, WordType type, string lang)
            : base(order, type)
        {
            this.lang = lang.GetLanguage();
            ai = new AIProvider();
        }

        public void Parse(IList<string> lines)
        {
            var temp = lines.Where(l => !string.IsNullOrEmpty(l)).Distinct();
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
                word = new WordClass
                {
                    De = de,
                    WrdClass = _type.ToString().ToLower()
                };

                string ex = ai.GetExample(de, _type, lang: lang);
                word.Example = ex;
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
            var i = de.Split(new[] {"\t" }, StringSplitOptions.RemoveEmptyEntries);
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
                string ex = ai.GetExample(de, WordType.Verb, verb.VerbClass, lang: lang);
                verb.Example = ex;
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