using System.IO;
using System.Text;

namespace HtmlParser.Language.Model
{
    public class Verb : WordClass
    {
        public string Info { get; set; }
        public string VerbClass { get; set; }

        public override void Write(StreamWriter sw)
        {
            sw.WriteLine(Ru);
            sw.WriteLine(De);
            sw.WriteLine(!string.IsNullOrEmpty(Info) ? $"{{{Info}}}" : "");
            sw.WriteLine(DeTranscription);
            sw.WriteLine("");

            //if (!string.IsNullOrEmpty(VerbClass))
            //{
            //    sb.Append(sb.Length != 0 ? $" {VerbClass}" : VerbClass);
            //}
        }
    }

    public class IrrVerb : Verb
    {
        public string Part2Verb { get; set; }
        public override void Write(StreamWriter sw)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(De))
            {
                sb.Append($"{Part2Verb} {De}");
            }
            if (!string.IsNullOrEmpty(DeTranscription))
            {
                sb.Append($" {DeTranscription}");
            }
            sw.WriteLine(sb.ToString());
        }
    }
}