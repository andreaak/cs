using System;
using System.Text.RegularExpressions;
using DevExpress.Data.Mask;
using DevExpress.Office.Utils;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;

namespace Note.ControlWrapper.DevExpressWrappers
{
    public class RtfWrapper : IRtfWrapper
    {
        private readonly RichEditControl control;
        
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
                return control.GetEditValue();
            }
            set
            {
                control.SetEditValue(value);
            }
        }

        public string PlainText
        {
            get
            {
                return control.Text;
            }
        }

        public string HtmlText
        {
            get
            {
                return control.HtmlText;
            }
        }

        public bool Enabled
        {
            set
            {
                control.Enabled = value;
            }
        }

        public RtfWrapper(RichEditControl control)
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
            SetMethodFormat(doc, range);
        }

        public void SetMethodFormat(Document doc, DocumentRange range)
        {
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
            cp.FontName = "Consolas";
            cp.FontSize = 9.5f;
            cp.Bold = false;
            cp.Italic = false;
            doc.EndUpdateCharacters(cp);
        }

        public void RemoveWhiteSpace()
        {
            DocumentRange range = control.Document.Selection;
            control.Document.ReplaceAll(" ", "", SearchOptions.None, range);
        }

        public void RemoveDoubleWhiteSpace()
        {
            RemoveSpecialCombinations();
            RemoveDoubleWhiteSpace_();
            FormatParagraphs();
        }

        public void RemoveLineBreak()
        {
            DocumentRange selection = control.Document.Selection;

            control.Document.ReplaceAll("- \r\n", "", SearchOptions.None, selection);
            control.Document.ReplaceAll("-\r\n", "", SearchOptions.None, selection);
            control.Document.ReplaceAll("\r\n", " ", SearchOptions.None, selection);
            control.Document.ReplaceAll("\t", "", SearchOptions.None, selection);

            RemoveSpecialCombinations();
            string pattern = @"\p{Cc}";

            Regex myRegEx = new Regex(pattern);
            control.Document.ReplaceAll(myRegEx, " ", selection);
            RemoveDoubleWhiteSpace_();

            FormatParagraphs();
            FormatParagraph();
            FormatText();
        }

        private void RemoveDoubleWhiteSpace_()
        {
            DocumentRange selection = control.Document.Selection;

            control.Document.ReplaceAll("  ", " ", SearchOptions.None, selection);
        }

        private void FormatParagraphs()
        {
            Document doc = control.Document;
            DocumentRange range = control.Document.Selection;
            ParagraphProperties par = doc.BeginUpdateParagraphs(range);
            par.FirstLineIndentType = ParagraphFirstLineIndent.Indented;
            par.LeftIndent = 0;
            par.RightIndent = 0;
            //par.FirstLineIndent = 0.0f;
            par.SpacingAfter = 0;
            par.SpacingBefore = 0;
            par.LineSpacingType = ParagraphLineSpacing.Single;
            par.Alignment = ParagraphAlignment.Justify;
            doc.EndUpdateParagraphs(par);
        }

        private void FormatParagraph()
        {
            Document doc = control.Document;
            DocumentRange range = control.Document.Selection;
            ParagraphProperties par = doc.BeginUpdateParagraphs(range);
            par.FirstLineIndentType = ParagraphFirstLineIndent.Indented;
            par.FirstLineIndent = 60.0f;
            par.Alignment = ParagraphAlignment.Justify;
            doc.EndUpdateParagraphs(par);
        }

        

        private void RemoveSpecialCombinations()
        {
            DocumentRange selection = control.Document.Selection;

            control.Document.ReplaceAll("( )", "()", SearchOptions.None, selection);
            control.Document.ReplaceAll(" () ", "() ", SearchOptions.None, selection);
            control.Document.ReplaceAll("—", "-", SearchOptions.None, selection);
            control.Document.ReplaceAll("( ", "(", SearchOptions.None, selection);
            control.Document.ReplaceAll(" )", ")", SearchOptions.None, selection);
            control.Document.ReplaceAll("[ ", "[", SearchOptions.None, selection);
            control.Document.ReplaceAll(" ]", "]", SearchOptions.None, selection);
            control.Document.ReplaceAll("< ", "<", SearchOptions.None, selection);
            control.Document.ReplaceAll(" >", ">", SearchOptions.None, selection);
            control.Document.ReplaceAll("\" ", "\"", SearchOptions.None, selection);
            control.Document.ReplaceAll(" \"", "\"", SearchOptions.None, selection);
            control.Document.ReplaceAll(" ,", ",", SearchOptions.None, selection);
            control.Document.ReplaceAll(" ;", ";", SearchOptions.None, selection);
            control.Document.ReplaceAll(" :", ":", SearchOptions.None, selection);
            control.Document.ReplaceAll(" .", ".", SearchOptions.None, selection);
            control.Document.ReplaceAll('\u00AD'.ToString(), "-", SearchOptions.None, selection);
            control.Document.ReplaceAll('\u000B'.ToString(), "\n", SearchOptions.None, selection);
        }

        private void FormatText()
        {
            DocumentRange selection = control.Document.Selection;

            Regex reg = new Regex(@"<(\w+)>");
            SetFormating(reg, selection, SetMethodFormat);
            reg = new Regex(@"</(\w+)>");
            SetFormating(reg, selection, SetMethodFormat);
            reg = new Regex(@"<(\w+)\s?/>");
            SetFormating(reg, selection, SetMethodFormat);
        }

        private void SetFormating(Regex reg, DocumentRange selection, Action<Document, DocumentRange> act)
        {
            var ranges = control.Document.FindAll(reg);
            foreach (var range in ranges)
            {
                if (selection.Start <= range.Start && range.End <= selection.End)
                {
                    act(control.Document, range);
                }
            }
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
