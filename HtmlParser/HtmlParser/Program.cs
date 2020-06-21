namespace HtmlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //var parser = new MonolitParser();
            var parser = new PidruchnikiParser();
            parser.Parse();
            //parser.Normalize();
            //ParseLocalFile();

        }
    }
}
