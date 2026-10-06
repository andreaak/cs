using System;
using System.Collections.Generic;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace Note.ExportData
{
    public class DocxExporter : IExporter2
    {
        public void Convert(string outputFile, IList<(string, string)> rtfs)
        {
            Word.Application wordApp = null;
            Word.Document mergedDoc = null;

            try
            {
                wordApp = new Word.Application();
                wordApp.Visible = false;

                mergedDoc = wordApp.Documents.Add();
                Word.Style normalStyle = mergedDoc.Styles[Word.WdBuiltinStyle.wdStyleNormal];

                normalStyle.ParagraphFormat.SpaceAfter = 0; 
                normalStyle.ParagraphFormat.SpaceBefore = 0;

                foreach ((string name, string rtf) in rtfs)
                {
                    InsertText(mergedDoc, name);

                    Word.Range range = mergedDoc.Content;
                    range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                    string tempFile = Path.GetTempFileName() + ".rtf";
                    File.WriteAllText(tempFile, rtf);

                    range.ParagraphFormat.SpaceAfter = 0;
                    range.ParagraphFormat.SpaceBefore = 0;
                    range.Font.Reset();

                    range.InsertFile(tempFile);

                    // Разрыв страницы после каждого документа
                    range = mergedDoc.Content;
                    range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                    range.InsertBreak(Word.WdBreakType.wdPageBreak);

                    File.Delete(tempFile);
                }

                mergedDoc.SaveAs2(
                    outputFile,
                    Word.WdSaveFormat.wdFormatXMLDocument);
            }
            finally
            {
                if (mergedDoc != null)
                {
                    mergedDoc.Close(false);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(mergedDoc);
                }

                if (wordApp != null)
                {
                    wordApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();

            }
        }

        public string[] GetExtensions()
        {
            return new[]
            {
                "Docx Files (*.docx)|*.docx|",
                "Doc Files (*.doc)|*.doc|",
                "All Files (*.*)|*.*"
            };
        }

        static void InsertText(Word.Document doc, string text)
        {
            Word.Range r = doc.Content;
            r.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

            r.InsertAfter(text);

            Word.Range title = doc.Range(r.Start, r.Start + text.Length);

            title.Font.Bold = 1;
            title.Font.Italic = 1;
            title.Font.Size = 14;
            title.ParagraphFormat.SpaceAfter = 0;

            // Создаём новый чистый абзац
            title.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
            title.InsertParagraphAfter();
            title.InsertParagraphAfter();
        }
    }
}
