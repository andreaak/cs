using DevExpress.XtraRichEdit;

namespace Note.ControlWrapper
{
    public static class MyRichEditControl
    {
        private const string WordPrefix = "<?mso-application progid=\"Word.Document\"?>";
        private const string RtfPrefix = @"{\rtf1";
        
        public static string GetEditValue(this RichEditControl control)
        {
            switch (Note.Options.DbFormatType)
            {
                case DocTypes.Doc:
                    return control.WordMLText;
                case DocTypes.Rtf:
                    return control.RtfText;
                case DocTypes.Html:
                    return control.HtmlText;
                case DocTypes.Mht:
                    return control.MhtText;
                default:
                    return string.Empty;
            }
        }

        public static void SetEditValue(this RichEditControl control, string data)
        {
            //if (control.InvokeRequired)
            //{
            //    control.Invoke(new Action<string>(control.SetEditValue), data);
            //}
            //else
            //{
                if (IsRtf(data))
                {
                    control.RtfText = data;
                }
                else if (IsWord(data))
                {
                    control.WordMLText = data;
                }
                else
                {
                    switch (Note.Options.DbFormatType)
                    {
                        case DocTypes.Doc:
                            control.WordMLText = data;
                            break;
                        case DocTypes.Rtf:
                            control.RtfText = data;
                            break;
                        case DocTypes.Html:
                            control.HtmlText = data;
                            break;
                        case DocTypes.Mht:
                            control.MhtText = data;
                            break;
                    }
                }
                //Document.Unit = DevExpress.Office.DocumentUnit.Inch;
                control.Document.Sections[0].Page.PaperKind = System.Drawing.Printing.PaperKind.A4;
                control.Document.Sections[0].Margins.Top =
                control.Document.Sections[0].Margins.Bottom =
                control.Document.Sections[0].Margins.Left =
                control.Document.Sections[0].Margins.Right = 200f;
            //}
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
