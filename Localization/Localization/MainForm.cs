using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Utils.WorkWithFiles;

namespace Localization
{
    public partial class MainForm : Form
    {
        SortedDictionary<string, string> sentences;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonReadDataFromPo_Click(object sender, EventArgs e)
        {
            string filePath = null;
            if (SelectFile.OpenFile("Open file", null, ref filePath, new string[] { @"Localization Files (.po)|*.po|All Files (*.*)|*.*" }))
            {
                IReader reader = new PoReader(new PoParser(), filePath);
                sentences = reader.Read();
                SetText();
            }
        }

        private void buttonSaveToTxtForExcel_Click(object sender, EventArgs e)
        {
            string filePath = null;
            if (sentences != null
                && SelectFile.SaveFile("Save file", null, ref filePath, new string[] { @"Text Files (.txt)|*.txt|All Files (*.*)|*.*" }))
            {
                IWriter writer = new ExcelWriter(filePath);
                writer.Write(sentences);
            }
        }

        private void buttonReadFromCsv_Click(object sender, EventArgs e)
        {
            string filePath = null;
            if (SelectFile.OpenFile("Open file", null, ref filePath, new string[] { @"Csv Files (.csv)|*.csv|All Files (*.*)|*.*" }))
            {
                IReader reader = new CsvReader(new CsvParser(), filePath);
                sentences = reader.Read();
                SetText();
            }
        }

        private void buttonAddFromCsv_Click(object sender, EventArgs e)
        {
            string filePath = null;
            if (SelectFile.OpenFile("Open file", null, ref filePath, new string[] { @"Csv Files (.csv)|*.csv|All Files (*.*)|*.*" }))
            {
                IReader reader = new CsvReader(new CsvParser(), filePath);
                SortedDictionary<string, string> newSentences = reader.Read();
                if (sentences == null)
                {
                    sentences = newSentences;
                }
                else
                {
                    foreach (var item in newSentences)
                    {
                        string temp;
                        if (!sentences.TryGetValue(item.Key, out temp))
                        {
                            sentences[item.Key] = item.Value;
                        }
                    }
                }
                SetText();
            }
        }

        private void buttonAddDataFromCsvCI_Click(object sender, EventArgs e)
        {
            string filePath = null;
            if (SelectFile.OpenFile("Open file", null, ref filePath, new string[] { @"Csv Files (.csv)|*.csv|All Files (*.*)|*.*" }))
            {
                IReader reader = new CsvReader(new CsvParser(), filePath);
                SortedDictionary<string, string> newSentences = reader.Read();
                if (sentences == null)
                {
                    sentences = newSentences;
                }
                else
                {
                    foreach (var item in newSentences)
                    {
                        if (!sentences.Any(sentence => sentence.Key.Equals(item.Key, StringComparison.OrdinalIgnoreCase)))
                        {
                            sentences[item.Key] = item.Value;
                        }
                    }
                }
                SetText();
            }
        }

        private void SetText()
        {
            Text = string.Format("Readed - {0} sentences", sentences.Count);
        }
    }
}
