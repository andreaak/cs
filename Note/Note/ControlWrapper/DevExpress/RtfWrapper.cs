using DevExpress.Office.Utils;
using DevExpress.XtraRichEdit.API.Native;

namespace Note.ControlWrapper.DevExpress
{
    public class RtfWrapper : IRtfWrapper
    {
        private readonly MyRichEditControl control;
        
        public bool IsModified
        {
            get
            {
                return control.Modified;
            }
        }

        public string EditValue
        {
            get
            {
                return control.EditValue;
            }
            set
            {
                control.EditValue = value;
            }
        }

        public string PlainText
        {
            get
            {
                return control.Text;
            }
        }

        public bool Enabled
        {
            set
            {
                control.Enabled = true;
            }
        }

        public RtfWrapper(MyRichEditControl control)
        {
            this.control = control;
        }

        #region PUBLIC

        public void SetDefinitionFormat()
        {
            Document doc = control.Document;
            DocumentRange range = control.Document.Selection;
            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.FontName = "Times New Roman";
            cp.FontSize = 12;
            cp.Bold = false;
            cp.Italic = true;
            doc.EndUpdateCharacters(cp);
        }

        public void SetMethodFormat()
        {
            Document doc = control.Document;
            DocumentRange range = control.Document.Selection;
            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.FontName = "Times New Roman";
            cp.FontSize = 12;
            cp.Bold = true;
            cp.Italic = true;
            doc.EndUpdateCharacters(cp);
        }

        public void SetClassFormat()
        {
            Document doc = control.Document;
            DocumentRange range = control.Document.Selection;
            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.FontName = "Times New Roman";
            cp.FontSize = 12;
            cp.Bold = true;
            cp.Italic = false;
            doc.EndUpdateCharacters(cp);
        }

        public void SetHeaderFormat()
        {
            Document doc = control.Document;
            DocumentRange range = control.Document.Selection;
            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.FontName = "Courier New";
            cp.FontSize = 14;
            cp.Bold = true;
            cp.Italic = false;
            doc.EndUpdateCharacters(cp);
        }

        public void SetInfoFormat()
        {
            Document doc = control.Document;
            DocumentRange range = control.Document.Selection;
            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.FontName = "Times New Roman";
            cp.FontSize = 12;
            cp.Bold = false;
            cp.Italic = false;
            doc.EndUpdateCharacters(cp);
        }

        public void SetCodeFormat()
        {
            Document doc = control.Document;
            DocumentRange range = control.Document.Selection;
            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.FontName = "Courier New";
            cp.FontSize = 10;
            cp.Bold = false;
            cp.Italic = true;
            doc.EndUpdateCharacters(cp);
        }

        public void RemoveWhiteSpace()
        {
            DocumentRange range = control.Document.Selection;
            control.Document.ReplaceAll(" ", "", SearchOptions.None, range);
        }

        public void RemoveDoubleWhiteSpace()
        {
            DocumentRange range = control.Document.Selection;
            //Regex reg = new Regex("  ");
            control.Document.ReplaceAll("  ", " ", SearchOptions.None, range);
            control.Document.ReplaceAll("( )", "()", SearchOptions.None, range);
            control.Document.ReplaceAll(" () ", "() ", SearchOptions.None, range);
            control.Document.ReplaceAll("—", "-", SearchOptions.None, range);
        }

        public void RemoveLineBreak()
        {
            DocumentRange range = control.Document.Selection;
            //Regex reg = new Regex(@"\r\n");
            char ch = '\u00AD';
            control.Document.ReplaceAll("\t", "", SearchOptions.None, range);
            control.Document.ReplaceAll(ch.ToString(), "-", SearchOptions.None, range);
            control.Document.ReplaceAll("- \r\n", "", SearchOptions.None, range);
            control.Document.ReplaceAll("-\r\n", "", SearchOptions.None, range);
            control.Document.ReplaceAll("\r\n", " ", SearchOptions.None, range);
            RemoveDoubleWhiteSpace();
            Paragraph par = control.Document.GetParagraph(range.Start);
            par.Alignment = ParagraphAlignment.Justify;
            par.FirstLineIndentType = ParagraphFirstLineIndent.Indented;
            par.FirstLineIndent = 60.0f;
        }

        public void ChangeState(bool isNoteNode)
        {
            if (!isNoteNode)
            {
                Clear();
            }
            else
            {
                Document doc = control.Document;
                DocumentPosition pos = doc.Selection.Start;
                DocumentRange range = doc.CreateRange(pos, 0);
                ParagraphProperties pp = doc.BeginUpdateParagraphs(range);
                TabInfoCollection tbiColl = pp.BeginUpdateTabs(true);
                TabInfo tbi = new TabInfo();
                tbi.Alignment = TabAlignmentType.Left;
                tbi.Position = Units.InchesToDocumentsF(0.2f);
                tbiColl.Add(tbi);
                pp.EndUpdateTabs(tbiColl);
                doc.EndUpdateParagraphs(pp);
            }
            control.Enabled = isNoteNode;
        }

        public void Clear()
        {
            control.Text = string.Empty;
        }

        #endregion
    }
}
