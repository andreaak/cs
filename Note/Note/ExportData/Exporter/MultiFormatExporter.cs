using System.Threading;
using Note;
using Note.ControlWrapper;

namespace ExportData
{
    public class MultiFormatExporter : Exporter
    {
        MyRichEditControl control;
        DevExpress.XtraRichEdit.DocumentFormat format;
 
        public MultiFormatExporter(ExportDocTypes format)
            :base(format)
	    {
            this.control = new MyRichEditControl();
            this.format = GetFormat(Format);
	    }

        public override bool Export(string fileName, string data)
        {
            control.Data = data;
            control.SaveDocument(fileName, format);
            Thread.Sleep(1000);
            return true;
        }

        private DevExpress.XtraRichEdit.DocumentFormat GetFormat(ExportDocTypes format)
        {
            switch (format)
            {
                case ExportDocTypes.Html:
                    return DevExpress.XtraRichEdit.DocumentFormat.Html;
                case ExportDocTypes.Doc:
                    return DevExpress.XtraRichEdit.DocumentFormat.Doc;
                case ExportDocTypes.Docx:
                    return DevExpress.XtraRichEdit.DocumentFormat.OpenXml;
                case ExportDocTypes.Txt:
                    return DevExpress.XtraRichEdit.DocumentFormat.PlainText;
                case ExportDocTypes.Rtf:
                default:
                    return DevExpress.XtraRichEdit.DocumentFormat.Rtf;
            }
        }
    }
}
