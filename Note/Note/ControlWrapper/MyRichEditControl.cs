using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;

namespace Note.ControlWrapper
{
    public class MyRichEditControl : RichEditControl
    {
        private MyRichEditControl richEditControl;

        public MyRichEditControl()
        {
            richEditControl = this;
        }
        
        public string Data
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
                            richEditControl.WordMLText = data;
                            break;
                        case DocTypes.Rtf:
                            richEditControl.RtfText = data;
                            break;
                        case DocTypes.Html:
                            richEditControl.HtmlText = data;
                            break;
                        case DocTypes.Mht:
                            richEditControl.MhtText = data;
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
            return !string.IsNullOrEmpty(data) && data.Contains("<?mso-application progid=\"Word.Document\"?>");
        }

        private static bool IsRtf(string data)
        {
            return !string.IsNullOrEmpty(data) && data.StartsWith(@"{\rtf1");
        }

        public string GetConvertedData(string data, DocTypes inType, DocTypes outType)
        {
            BackupData();
            SetData(data);

            string res = GetData(outType);

            RestoreData();
            return res;
        }

        private string GetData(DocTypes outType)
        {
            string res;
            switch (outType)
            {
                case DocTypes.Doc:
                    res = richEditControl.WordMLText;
                    break;
                case DocTypes.Rtf:
                    res = richEditControl.RtfText;
                    break;
                case DocTypes.Html:
                    res = richEditControl.HtmlText;
                    break;
                case DocTypes.Mht:
                    res = richEditControl.MhtText;
                    break;
                default:
                    res = "";
                    break;
            }
            return res;
        }

        string data;
        public void BackupData()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(BackupData));
            }
            else
            {
                data = this.Data;
                this.BeginUpdate();
            }
        }

        public void RestoreData()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(RestoreData));
            }
            else
            {
                this.Data = data;
                this.EndUpdate();
            }
        }

    }


}
