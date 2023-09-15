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

            if (!string.IsNullOrEmpty(Genus))
            {
                sb.Append(sb.Length != 0 ? $" {Genus}" : Genus);
            }
            if (!string.IsNullOrEmpty(Flexion))
            {
                sb.Append(sb.Length != 0 ? $" {Flexion}" : Flexion);
            }
            sw.WriteLine(sb.ToString());

            sw.WriteLine(DeTranscription);
            sw.WriteLine("");
        }
    }
}