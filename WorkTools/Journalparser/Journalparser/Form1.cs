using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Journalparser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonParse_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFileDlg = new OpenFileDialog();
            oFileDlg.Filter = "TXT Files (*.txt)|*.txt|" + "All Files (*.*)|*.*";
            if (oFileDlg.ShowDialog() == DialogResult.OK && oFileDlg.FileName.Length > 0)
            {
                string file = oFileDlg.FileName;
                var points = Parser.Parse(file, (int)numericUpDown1.Value);
                WriteToFile(points);
                MessageBox.Show("Done");
            }

        }

        private void WriteToFile(List<FreezedItem> points)
        {
            using (var writer = File.CreateText("result.txt"))
            {
                foreach (var item in points)
                {
                    writer.WriteLine($"Duration: {item.Time} Start: {item.Start} Line Start: {item.LineStart} End: {item.End} Line End: {item.LineEnd}");
                    writer.WriteLine("");
                    writer.WriteLine(item.Text);
                    writer.WriteLine("\r\n");
                }
            }
        }
    }
}
