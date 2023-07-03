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


            //var parser = new TranslateRuParser();
            //var parser = new TranslateDeRuParser();
            var parser = new TranslateDeRuVerbParser();
            parser.Parse();

        }
    }
}
