using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using HtmlParser.Language;
using Newtonsoft.Json;

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

            var request = Temp();

            var conv = JsonConvert.SerializeObject(request);

            if (request.Content is StringContent ct)
            {
                var t = ct.ReadAsStringAsync().GetAwaiter().GetResult();
                var t2 = ct.ReadAsStringAsync().GetAwaiter().GetResult();
            }

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

            var type = GetType(prms);

            switch (id)
            {
                case "rude": 
                    return new TranslateRuDeParser(order, type);
                case "deru": 
                    return new TranslateDeRuParser(order, type);
                case "derulist": 
                    return new TranslateDeRuVocabularyCornelseParser(order);
                case "deruverb": 
                    return new TranslateDeRuVerbFormsParser();
                case "rudepair": 
                    return new TranslateRuDePairParser(order, type);
                case "deverbform":
                    return new DeVerbFormParser();
                case "deverbprap":
                    return new DeVerbDicLeoPrapParser(order);
                case "deruverbpref":
                    return new TranslateDeRuVerbPrefixParser();

                default:
                    throw new Exception("wrong parameter");
            }
        }

        private static WordType GetType(string[] prms)
        {
            return Enum.TryParse<WordType>(prms.ElementAtOrDefault(2) ?? "", true, out var type) ? 
                type : 
                WordType.First;
        }

        private static HttpRequestMessage Temp()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("https://apigee.rwlv.dev/rwlv-dev-oc-syn")
            };

            var contentType = "application/xml";

            var commandString = "Test Command";

            request.Content =
                new StringContent(commandString, Encoding.UTF8, contentType);

            request.Method = HttpMethod.Post;

            request.Headers.Add("Authorization", $"Bearer Test");

            return request;
        }
    }
}
