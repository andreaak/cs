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
        SortedDictionary<string, string> words;
        
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
                words = reader.Read();
            }
        }

        private void buttonSaveToTxtForExcel_Click(object sender, EventArgs e)
        {
            if (words != null)
            {
                string filePath = null;
                if (SelectFile.SaveFile("Save file", null, ref filePath, new string[] { @"Text Files (.txt)|*.txt|All Files (*.*)|*.*" }))
                {
                    IWriter writer = new ExcelWriter(filePath);
                    writer.Write(words);
                }
            }
        }

        private void buttonReadFromCsv_Click(object sender, EventArgs e)
        {
            string filePath = null;
            if (SelectFile.OpenFile("Open file", null, ref filePath, new string[] { @"Csv Files (.csv)|*.csv|All Files (*.*)|*.*" }))
            {
                IReader reader = new CsvReader(new CsvParser(), filePath);
                words = reader.Read();
            }
        }

        private void buttonAddFromCsv_Click(object sender, EventArgs e)
        {
            string filePath = null;
            if (SelectFile.OpenFile("Open file", null, ref filePath, new string[] { @"Csv Files (.csv)|*.csv|All Files (*.*)|*.*" }))
            {
                IReader reader = new CsvReader(new CsvParser(), filePath);
                var newWords = reader.Read();
                foreach (var item in newWords)
	            {
		            string temp;
                    if(!words.TryGetValue(item.Key, out temp))
                    {
                        words[item.Key] = item.Value;
                    }
	            }
            }
        }
    }
}
