using System.IO;

namespace HtmlParser.Language.Model
{
    public class WordClass
    {
        public string Ru { get; set; }
        public string De { get; set; }
        public string DeTranscription { get; set; }

        public virtual void Write(StreamWriter sw)
        {
            sw.WriteLine(Ru);
            sw.WriteLine(De);
            sw.WriteLine("");
            sw.WriteLine(DeTranscription);
            sw.WriteLine("");
        }
    }
}