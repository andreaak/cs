using System.IO;
using System.Text;

namespace HtmlParser.Language
{
    public class Verb : WordClass
    {
        public string Info { get; set; }
        public string VerbClass { get; set; }

        public override void Write(StreamWriter sw)
        {
            sw.WriteLine(Ru);

            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(De))
            {
                sb.Append(De);
            }
            if (!string.IsNullOrEmpty(Info))
            {
                sb.Append($" {{{Info}}}");
            }
            //if (!string.IsNullOrEmpty(VerbClass))
            //{
            //    sb.Append(sb.Length != 0 ? $" {VerbClass}" : VerbClass);
            //}
            sw.WriteLine(sb.ToString());

            sw.WriteLine(DeTranscription);
            sw.WriteLine("");
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