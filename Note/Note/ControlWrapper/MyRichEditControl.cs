using System;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;

namespace Note.ControlWrapper
{
    public class MyRichEditControl : RichEditControl
    {
        private const string WordPrefix = "<?mso-application progid=\"Word.Document\"?>";
        private const string RtfPrefix = @"{\rtf1";
        
        public string EditValue
        {
            get
            {
                return GetData(Note.Options.DbFormatType);
            }
            set
            {
                SetData(value);
            }
        }

        private string GetData(DocTypes outType)
        {
            switch (outType)
            {
                case DocTypes.Doc:
                    return WordMLText;
                case DocTypes.Rtf:
                    return RtfText;
                case DocTypes.Html:
                    return HtmlText;
                case DocTypes.Mht:
                    return MhtText;
                default:
                    return string.Empty;
            }
        }

        private void SetData(string data)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<string>(SetData), data);
            }
            else
            {
                if (IsRtf(data))
                {
                    this.RtfText = data;
                }
                else if (IsWord(data))
                {
                    this.WordMLText = data;
                }
                else
                {
                    switch (Note.Options.DbFormatType)
                    {
                        case DocTypes.Doc:
                            WordMLText = data;
                            break;
                        case DocTypes.Rtf:
                            RtfText = data;
                            break;
                        case DocTypes.Html:
                            HtmlText = data;
                            break;
                        case DocTypes.Mht:
                            MhtText = data;
                            break;
                    }
                }
                //Document.Unit = DevExpress.Office.DocumentUnit.Inch;
                Document.Sections[0].Page.PaperKind = System.Drawing.Printing.PaperKind.A4;
                Document.Sections[0].Margins.Top =
                Document.Sections[0].Margins.Bottom =
                Document.Sections[0].Margins.Left =
                Document.Sections[0].Margins.Right = 200f;

            }
        }

        private static bool IsWord(string data)
        {
            return !string.IsNullOrEmpty(data) && data.Contains(WordPrefix);
        }

        private static bool IsRtf(string data)
        {
            return !string.IsNullOrEmpty(data) && data.StartsWith(RtfPrefix);
        }
    }
}
