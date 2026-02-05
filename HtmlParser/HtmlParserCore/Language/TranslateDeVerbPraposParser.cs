using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateDeVerbPraposParser : LanguageParser, ILanguageParser
    {
        private bool isCanceled;
        private Parameters parameters;

        public TranslateDeVerbPraposParser(Parameters parameters)
            : base(parameters.Order, parameters.WordType)
        {
            this.parameters = parameters;
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
                    if (isCanceled)
                    {
                        break;
                    }
                }
            }
        }

        private VerbPrapItem Parse(string line)
        {
            Console.WriteLine(line);

            Stopwatch sw = new Stopwatch();
            sw.Start();


            var item = new VerbPrapItem();
            var items = line.Trim().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            int i = 0;

            if (items[0].ToLower() == "sich")
            {
                item.Sich = true;
                i++;
            }

            item.Verb = items[i++];
            item.Prap = items[i++];
            item.Kasus = GetKasus(items[i++]);
            item.Ru = GetRu(items, i);
            var (level, sound) = item.Verb.GetLevelAndSound(_type.ToString().ToLower());
            item.Level = level;

            if (parameters.GetExample)
            {
                Task<string> task = null;
                if (parameters.WordType == WordType.Verb)
                {
                    task = Task.Run(() => item.Verb.GetExample("de", WordType.Verb.GetExampleType(), (item.Sich ? "refl " : "") + $"c предлогом {item.Prap}"));
                }
                else
                {
                    task = Task.Run(() => item.Verb.GetExample("de", parameters.WordType.GetExampleType(), $"c предлогом {item.Prap}"));
                }


                var res = Task.WaitAll(new[] { task }, TimeSpan.FromMinutes(3));
                if (!res)
                {
                    isCanceled = true;
                }
                else
                {
                    item.Example = task.Result;
                }
            }

            sw.Stop();
            if (sw.Elapsed > TimeSpan.FromMinutes(3))
            {
                isCanceled = true;
            }
           
            return item;
        }

        private string GetRu(string[] items, int i)
        {
            if (items.Length <= i)
            {
                return "";
            }

            return string.Join(" ", items.Skip(i));
        }

        private string GetKasus(string v)
        {
            var value = v.ToLower();
            return value.Contains("d") ? "Dat." : "Akk.";
        }
    }
}