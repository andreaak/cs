using System.IO;
using System.Text;

namespace HtmlParser.Language.Model
{
    public class Substantiv : WordClass
    {
        public string Artikle { get; set; }
        public string Flexion { get; set; }
        public string Genus { get; set; }
        
        public override void Write(StreamWriter sw)
        {
            sw.WriteLine(WrdClass);
            sw.WriteLine(Ru);

            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Artikle))
            {
                sb.Append(Artikle);
            }
            if (!string.IsNullOrEmpty(De))
            {
                sb.Append(sb.Length != 0 ? $" {De}" : De);
            }
            sw.WriteLine(sb.ToString());
            sb.Clear();

            sw.WriteLine(!string.IsNullOrEmpty(Flexion) ? Flexion : Info);

            sw.WriteLine(DeTranscription);
            sw.WriteLine(Level);
            sw.WriteLine(Example);
            sw.WriteLine(Description);
            sw.WriteLine("");
            sw.WriteLine("");

        }
    }
}