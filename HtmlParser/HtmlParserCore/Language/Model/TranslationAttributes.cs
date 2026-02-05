namespace HtmlParser.Language.Model
{
    public class TranslationAttributes
    {
        public string Phonetics { get; set; }//transcription

        public string Genus { get; set; }
        public string Artikle { get; set; }
        public string Flexion { get; set; }

        public string Info { get; set; }
        public string WordClass { get; set; }
        public string VerbClass { get; set; }
        public string HeadWord { get; set; }
    }
}