using System.Threading;
using DevExpress.XtraPrinting;
using Note.ControlWrapper;
using DevExpress.XtraRichEdit;

namespace Note.ExportData.Exporter
{
    class PdfExporter : Exporter
    {
        private readonly RichEditControl control;

        public PdfExporter()
            : base(ExportDocTypes.Pdf)
	    {
            this.control = new RichEditControl();
        }

        public override void Export(string fileName, string data)
        {
            control.SetEditValue(data);
            PdfExportOptions options = new PdfExportOptions();
            options.ImageQuality = PdfJpegImageQuality.Medium;
            control.ExportToPdf(fileName, options);
            Thread.Sleep(1000);
        }
    }
}
