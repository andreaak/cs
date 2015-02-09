using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Assist
{
    public delegate void GoToLineDelegate(int line);
    public partial class RTFTextControl : UserControl
    {
        public event GoToLineDelegate GoToLine;
        protected virtual void OnGoToLine(int line)
        {
            if (GoToLine != null)
                GoToLine(line);
        }

        string fontType;
        float fontSize;
        FontStyle fontStyle;
        Color color;

        string fontType2;
        float fontSize2;
        FontStyle fontStyle2;
        Color color2;

        public RTFTextControl()
        {
            InitializeComponent();
        }

        public void SetTextInControl(List<SelectedLine> values, bool matchCase, bool wholeWord)
        {
            richTextBox1.Text = "";
            ReadDataFromReg();
            
            Logger.WriteLine(string.Format("Method: SetTextInControl"));
            int prevLine = -1;
            List<int> lnesNumbers = new List<int>();
            foreach (SelectedLine line in values)
            {
                Logger.WriteLine(string.Format("Transfer line {0} with text {1} ", line._lineNumber, line._selectionString));

                int index = line._lineNumber - line.beforeStrings.Count;
                if (prevLine != -1 && prevLine < (index - 1))
                {
                    richTextBox1.Text += string.Format("...\n");
                }
                
                foreach (string str in line.beforeStrings)
                {
                    if (!lnesNumbers.Contains(index))
                    {
                        prevLine = index;
                        lnesNumbers.Add(index);
                        richTextBox1.Text += string.Format("{0:D5}:{1} \n", index, str);
                    }
                    index++;
                }

                if (!lnesNumbers.Contains(index))
                {
                    prevLine = index;
                    lnesNumbers.Add(index);
                    richTextBox1.Text += string.Format("{0:D5}:{1} \n", index, line._selectionString);
                }
                index = line._lineNumber + 1;

                foreach (string str in line.afterStrings)
                {
                    if (!lnesNumbers.Contains(index))
                    {
                        prevLine = index;
                        lnesNumbers.Add(index);
                        richTextBox1.Text += string.Format("{0:D5}:{1} \n", index, str);
                    }
                    index++;
                }
                
            }

            HighlightWords(richTextBox1, values, matchCase, wholeWord);
        }

        private int HighlightWords(RichTextBox rtb, List<SelectedLine> values, bool matchCase, bool wholeWord)
        {
            Logger.WriteLine(string.Format("Method: HighlightWords"));

            int iMatchCount = 0;     //'Number of matches

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
            foreach (SelectedLine line in values)
            {
                int foundPosition = rtb.Find(line._selectionString, foundStringPos, rtb.TextLength, opt);
                if (foundPosition > -1)
                {
                    int lFoundPos = rtb.Find(line._selection, foundPosition, rtb.TextLength, opt);
                    rtb.SelectionStart = lFoundPos;
                    rtb.SelectionLength = line._selection.Length;

                    if (line._isRight)
                    {
                        rtb.SelectionColor = color;
                        rtb.SelectionFont = new Font(fontType, fontSize, fontStyle);
                    }
                    else
                    {
                        rtb.SelectionColor = color2;
                        rtb.SelectionFont = new Font(fontType2, fontSize2, fontStyle2);
                    }
                    iMatchCount++;
                    foundStringPos = foundPosition + line._selectionString.Length;
                }
            }

            return iMatchCount;

        }

        private void ReadDataFromReg()
        {
            Dictionary<string, object> values;
            if (WorkWithRegistry.ReadFromReg(out values) || values == null)
            {
                GetFontsData(values);
            }
        }

        private void GetFontsData(Dictionary<string, object> values)
        {
            string key = "fontType";
            if (values.ContainsKey(key))
            {
                fontType = values[key].ToString();
            }

            key = "fontSize";
            if (values.ContainsKey(key))
            {
                float.TryParse(values[key].ToString(), out fontSize);
            }

            key = "fontStyle";
            if (values.ContainsKey(key))
            {
                fontStyle = (FontStyle)Enum.Parse(fontStyle.GetType(), values[key].ToString(), true);
            }

            key = "color";
            if (values.ContainsKey(key))
            {
                color = UTILS.GetColor(values, key);
            }

            key = "fontType2";
            if (values.ContainsKey(key))
            {
                fontType2 = values[key].ToString();
            }

            key = "fontSize2";
            if (values.ContainsKey(key))
            {
                float.TryParse(values[key].ToString(), out fontSize2);
            }

            key = "fontStyle2";
            if (values.ContainsKey(key))
            {
                fontStyle2 = (FontStyle)Enum.Parse(fontStyle2.GetType(), values[key].ToString(), true);
            }

            key = "color2";
            if (values.ContainsKey(key))
            {
                color2 = UTILS.GetColor(values, key);
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            int lineNumber = GetLineNumber();
            if (lineNumber == -1)
            {
                return;
            }
            OnGoToLine(lineNumber);
        }

        private int GetLineNumber()
        {
            int selectionStart = richTextBox1.SelectionStart;
            int nextLineBreak = richTextBox1.Text.IndexOf('\n', selectionStart);
            if (nextLineBreak == -1)
            {
                nextLineBreak = richTextBox1.Text.Length - 1;
            }
            int previousLineBreak = 0;
            int temp = richTextBox1.Text.IndexOf('\n', previousLineBreak);
            while (temp != -1 && temp < nextLineBreak)
            {
                previousLineBreak = temp + 1;
                temp = richTextBox1.Text.IndexOf('\n', previousLineBreak);
            }
            string lineString = richTextBox1.Text.Substring(previousLineBreak, nextLineBreak - previousLineBreak);
            string number = string.Empty;
            int i = 0;
            while (char.IsDigit(lineString, i))
            {
                number += lineString[i++];
            }
            int numberInt;
            if (!int.TryParse(number, out numberInt))
            {
                numberInt = -1;
            }
            return numberInt;
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            buttonGo_Click(null, null);
        }

    }
}
