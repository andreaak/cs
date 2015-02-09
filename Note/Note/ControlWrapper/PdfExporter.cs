using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Note.ControlWrapper
{
    class PdfExporter : Exporter
    {
        SautinSoft.PdfMetamorphosis pdfConverter = new SautinSoft.PdfMetamorphosis();
        MyRichEditControl richEditControl;
        public PdfExporter(ExportDocTypes format, MyRichEditControl richEditControl)
            :base(format)
	    {
            this.richEditControl = richEditControl;
	    }

        public override bool Export(string fileName, string data)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document,
              new FileStream(fileName, FileMode.Create)
              );

            HTMLWorker worker = new HTMLWorker(document);
            document.Open();

            worker.StartDocument();
            worker.Parse(new StringReader(
                richEditControl.GetConvertedData(data, DocTypes.Rtf, DocTypes.Html)));
            worker.EndDocument();
            worker.Close();
            document.Close();
            //
            //int i = pdfConverter.RtfToPdfConvertStringToFile(data, fileName);
            return true;
        }
    }
}
