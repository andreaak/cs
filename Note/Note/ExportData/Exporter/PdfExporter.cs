using System.Threading;
using DevExpress.XtraPrinting;
using Note.ControlWrapper;

namespace Note.ExportData.Exporter
{
    class PdfExporter : Exporter
    {
        public PdfExporter()
            : base(ExportDocTypes.Pdf)
	    {
        }

        public override void Export(string fileName, string data)
        {
            control.SetEditValue(data);
            PdfExportOptions options = new PdfExportOptions();
            options.ImageQuality = PdfJpegImageQuality.Medium;
            control.ExportToPdf(fileName, options);
            Thread.Sleep(SleepTime);
        }
    }
}
