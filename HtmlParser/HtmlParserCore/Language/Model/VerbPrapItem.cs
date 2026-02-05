using System.IO;

namespace HtmlParser.Language.Model
{
    public class VerbPrapItem
    {
        public bool Sich { get; set; }
        public string Verb { get; set; }
        public string Prap { get; set; }
        public string Kasus { get; set; }
        public string Ru { get; set; }
        public string Example { get; set; }
        public string Level { get; set; }

        public virtual void Write(StreamWriter sw)
        {
            sw.WriteLine(Verb);
            sw.WriteLine(Sich ? "sich" : "" );
            sw.WriteLine(Prap);
            sw.WriteLine(Kasus);
            sw.WriteLine("");
            sw.WriteLine(Ru);
            sw.WriteLine(Example);
            sw.WriteLine(Level);
            sw.WriteLine("");
        }
    }
}