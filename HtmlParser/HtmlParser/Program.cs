using System;
using System.IO;
using System.Linq;
using HtmlParser.Language;

namespace HtmlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //var parser = new MonolitParser();
            //var parser = new PidruchnikiParser();
            //var parser = new RemoveDuplicateParser();
            //parser.Parse();
            //parser.Normalize();
            //ParseLocalFile();

            string file = "list.txt";
            var lines = File.ReadLines(file);
            var parser = GetParser(lines.First());
            parser.Parse(lines.Skip(1).ToArray());

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static ILanguageParser GetParser(string line)
        {
            var prms = line.Trim().Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
            var id = prms[0];
            bool order = prms.Length > 1 && prms[1] == "ord";
            string type = prms.Length > 2 ? prms[2] : null;
            switch (id)
            {
                case "rude": 
                    return new TranslateRuDeParser(order, type);
                case "deru": 
                    return new TranslateDeRuParser(order, type);
                case "deruverb": 
                    return new TranslateDeRuVerbParser(order, type);
                case "rudepair": 
                    return new TranslateRuDePairParser(order, type);
                default:
                    throw new Exception("wrong parameter");
            }
        }
    }
}
