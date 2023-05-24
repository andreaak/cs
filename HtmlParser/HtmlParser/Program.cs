namespace HtmlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //var parser = new MonolitParser();
            //var parser = new PidruchnikiParser();
            var parser = new TranslateRuParser();
            parser.Parse();
            //parser.Normalize();
            //ParseLocalFile();

        }
    }
}
