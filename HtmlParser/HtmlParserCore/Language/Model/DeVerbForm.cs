using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HtmlParser.Language.Model
{
    public enum VerbForm
    {
        Prasens,
        Prateritum,
        Perfect,
        Plusquamperfekt,
        FuturI,
        FuturII,
        KonjunktivIPrasens,
        KonjunktivIPerfect,
        KonjunktivIFuturI,
        KonjunktivIFuturII,
        KonjunktivIIPrateritum,
        KonjunktivIIPlusquamperfekt,
        KonjunktivIIFuturI,
        KonjunktivIIFuturII,
        Imperativ
    }

    public class DeVerbForms
    {
        public string Value { get; set; }
        public IList<DeVerbForm> List { get; set; }
        public string Translation { get; set; }
        public string FileSuffix { get; set; }
        public VerbType VerbType { get; set; }
    }

    public enum VerbType
    {
        Trans,
        Intr,
        Refl,
        All,
    }

    public class DeVerbForm
    {
        public VerbForm Form { get; set; }
        public string Single1 { get; set; }
        public string Single2 { get; set; }
        public string Single3 { get; set; }
        public string Plural1 { get; set; }
        public string Plural2 { get; set; }
        public string Plural3 { get; set; }


        //public void Write(StreamWriter sw)
        //{
        //    var sb = new StringBuilder();
        //    Write(sb, Infinitive);
        //    Write(sb, Present);
        //    Write(sb, Prateritum);
        //    WritePart2(sb, Part2);
        //    sb.Append(!string.IsNullOrEmpty(Ru) ? Ru : "\r\n");

        //    sw.WriteLine(sb.ToString());
        //    sw.WriteLine("");
        //}

        //private void Write(StringBuilder sb, WordClass word)
        //{
        //    if (!string.IsNullOrEmpty(word.De))
        //    {
        //        sb.Append(word.De);
        //        sb.Append("\r\n");
        //        if (!string.IsNullOrEmpty(word.DeTranscription))
        //        {
        //            sb.Append(word.DeTranscription);
        //        }
        //    }
        //    else
        //    {
        //        sb.Append("\r\n\r\n");
        //    }
        //    sb.Append("\r\n");
        //}

        //private void WritePart2(StringBuilder sb, IrrVerb word)
        //{
        //    if (!string.IsNullOrEmpty(word.De))
        //    {
        //        sb.Append($"{word.Part2Verb} {word.De}");
        //        sb.Append("\r\n");
        //        if (!string.IsNullOrEmpty(word.DeTranscription))
        //        {
        //            sb.Append(word.DeTranscription);
        //        }
        //    }
        //    else
        //    {
        //        sb.Append("\r\n\r\n");
        //    }
        //    sb.Append("\r\n");
        //}
    }
}