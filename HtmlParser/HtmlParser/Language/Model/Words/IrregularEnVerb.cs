using System.IO;
using System.Text;

namespace HtmlParser.Language.Model
{
    public class IrregularEnVerb
    {
        public WordClass Infinitive { get; set; }
        public WordClass Past { get; set; }
        public WordClass Part2 { get; set; }
        public string Ru { get; set; }
        public string Level { get; set; }

        public void Write(StreamWriter sw)
        {
            //var sb = new StringBuilder();
            //Write(sb, Infinitive);
            //Write(sb, Past);
            //Write(sb, Part2);
            //sb.Append(!string.IsNullOrEmpty(Ru) ? Ru : "\r\n");
            //sb.Append("\r\n");
            //sb.Append(!string.IsNullOrEmpty(Level) ? Level : "\r\n");
            //sw.WriteLine(sb.ToString());
            //sw.WriteLine("");

            sw.WriteLine(Infinitive.De);
            sw.WriteLine(Infinitive.DeTranscription);
            sw.WriteLine(Past.De);
            sw.WriteLine(Past.DeTranscription);
            sw.WriteLine(Part2.De);
            sw.WriteLine(Part2.DeTranscription);
            sw.WriteLine(Ru);
            sw.WriteLine(Level);
            sw.WriteLine("");
        }

        private void Write(StringBuilder sb, WordClass word)
        {
            if (!string.IsNullOrEmpty(word.De))
            {
                sb.Append(word.De);
                sb.Append("\r\n");
                if (!string.IsNullOrEmpty(word.DeTranscription))
                {
                    sb.Append(word.DeTranscription);
                }
            }
            else
            {
                sb.Append("\r\n\r\n");
            }
            sb.Append("\r\n");
        }
    }
}