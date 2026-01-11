using System.IO;

namespace HtmlParser.Language.Model
{
    public class WordClass
    {
        public string Ru { get; set; }
        public string De { get; set; }
        public string DeTranscription { get; set; }
        public string WrdClass { get; set; }
        public string Level { get; set; }
        public string Example { get; set; }
        public string Description { get; set; }
        public bool Found { get; set; }
        public string Info { get; set; }

        public virtual void Write(StreamWriter sw)
        {
            sw.WriteLine(WrdClass);
            sw.WriteLine(Ru);
            sw.WriteLine(De);
            sw.WriteLine(Info);
            sw.WriteLine(DeTranscription);
            sw.WriteLine(Level);
            sw.WriteLine(Example);
            sw.WriteLine(Description);
            sw.WriteLine("");
            sw.WriteLine("");
        }
    }
}