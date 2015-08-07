using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using gudusoft.gsqlparser.Units;
using gudusoft.gsqlparser;
using System.Runtime.InteropServices;
using GrabDatabase.ParseQuery;

namespace ParseQuery
{
    public partial class ParseQueryForm : Form
    {
        private const string PARAM_PROXY = "?";
        private const string PARAM_PROXY_REG = @"\?";
        private bool sync;
        private bool syncLeft;
        private  bool syncRight;
        
        enum FontSize
        { 
            small,
            medium,
            large,
        }

        public enum ScrollBarType : uint
        {
            SbHorz = 0,
            SbVert = 1,
            SbCtl = 2,
            SbBoth = 3
        }

        public enum Message : uint
        {
            WM_VSCROLL = 0x0115
        }

        public enum ScrollBarCommands : uint
        {
            SB_THUMBPOSITION = 4
        }

        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        
        
        public ParseQueryForm()
        {
            InitializeComponent();
        }

        private void SetupFormatOptions()
        {
            //for more format options, please check document

            lzbasetype.gFmtOpt.Select_Columnlist_Style = TAlignStyle.asStacked;
            lzbasetype.gFmtOpt.Select_Columnlist_Comma = TLinefeedsCommaOption.lfAfterComma;
            lzbasetype.gFmtOpt.SelectItemInNewLine = false;
            lzbasetype.gFmtOpt.AlignAliasInSelectList = true;
            lzbasetype.gFmtOpt.TreatDistinctAsVirtualColumn = false;

            //setup more format options ...

            lzbasetype.gFmtOpt.linenumber_enabled = false;
        }

        private void SetupHighlighterAttributes()
        {
            lzbasetype.gFmtOpt.HighlightingFontname = "Courier New";
            lzbasetype.gFmtOpt.HighlightingFontsize = 10;

            //for other elements you want to customize, please check document
            lzbasetype.gFmtOpt.HighlightingElements[(int)TLzHighlightingElement.sfkIdentifer].SetForegroundInRGB("#008000");
            lzbasetype.gFmtOpt.HighlightingElements[(int)TLzHighlightingElement.sfkIdentifer].StyleBold = true;
            lzbasetype.gFmtOpt.HighlightingElements[(int)TLzHighlightingElement.sfkIdentifer].StyleItalic = false;
            lzbasetype.gFmtOpt.HighlightingElements[(int)TLzHighlightingElement.sfkIdentifer].StyleStrikeout = false;
            lzbasetype.gFmtOpt.HighlightingElements[(int)TLzHighlightingElement.sfkIdentifer].StyleUnderline = false;

            //setup more elements attributes ....
        }

        //private void btnFormatSqlToHtml_Click(object sender, EventArgs e)
        //{
        //    tabControl1.SelectedIndex = 1;
        //    setupformatoptions();
        //    setuphighlighterAttributes();

        //    TGSqlParser sqlparser = new TGSqlParser(getDBVendor());
        //    sqlparser.SqlText.Text = inputsql.Text;
        //    webBrowser1.DocumentText = sqlparser.ToHtml(TOutputFmt.ofhtml);

        //}

        //private void btnFormatSQLInRTF_Click(object sender, EventArgs e)
        //{
        //    tabControl1.SelectedIndex = 2;
        //    setupformatoptions();
        //    setuphighlighterAttributes();

        //    TGSqlParser sqlparser = new TGSqlParser(getDBVendor());
        //    sqlparser.SqlText.Text = inputsql.Text;
        //    //richTextBox1.Text = sqlparser.ToRTF(TOutputFmt.ofrtf);
        //    richTextBox1.Rtf = sqlparser.ToRTF(TOutputFmt.ofrtf);

        //}

        # region HANDLERS

        private void btnFormatSQL_Click(object sender, EventArgs e)
        {
            SetupFormatOptions();

            TGSqlParser sqlparser = new TGSqlParser(getDBVendor());
            sqlparser.SqlText.Text = textBoxBase.Text;
            int i = sqlparser.PrettyPrint();
            textBoxBase.Clear();  
            if (i == 0)
            {
                string outStr =
                textBoxBase.Text = sqlparser.FormattedSqlText.Text;
            }
            else
            {
                textBoxBase.Text = sqlparser.ErrorMessages;
            }
        }

        private void toolStripButtonGetParameters_Click(object sender, EventArgs e)
        {
            sync = false;
          
            btnFormatSQL_Click(null, null);

            int count = GetParametersNumber(textBoxBase.Text);
            if (count == 0)
            {
                textBoxParse.Clear();
                textBoxParse.Text = textBoxBase.Text;
            }
            else
            {

                listBox1.Items.Clear();
                for (int i = 0; i < count; i++)
                {
                    listBox1.Items.Add(new Parameter(ParameterType.None, PARAM_PROXY));
                }
                this.listBox1.SelectedValueChanged -= new System.EventHandler(this.toolStripButtonParseQueryParameters_Click);
                listBox1.SelectedIndex = 0;
            }
            ColorLexem(textBoxBase, Options.LexemColors);
            ColorLexem(textBoxParse, Options.LexemColors);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string data = listBox1.SelectedItem.ToString();
            InputData form = new InputData(listBox1.SelectedItem);
            if (form.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items[listBox1.SelectedIndex] = form.Data;
                toolStripButtonParseQueryParameters_Click(null, null);
            }
        }

        private void toolStripButtonParseQueryParameters_Click(object sender, EventArgs e)
        {
            Dictionary<int, string> parametersPositions;
            textBoxParse.Clear();
            textBoxParse.Text = GetQueryWithParams(textBoxBase.Text, listBox1.Items, out parametersPositions);

            ColorLexem(textBoxParse, Options.LexemColors);
            
            foreach (int position in parametersPositions.Keys)
            {
                HighlightSentense(textBoxParse, position, parametersPositions[position].Length, Color.Red, FontSize.large);
            }

            sync= true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBoxDatabases.SelectedIndex = 0;
        }

        #endregion

        # region UTILS

        private int GetParametersNumber(string query)
        {
            Regex pattern = new Regex(string.Format("{0}", PARAM_PROXY_REG));
            int count = pattern.Matches(query).Count;
            return count;
        }

        public static String GetQueryWithParams(String temp, IList prms, out Dictionary<int, string> positions)
	    {
            positions = new Dictionary<int, string>();
            int index = 0;
		    String paramProxy = "?";
		    foreach(object prm_ in prms)
		    {
                Parameter prm = prm_ as Parameter;
                if (prm == null)
                {
                    continue;
                }

                String insertStr = null;
			    if(prm.Type == ParameterType.String )
			    {
				    insertStr = "'" + prm.Value + "'";
			    }
			    else
			    {
                    insertStr = prm.Value.ToString(); ;
			    }
                index = temp.IndexOf(paramProxy, index);
			    if(index == -1)
			    {
				    break;
			    }
                temp = temp.Remove(index, paramProxy.Length).Insert(index, insertStr);
                positions.Add(index, insertStr);
                ++index;
		    }
            return temp;
	    }
 
        private TDbVendor getDBVendor()
        {
            TDbVendor dbVendor = TDbVendor.DbVMssql;
            switch (toolStripComboBoxDatabases.SelectedIndex)
            {
                case 1:
                    dbVendor = TDbVendor.DbVMssql;
                    break;
                case 0:
                    dbVendor = TDbVendor.DbVOracle;
                    break;
                case 2:
                    dbVendor = TDbVendor.DbVDB2;
                    break;
                case 3:
                    dbVendor = TDbVendor.DbVMysql;
                    break;
                case 4:
                    dbVendor = TDbVendor.DbVAccess;
                    break;
                default:
                    break;
            }

            return dbVendor;
        }
 
        #endregion

        # region RTF

        private static void ColorLexem(RichTextBox rtb, IDictionary<string, Color> clr)
        {
            foreach (string item in clr.Keys)
            {
                HighlightWord(rtb, item, true, true, clr[item], FontSize.medium);
            }
            ColorComments(rtb, Options.CommentsColor, FontSize.small);
        }

        private static void HighlightWord(RichTextBox rtb, string word, bool matchCase, bool wholeWord, Color clr, FontSize fontSize)
        {
            RichTextBoxFinds opt = RichTextBoxFinds.NoHighlight;
            if (matchCase)
            {
                opt |= RichTextBoxFinds.MatchCase;
            }
            if (wholeWord)
            {
                opt |= RichTextBoxFinds.WholeWord;
            }

            int startFromPosition = 0;            
            int startPosition = rtb.Find(word, startFromPosition, rtb.TextLength, opt);
            while(startPosition > -1)
            {
                HighlightSentense(rtb, startPosition, word.Length, clr, fontSize);
                startFromPosition = startPosition + word.Length;
                startPosition = rtb.Find(word, startFromPosition, rtb.TextLength, opt);
            }
        }

        private static void ColorComments(RichTextBox rtb, Color color, FontSize fontSize)
        {
            string startComment = @"/*";
            string endComment = @"*/";
            color = HighlightSentense(rtb, color, fontSize, startComment, endComment);
            startComment = @"--";
            endComment = "\r";
            color = HighlightSentense(rtb, color, fontSize, startComment, endComment);
        }

        private static Color HighlightSentense(RichTextBox rtb, Color color, FontSize fontSize, string startSymbol, string endSymbol)
        {
            RichTextBoxFinds opt = RichTextBoxFinds.None;
            int startFromPosition = 0;
            int startPosition = rtb.Find(startSymbol, startFromPosition, rtb.TextLength, opt);
            while (startPosition > -1 && startFromPosition < rtb.TextLength)
            {
                int secondPosition = rtb.Find(endSymbol, startPosition + startSymbol.Length, rtb.TextLength, opt);
                int length = secondPosition > -1 ? secondPosition - startPosition + endSymbol.Length : rtb.TextLength - startPosition;
                HighlightSentense(rtb, startPosition, length, color, fontSize);

                startFromPosition = startPosition + length;
                startPosition = rtb.Find(startSymbol, startFromPosition, rtb.TextLength, opt);
            }
            return color;
        }

        private static void HighlightSentense(RichTextBox rtb, int startPosition,int length, Color clr, FontSize fontSize)
        {
            rtb.SelectionStart = startPosition;
            rtb.SelectionLength = length;
            rtb.SelectionColor = clr;
            switch (fontSize)
            { 
                case FontSize.small:
                    rtb.SelectionFont = Options.SmallFont;
                    break;
                case FontSize.medium:
                    rtb.SelectionFont = Options.MediumFont;
                    break;
                case FontSize.large:
                    rtb.SelectionFont = Options.LargeFont;
                    break;
            }
        }

        #endregion

        # region SCROLL

        private void textBoxBase_VScroll(object sender, EventArgs e)
        {
            if (!sync || !syncLeft)
            {
                return;
            }
            try
            {
                int nPos = GetScrollPos(textBoxBase.Handle, (int)ScrollBarType.SbVert);
                nPos <<= 16;
                uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
                SendMessage(textBoxParse.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
            }
            catch (Exception)
            {
 
            }

        }

        private void textBoxParse_VScroll(object sender, EventArgs e)
        {
            if (!sync || !syncRight)
            {
                return;
            }
            try
            {
                int nPos = GetScrollPos(textBoxParse.Handle, (int)ScrollBarType.SbVert);
                
                nPos <<= 16;
                uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
                SendMessage(textBoxBase.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
            }
            catch (Exception)
            {
 
            }
        }

        private void toolStripButtonSyncLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (toolStripButtonSyncLeft.Checked)
            {
                toolStripButtonSyncRight.Checked = false;
                syncLeft = true;
                syncRight = false;
            }
        }

        private void toolStripButtonSyncRight_CheckedChanged(object sender, EventArgs e)
        {
            if (toolStripButtonSyncRight.Checked)
            {
                toolStripButtonSyncLeft.Checked = false;
                syncLeft = false;
                syncRight = true;
            }
        }

        #endregion
    }
}
