using System.IO;
using System.Text;

namespace HtmlParser.Language
{
    public class IrregularDeVerb 
    {
        public WordClass Infinitive { get; set; }
        public WordClass Present { get; set; }
        public WordClass Prateritum { get; set; }
        public IrrVerb Part2 { get; set; }
        public string Ru { get; set; }


        public void Write(StreamWriter sw)
        {
            var sb = new StringBuilder();
            Write(sb, Infinitive);
            Write(sb, Present);
            Write(sb, Prateritum);
            WritePart2(sb, Part2);
            sb.Append(!string.IsNullOrEmpty(Ru) ? Ru : "\r\n");

            sw.WriteLine(sb.ToString());
            sw.WriteLine("");
        }

        private void Write(StringBuilder sb, WordClass word)
        {
            if (!string.IsNullOrEmpty(word.De))
            {
                sb.Append(word.De);
                if (!string.IsNullOrEmpty(word.DeTranscription))
                {
                    sb.Append($" {word.DeTranscription}");
                }
            }
            else
            {
                sb.Append("\r\n");
            }
            sb.Append("\r\n");
        }

        private void WritePart2(StringBuilder sb, IrrVerb word)
        {
            if (!string.IsNullOrEmpty(word.De))
            {
                sb.Append($"{word.Part2Verb} {word.De}");
                if (!string.IsNullOrEmpty(word.DeTranscription))
                {
                    sb.Append($" {word.DeTranscription}");
                }
            }
            else
            {
                sb.Append("\r\n");
            }
            sb.Append("\r\n");
        }
    }
}