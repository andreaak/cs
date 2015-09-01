using DevExpress.XtraRichEdit;

namespace Note.ExportData.Exporter
{
    public abstract class Exporter
    {
        protected readonly RichEditControl control;
        protected const int SleepTime = 200;

        public ExportDocTypes Format
        {
            get;
            private set;
        }

        protected Exporter(ExportDocTypes format)
	    {
            Format = format;
            this.control = new RichEditControl();
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
