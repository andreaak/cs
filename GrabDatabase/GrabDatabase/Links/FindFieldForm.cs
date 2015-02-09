using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DBLinks;

namespace GrabDatabase.Links
{
    public partial class FindFieldForm : Form
    {
        Dictionary<string, Table> tables;
        
        public FindFieldForm(Dictionary<string, Table> tables)
        {
            InitializeComponent();
            this.tables = tables;
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if(textBoxFieldName.Text == string.Empty)
            {
                return;
            }
            int index = 0;
            foreach (Table table in tables.Values)
            {
                if(table.Fields.ContainsKey(textBoxFieldName.Text))
                {
                    index++;
                    ListViewItem lvi = new ListViewItem(new string[]{index.ToString(),table.Name, table.Fields[textBoxFieldName.Text].Type});
                    listView1.Items.Add(lvi);
                }
            }
        }


    }
}