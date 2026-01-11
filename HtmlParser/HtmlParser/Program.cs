using System;
using System.IO;
using System.Linq;
using HtmlParser.Language;
using HtmlParser.Language.Containers;

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

            var parameters = ParametersProvider.Parse(line);

            switch (parameters.Action)
            {
                //case "rude": 
                //    return new TranslateRuDeParser(parameters.Order, parameters.WordType);
                case "deru": 
                    return new TranslateDeRuParser(parameters);
                case "derulist": 
                    return new TranslateDeRuVocabularyCornelseParser(parameters.Order);
                case "deruverb": 
                    return new TranslateDeRuVerbFormsParser();
                case "deverbprapparser":
                    return new TranslateDeVerbPraposParser(parameters);
                case "rudepair": 
                    return new TranslateRuDePairParser(parameters.Order, parameters.WordType);
                case "deverbform":
                    return new DeVerbFormParser();
                case "deverbprap":
                    return new DeVerbDicLeoPrapParser(parameters.Order);
                case "deruverbpref":
                    return new TranslateDeRuVerbPrefixParser(parameters);
                case "deruverbfile":
                    return new TranslateDeRuVerbFilesParser();
                case "deruverbfilesetlevel":
                    return new TranslateDeRuVerbFilesSetLevel();
                case "deruverbfilereparse":
                    return new TranslateDeRuVerbFilesReparser();
                case "example":
                    return new GetDeRuExample(parameters.Order, parameters.WordType, parameters.Lang);
                case "delevel":
                    return new GetDeLevel(parameters.Order, parameters.WordType);
                case "dwds":
                    return new TranslateDWDSVocabularyParser(parameters);
                case "dicleo":
                    return new TranslateDicLeoParser(parameters);
                
                
                case "enru": 
                    return new TranslateEnRuParser(parameters);
                case "enlevel":
                    return new GetEnLevel(parameters.Order, parameters.WordType);
                case "enruverb":
                    return new TranslateEnRuIrregVerbFormsParser();
                case "enrulist":
                    return new TranslateEnRuVocabularyParser(parameters);
                case "enruphrasal":
                    return new TranslateEnRuPhrasalVerbParser(parameters);
                case "enrulistsimple":
                    return new TranslateEnRuVocabularySimpleParser(parameters);

                default:
                    throw new Exception("wrong parameter");
            }
        }
    }
}
