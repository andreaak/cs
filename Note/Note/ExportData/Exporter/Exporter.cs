namespace Note.ExportData.Exporter
{
    public abstract class Exporter
    {
        public ExportDocTypes Format
        {
            get;
            private set;
        }

        protected Exporter(ExportDocTypes format)
	    {
            Format = format;
	    }

        public abstract void Export(string fileName, string data);

        public string GetExtension()
        {
            switch (Format)
            {
                case ExportDocTypes.Html:
                    return "html";
                case ExportDocTypes.Doc:
                    return "doc";
                case ExportDocTypes.Docx:
                    return "docx";
                case ExportDocTypes.Pdf:
                    return "pdf";
                case ExportDocTypes.Txt:
                    return "txt";
                case ExportDocTypes.Rtf:
                default:
                    return "rtf";
            }
        }
    }
}
