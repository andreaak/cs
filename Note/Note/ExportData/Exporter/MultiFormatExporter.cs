﻿using System;
using System.Threading;
using DevExpress.XtraRichEdit;
using Note.ControlWrapper;

namespace Note.ExportData.Exporter
{
    public class MultiFormatExporter : Exporter
    {
        private readonly DocumentFormat format;
 
        public MultiFormatExporter(ExportDocTypes format)
            :base(format)
	    {
            this.format = GetFormat(Format);
	    }

        public override void Export(string fileName, string data)
        {
            try
            {
                control.SetEditValue(data);
                control.SaveDocument(fileName, format);
            }
            catch (Exception e)
            {
            }

            Thread.Sleep(SleepTime);
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
                case ExportDocTypes.Epub:
                    return DocumentFormat.ePub;
                default:
                    return DocumentFormat.Rtf;
            }
        }
    }
}
