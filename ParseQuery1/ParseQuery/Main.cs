using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using gudusoft.gsqlparser.Units;
using gudusoft.gsqlparser;
using System.Runtime.InteropServices;

namespace ParseQuery
{
    public partial class Main : Form
    {
        bool sync;
        bool syncLeft;        
        bool syncRight;
        enum FontSize
        { 
            base_,
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
        
        
        string paramProxy = "?";
        string paramProxyReg = @"\?";
        static Dictionary<string, Color> lexemColor;

        static Main()
        {
            lexemColor = new Dictionary<string, Color>();
            lexemColor.Add("select", Color.Blue);
            lexemColor.Add("insert", Color.Blue);
            lexemColor.Add("update", Color.Blue);
            lexemColor.Add("delete", Color.Blue);
            lexemColor.Add("into", Color.Blue);
            lexemColor.Add("all", Color.Purple);
            lexemColor.Add("union", Color.Purple);

            lexemColor.Add("from", Color.Green);
            lexemColor.Add("where", Color.DarkMagenta);
            lexemColor.Add("and", Color.Blue);
            lexemColor.Add("or", Color.Blue);
            lexemColor.Add("?", Color.Red);

        }
        
        public Main()
        {
            InitializeComponent();
        }

        private void setupformatoptions()
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

        private void setuphighlighterAttributes()
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
            setupformatoptions();

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
                ColorLexem(textBoxBase, lexemColor);
                ColorLexem(textBoxParse, lexemColor);
				return;
            }

            listBox1.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                listBox1.Items.Add(new Parameter(ParameterType.None, paramProxy));
            }
            this.listBox1.SelectedValueChanged -= new System.EventHandler(this.toolStripButtonParseQueryParameters_Click);
            listBox1.SelectedIndex = 0;
            ColorLexem(textBoxBase, lexemColor);
            ColorLexem(textBoxParse, lexemColor);
            //HighlightWords(textBoxBase, paramProxy, false, true, Color.Red);
           
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
            
            ColorLexem(textBoxParse, lexemColor);
            
            foreach (int position in parametersPositions.Keys)
            {
                HighlightWord(textBoxParse, parametersPositions[position], position, Color.Red, FontSize.large);
            }

            sync= true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBoxDatabases.SelectedIndex = 0;
        }

        #endregion

        #region DISPLAY
        //
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }
        //
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && this.Visible)
            {
                this.Visible = false;
                notifyIcon1.Visible = true;
            }
            else
            {
                int size = (this.ClientSize.Width - listBox1.Width) / 2 - 20;
                textBoxBase.Width = listBox1.Left - 5;
                textBoxParse.Location = new Point(listBox1.Right + 5, textBoxParse.Location.Y);
                textBoxParse.Width = this.ClientSize.Width - (listBox1.Right + 5);
            }
        }
        #endregion

        # region UTILS

        private int GetParametersNumber(string query)
        {
            Regex pattern = new Regex(string.Format("{0}", paramProxyReg));
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

        private static void HighlightWords(RichTextBox rtb, string value, bool matchCase, bool wholeWord, Color clr, FontSize fontSize)
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

            int foundStringPos = 0;            
            int foundPosition = rtb.Find(value, foundStringPos, rtb.TextLength, opt);
            while(foundPosition > -1)
            {
                HighlightWord(rtb, value, foundPosition, clr, fontSize);
                
                foundStringPos = foundPosition + value.Length;
                foundPosition = rtb.Find(value, foundStringPos, rtb.TextLength, opt);
            }
            
        }

        private static void HighlightWord(RichTextBox rtb, string value, int foundPosition, Color clr, FontSize fontSize)
        {
            rtb.SelectionStart = foundPosition;
            rtb.SelectionLength = value.Length;
            rtb.SelectionColor = clr;
            switch (fontSize)
            { 
                case FontSize.base_:
                    break;
                case FontSize.medium:
                    rtb.SelectionFont = new Font("TimesNewRoman", 12f, FontStyle.Bold);
                    break;
                case FontSize.large:
                    rtb.SelectionFont = new Font("TimesNewRoman", 14f, FontStyle.Bold);
                    break;
            }
        }

        private static void ColorLexem(RichTextBox rtb, Dictionary<string, Color> clr)
        {
            foreach (string item in clr.Keys)
            {
                HighlightWords(rtb, item, false, true, clr[item], FontSize.medium);
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
