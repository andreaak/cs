using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Note.ControlWrapper
{
    public abstract class Exporter
    {
        public ExportDocTypes Format
        {
            get;
            private set;
        }

        public Exporter(ExportDocTypes format)
	    {
            Format = format;
	    }

        public abstract bool Export(string fileName, string data);

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
