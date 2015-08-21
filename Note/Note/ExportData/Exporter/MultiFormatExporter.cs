using System.Threading;
using DevExpress.XtraRichEdit;
using Note.ControlWrapper;

namespace Note.ExportData.Exporter
{
    public class MultiFormatExporter : Exporter
    {
        readonly MyRichEditControl control;
        readonly DocumentFormat format;
 
        public MultiFormatExporter(ExportDocTypes format)
            :base(format)
	    {
            this.control = new MyRichEditControl();
            this.format = GetFormat(Format);
	    }

        public override void Export(string fileName, string data)
        {
            control.EditValue = data;
            control.SaveDocument(fileName, format);
            Thread.Sleep(1000);
        }

        private DocumentFormat GetFormat(ExportDocTypes format)
        {
            switch (format)
            {
                case ExportDocTypes.Html:
                    return DocumentFormat.Html;
                case ExportDocTypes.Doc:
                    return DocumentFormat.Doc;
                case ExportDocTypes.Docx:
                    return DocumentFormat.OpenXml;
                case ExportDocTypes.Txt:
                    return DocumentFormat.PlainText;
                default:
                    return DocumentFormat.Rtf;
            }
        }
    }
}
