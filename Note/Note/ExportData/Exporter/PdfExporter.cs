using System.Threading;
using DevExpress.XtraPrinting;
using Note.ControlWrapper;

namespace Note.ExportData.Exporter
{
    class PdfExporter : Exporter
    {
        private readonly MyRichEditControl control;

        public PdfExporter()
            : base(ExportDocTypes.Pdf)
	    {
            this.control = new MyRichEditControl();
        }

        public override void Export(string fileName, string data)
        {
            control.EditValue = data;
            PdfExportOptions options = new PdfExportOptions();
            options.ImageQuality = PdfJpegImageQuality.Medium;
            control.ExportToPdf(fileName, options);
            Thread.Sleep(1000);
        }
    }
}
