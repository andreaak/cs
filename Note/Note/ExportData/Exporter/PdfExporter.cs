using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using ExportData;

namespace Note.ControlWrapper
{
    class PdfExporter : Exporter
    {
        MyRichEditControl control;

        public PdfExporter()
            : base(ExportDocTypes.Pdf)
	    {
            this.control = new MyRichEditControl();
        }

        public override bool Export(string fileName, string data)
        {
            {
                control.EditValue = data;
                PdfExportOptions options = new PdfExportOptions();
                options.ImageQuality = PdfJpegImageQuality.Medium;
                control.ExportToPdf(fileName, options);
                Thread.Sleep(1000);
            }
            return true;
        }
    }
}
