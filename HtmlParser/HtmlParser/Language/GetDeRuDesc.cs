using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class GetDeRuDesc : LanguageParser, ILanguageParser
    {
        private bool break_;
        private string lang;


        public GetDeRuDesc(bool order, WordType type, string lang)
            : base(order, type)
        {
            this.lang = lang;
        }

        public void Parse(IList<string> lines)
        {
            var temp = lines.Where(l => !string.IsNullOrEmpty(l)).Distinct();
            var list = _order ?
                temp.OrderBy(l => l).ToArray() :
                temp.ToArray();

            list = list.Select(i => i.Split(new[] {" ", "\t"}, StringSplitOptions.RemoveEmptyEntries))
                .Select(ar => new {A = ar[0], B = ar.Length > 1 ? ar[1] : ""})
                .GroupBy(ob => ob.A)
                .Select(gr => gr.Key + "\t" + string.Join(",", gr.Select(g => g.B)))
                .ToArray();

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
                word = SetVerbDescription(de);
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
                Console.WriteLine("Timeout");
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

        private Verb SetVerbDescription(string de)
        {
            de = de.Replace("|", "");
            var i = de.Split(new[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            de = i[0];

            var verb = new Verb
            {
                De = i[0],
                WrdClass = "verb"
            };

            if (i.Length > 1)
            {
                verb.VerbClass = i[1].Replace("{", "").Replace("}", "");
            }

            try
            {
                string request;
                if (lang == "de")
                {
                    request =
                        $"значение и типы немецкого глагола {de} {verb.VerbClass} с переводом на русский, примерами, синонимами, антонимами, предложное управление и также уровень слова";

                }
                else
                {
                    request =
                        $"значение и типы английского глагола {de} {verb.VerbClass} с переводом на русский, примерами, синонимами, антонимами, предложное управление и также уровень слова";
                }


                var res = request.GetGPTResponse();

                verb.Description = res;
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