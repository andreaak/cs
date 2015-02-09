using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using EnvDTE;

namespace Assist
{
    public partial class RTFTextControlOptionPage : UserControl, IDTToolsOptionsPage
    {
        public RTFTextControlOptionPage()
        {
            InitializeComponent();
        }        
        
        #region INTERFACE

        public void OnOK()
        {
            Options.GetInstance.SkipInDebug = UTILS.GetSepDataFromGrid(dataGridView1);
            Options.GetInstance.SaveDataToReg();
        }

        public void OnAfterCreated(DTE DTEObject)
        {
            if (Options.GetInstance.Guids != null)
            {
                this.comboBoxGuidR.SelectedIndexChanged -= new System.EventHandler(this.comboBoxGuidR_SelectedIndexChanged);
                this.comboBoxGuidL.SelectedIndexChanged -= new System.EventHandler(this.comboBoxGuidL_SelectedIndexChanged);
                
                List<string> tempList = new List<string>(Options.GetInstance.Guids);
                comboBoxGuidL.DataSource = Options.GetInstance.Guids;
                this.comboBoxGuidL.SelectedItem = Options.GetInstance.GuidL;
                comboBoxGuidR.DataSource = tempList;
                this.comboBoxGuidR.SelectedItem = Options.GetInstance.GuidR;
                this.comboBoxGuidR.SelectedIndexChanged += new System.EventHandler(this.comboBoxGuidR_SelectedIndexChanged);
                this.comboBoxGuidL.SelectedIndexChanged += new System.EventHandler(this.comboBoxGuidL_SelectedIndexChanged);
            }

            textBoxFontR.Text = GetDescription(Options.GetInstance.FontType, Options.GetInstance.FontSize,
                Options.GetInstance.FontStyle, Options.GetInstance.Color.Name);

            textBoxFontL.Text = GetDescription(Options.GetInstance.FontType2, Options.GetInstance.FontSize2,
                Options.GetInstance.FontStyle2, Options.GetInstance.Color2.Name);

            numericUpDownSubRow.Value = Options.GetInstance.SubRowCount;
        }

        public void GetProperties(ref object PropertiesObject)
        {
            //throw new NotImplementedException();
        }

        public void OnCancel()
        {
            //throw new NotImplementedException();
        }

        public void OnHelp()
        {
            //throw new NotImplementedException();
        }

        #endregion

        private static string GetDescription(string fontType, float fontSize, FontStyle fontStyle, string colorName)
        {
            return string.Format("{0} {1} {2} {3}", fontType, fontSize, fontStyle, colorName);
        }

        private void buttonChooseFontR_Click(object sender, EventArgs e)
        {
            FontDialog form = new FontDialog();
            if (Options.GetInstance.FontType != null)
            {
                form.Font = new Font(Options.GetInstance.FontType, Options.GetInstance.FontSize, Options.GetInstance.FontStyle);
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                ColorDialog form2 = new ColorDialog();
                form2.Color = Options.GetInstance.Color;
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    Options.GetInstance.FontType = form.Font.Name;
                    Options.GetInstance.FontSize = form.Font.Size;
                    Options.GetInstance.FontStyle = form.Font.Style;
                    Options.GetInstance.Color = form2.Color;
                    textBoxFontR.Text = GetDescription(Options.GetInstance.FontType, Options.GetInstance.FontSize,
                    Options.GetInstance.FontStyle, Options.GetInstance.Color.Name);
                }
            }
        }

        private void buttonChooseFontL_Click(object sender, EventArgs e)
        {
            FontDialog form = new FontDialog();
            if (Options.GetInstance.FontType2 != null)
            {
                form.Font = new Font(Options.GetInstance.FontType2, Options.GetInstance.FontSize2, Options.GetInstance.FontStyle2);
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                ColorDialog form2 = new ColorDialog();
                form2.Color = Options.GetInstance.Color2;
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    Options.GetInstance.FontType2 = form.Font.Name;
                    Options.GetInstance.FontSize2 = form.Font.Size;
                    Options.GetInstance.FontStyle2 = form.Font.Style;
                    Options.GetInstance.Color2 = form2.Color;
                    textBoxFontL.Text = GetDescription(Options.GetInstance.FontType2, Options.GetInstance.FontSize2,
                    Options.GetInstance.FontStyle2, Options.GetInstance.Color2.Name);
                }
            }
        }

        private void checkBoxSetLogger_CheckedChanged(object sender, EventArgs e)
        {
            Options.GetInstance.SetLogger = checkBoxSetLogger.Checked;
            textBoxLoggerPath.Enabled = Options.GetInstance.SetLogger;
            labelPath.Enabled = Options.GetInstance.SetLogger;
        }

        private void comboBoxGuidR_SelectedIndexChanged(object sender, EventArgs e)
        {
            Options.GetInstance.GuidR = comboBoxGuidR.SelectedItem.ToString();
        }

        private void comboBoxGuidL_SelectedIndexChanged(object sender, EventArgs e)
        {
            Options.GetInstance.GuidL = comboBoxGuidL.SelectedItem.ToString();
        }

        private void checkBoxSetTextInControl_CheckedChanged(object sender, EventArgs e)
        {
            Options.GetInstance.HighlightText = checkBoxHighlightText.Checked;
            Options.GetInstance.SetTextInControl = checkBoxSetTextInControl.Checked;
        }

        private void textBoxLoggerPath_TextChanged(object sender, EventArgs e)
        {
            Options.GetInstance.LoggerFilePath = textBoxLoggerPath.Text;
        }

        private void buttonChoosePath_Click(object sender, EventArgs e)
        {

           string[] extensions = new string[]
           {
               "XML Files (*.log)|*.log|",
               "All Files (*.*)|*.*"
           };
           string title = "Выберите файл лога";
           string filePath = string.Empty;
           if (Utils.SelectFile.SaveFile(title, string.Empty, ref filePath, extensions))
           {
               textBoxLoggerPath.Text = filePath;
           }

        }

        private void numericUpDownSubRow_ValueChanged(object sender, EventArgs e)
        {
            Options.GetInstance.SubRowCount = (int)numericUpDownSubRow.Value;
        }


   
    }
}
