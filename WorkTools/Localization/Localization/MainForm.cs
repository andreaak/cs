using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Localization.Readers;
using Localization.Writers;
using Utils;

namespace Localization
{
    public partial class MainForm : Form
    {
        SortedDictionary<string, string> sentences;
        
        public MainForm()
        {
            InitializeComponent();
        }

        #region BUTTON

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

        private void buttonReadTranslationCsv_Click(object sender, EventArgs e)
        {
            string filePath = null;
            if (SelectFile.OpenFile("Open file", null, ref filePath, new string[] { @"Csv Files (.csv)|*.csv|All Files (*.*)|*.*" }))
            {
                IReader reader = new CsvReader(new CsvParser(), filePath);
                SortedDictionary<string, string> translations = reader.Read();
                Translator translator = new Translator();
                translator.Translations = translations;
                foreach (string key in sentences.Keys.ToArray())
                {
                    string translation = translator.Translate(key);
                    if (!string.IsNullOrEmpty(translation))
                    {
                        sentences[key] = translation;
                    }
                }
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

        private void buttonSavePoFile_Click(object sender, EventArgs e)
        {
            string filePath = null;
            if (sentences != null
                && SelectFile.SaveFile("Save file", null, ref filePath, new string[] { @"Po Files (.po)|*.po|All Files (*.*)|*.*" }))
            {
                IWriter writer = new PoWriter(filePath);
                writer.Write(sentences);
            }
        }

        #endregion

        private void SetText()
        {
            Text = string.Format("Readed - {0} sentences", sentences.Count);
        }


    }
}
