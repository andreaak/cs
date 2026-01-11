using System.IO;
using System.Linq;
using System.Text;

namespace HtmlParser.Language.Model
{
    public class Verb : WordClass
    {
        
        public string VerbClass { get; set; }

        public override void Write(StreamWriter sw)
        {
            var info = string.Join("; ", new[] { VerbClass/*, Info*/ }.Where(t => !string.IsNullOrEmpty(t)));

            sw.WriteLine(WrdClass);
            sw.WriteLine(Ru);
            sw.WriteLine(De);
            sw.WriteLine(GetInfo());
            sw.WriteLine(DeTranscription);
            sw.WriteLine(Level);
            sw.WriteLine(Example);
            sw.WriteLine(Description);
            sw.WriteLine("");
            sw.WriteLine("");
        }

        public string GetInfo()
        {
            if (string.IsNullOrEmpty(VerbClass))
            {
                return "";
            }
            return VerbClass.Contains("{") ? VerbClass.Trim().TrimStart('{').TrimEnd('}') : VerbClass;
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