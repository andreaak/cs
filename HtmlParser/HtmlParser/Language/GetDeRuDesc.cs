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

            //list = list.Select(i => i.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries))
            //    .Select(ar => new { A = ar[0], B = ar.Length > 1 ? ar[1] : "" })
            //    .GroupBy(ob => ob.A)
            //    .Select(gr => gr.Key + "\t" + string.Join(",", gr.Select(g => g.B)))
            //    .ToArray();

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
            else if (_type == WordType.Adv)
            {
                word = SetAdverbDescription(de);
            }
            else if (_type == WordType.Adj)
            {
                word = SetAdjDescription(de);
            }
            else if (_type == WordType.Subst)
            {
                word = SetSubstDescription(de);
            }
            else if (_type == WordType.Konj)
            {
                word = SetKonjDescription(de);
            }
            else if (_type == WordType.Prep)
            {
                word = SetPrepDescription(de);
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
            var i = de.Split(new[] { /*" ",*/ "\t" }, StringSplitOptions.RemoveEmptyEntries);
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
                string langItem;
                switch (lang)
                {
                    case "de":
                        langItem = "немецкого";
                        break;
                    case "en":
                        langItem = "английского";
                        break;
                    default:
                        return null;
                }
                string request = $"значение и типы {langItem} глагола {de} {verb.VerbClass} с переводом на русский, примерами, синонимами, антонимами, предложное управление, транскрипция и также уровень слова";

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
        
        private WordClass SetAdverbDescription(string de)
        {
            string part = "наречия";
            return SetDescription(de, part, "adv");
        }

        private WordClass SetAdjDescription(string de)
        {
            string part = "прилагательного";
            return SetDescription(de, part, "adj");
        }

        private WordClass SetSubstDescription(string de)
        {
            string part = "существительного";
            return SetDescription(de, part, "subst");
        }

        private WordClass SetKonjDescription(string de)
        {
            string part = "союза";
            return SetDescription(de, part, "konj");
        }

        private WordClass SetPrepDescription(string de)
        {
            string part = "предлога";
            return SetDescription(de, part, "präp");
        }

        private WordClass SetDescription(string de, string part, string wordClass)
        {
            var word = new WordClass
            {
                De = de,
                WrdClass = wordClass 
            };

            try
            {
                string langItem;
                switch (lang)
                {
                    case "de":
                        langItem = "немецкого";
                        break;
                    case "en":
                        langItem = "английского";
                        break;
                    default:
                        return null;
                }
                string request = $"значение {langItem} {part} {de} с переводом на русский, примерами, синонимами, антонимами, предложное управление, транскрипция и также уровень слова"; 
                var res = request.GetGPTResponse();

                word.Description = res;
                return word;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                break_ = true;
                return null;
            }

        }
    }
}