using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace DiffgramAnalyzer
{
    interface IParser
    {
        List<Table> Parse(List<Table> tables, XmlNode node, bool isBefore);
    }

    public partial class XMLAnalyzer : Form
    {
        List<Table> tables;
        public List<Table> Tables
        {
            get { return tables; }
            set { tables = value; }
        }
        public XMLAnalyzer()
        {
            InitializeComponent();
            tables = new List<Table>();
        }

        private void toolStripButtonParse_Click(object sender, EventArgs e)
        {
            try
            {
                string xml = richTextBox1.Text.Trim();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNode root = doc.DocumentElement;

                int i = 0;
                XmlNode after = null;
                XmlNode before = null;
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (i == 0)
                    {
                        after = node;
                        ++i;
                    }
                    else if(i==1)
                    {
                        before = node;
                        ++i;
                    }
                }

                tables = new List<Table>();
                IParser parser = new DiffgramParser();
                parser.Parse(tables, after, false);
                parser.Parse(tables, before, true);
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
                //this.DialogResult = DialogResult.Abort;
            }
            
            this.Close();
        }

 

        private void toolStripButtonParseDs_Click(object sender, EventArgs e)
        {
            try
            {
                string xml = richTextBox1.Text.Trim();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNode root = doc.DocumentElement;

                tables = new List<Table>();
                IParser parser = new DatasetParser();
                parser.Parse(tables, root, false);
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
                //this.DialogResult = DialogResult.Abort;
            }

            this.Close();
        }

        public string FormatXml(string sUnformattedXml)
        {
            //load unformatted xml into a dom
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sUnformattedXml);

            //will hold formatted xml
            StringBuilder sb = new StringBuilder();

            //pumps the formatted xml into the StringBuilder above
            StringWriter sw = new StringWriter(sb);

            //does the formatting
            XmlTextWriter xtw = null;

            try
            {

                //point the xtw at the StringWriter
                xtw = new XmlTextWriter(sw);

                //we want the output formatted
                xtw.Formatting = Formatting.Indented;

                //get the dom to dump its contents into the xtw
                xd.WriteTo(xtw);
            }
            finally
            {
                //clean up even if error
                if (xtw != null)
                    xtw.Close();
            }

            //return the formatted xml
            return sb.ToString();
        }

        private void toolStripButtonFormatXml_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = FormatXml(richTextBox1.Text);
        }
    }
}