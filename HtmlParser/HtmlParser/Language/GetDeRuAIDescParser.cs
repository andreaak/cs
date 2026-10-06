using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.HTMLClients;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class GetDeRuAIDescParser : LanguageParser, ILanguageParser
    {
        private bool break_;
        private Language lang;
        private AIProvider ai;

        public GetDeRuAIDescParser(bool order, WordType type, string lang)
            : base(order, type)
        {
            this.lang = lang.GetLanguage();
            ai = new AIProvider(); 
        }

        public void Parse(IList<string> lines)
        {
            var temp = lines.Where(l => !string.IsNullOrEmpty(l));//.Distinct();
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
            switch (_type)
            {
                case WordType.Verb:
                    word = SetVerbDescription(de);
                    break;
                case WordType.Adv:
                case WordType.Adj:
                case WordType.Subst:
                case WordType.Konj:
                case WordType.Prep:
                    word = SetDescription(de, _type);
                    break;
                case WordType.Complex:
                    word = SetTranslation(de);
                    break;
                default:

                    break;
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

        private string last = "";

        private Verb SetVerbDescription(string de)
        {
            de = de.Replace("|", "");
            var i = de.Split(new[] { /*" ",*/ '\t' }/*, StringSplitOptions.RemoveEmptyEntries*/);
            
            de = i[0];

            if (string.IsNullOrEmpty(de))
            {
                de = last;
            }
            else
            {
                last = de;
            }

            var verb = new Verb
            {
                De = de,
                WrdClass = "verb"
            };

            if (i.Length > 1)
            {
                verb.VerbClass = i[1].Replace("{", "").Replace("}", "");
            }

            verb.GptDescription = ai.GetGptDescription(de, WordType.Verb, lang, verb.VerbClass);

            return verb;
        }

        private WordClass SetDescription(string de, WordType type)
        {
            var word = new WordClass
            {
                De = de,
                WrdClass = type.GetDEStringType()
            };

            word.GptDescription = ai.GetGptDescription(de, type, lang);

            return word;
        }

        private WordClass SetTranslation(string de)
        {
            return new WordClass
            {
                De = de,
                WrdClass = "complex",
                GptDescription = ai.GetTranslationWithInfo(de)
            };
        }
    }
}