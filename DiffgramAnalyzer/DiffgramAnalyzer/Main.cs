using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DiffgramAnalyzer
{
    public partial class Main : Form
    {
        List<Table> tables;
        Table currentTable;
        WorkWithListView wt;
        public Main()
        {
            InitializeComponent();
        }

        private void toolStripButtonAddXml_Click(object sender, EventArgs e)
        {
            XMLAnalyzer form = new XMLAnalyzer();
            if(form.ShowDialog()  ==  DialogResult.OK)
            {
                wt = new WorkWithListView(listView1);
                tables = form.Tables;
                wt.ClearListView();
                FillTableCombobox();
                //wt.FillListView(tables);
            }
        }

        private void FillTableCombobox()
        {
            this.toolStripComboBoxTables.SelectedIndexChanged -= new System.EventHandler(this.toolStripComboBoxTables_SelectedIndexChanged);

            toolStripComboBoxTables.Items.Clear();
            toolStripComboBoxTables.Text = "";
            toolStripComboBoxRow.Items.Clear();
            toolStripComboBoxRow.Text = "";
            toolStripComboBoxTables.Items.Add("All");
            foreach (Table table in tables)
	        {
        		 toolStripComboBoxTables.Items.Add(table.name);
	        }

            this.toolStripComboBoxTables.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxTables_SelectedIndexChanged);
            toolStripComboBoxTables.SelectedIndex = -1;

        }

        private void toolStripComboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBoxTables.SelectedIndex == -1)
            {
                currentTable = null;
                return;
            }
            if (toolStripComboBoxTables.SelectedIndex == 0)
            {
                toolStripComboBoxRow.SelectedIndex = -1;
                toolStripComboBoxRow.Items.Clear();
                wt.FillListView(tables);
                currentTable = null;
            }
            else
            {
                currentTable = GetTable(tables, toolStripComboBoxTables.SelectedItem.ToString());
                if (currentTable != null)
                {
                    FillRowCombobox(currentTable);
                    wt.ClearListView();
                    wt.FillListView(currentTable, null);
                }
            }
        }

        private Table GetTable(List<Table> tables, string name)
        {
            foreach (Table table in tables)
            {
                if (table.name == name)
                {
                    return table;
                }
            }
            return null;
        }

        private void toolStripComboBoxRow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentTable != null )
            {
                string temp = null;
                if (toolStripComboBoxRow.SelectedIndex > 0)
                {
                    temp = toolStripComboBoxRow.SelectedItem.ToString();
                }
                wt.ClearListView();
                wt.FillListView(currentTable, temp);
            }
            else
            {
                wt.FillListView(tables);
            }
        }
        
        private void FillRowCombobox(Table table)
        {
            this.toolStripComboBoxRow.SelectedIndexChanged -= new System.EventHandler(this.toolStripComboBoxRow_SelectedIndexChanged);

            toolStripComboBoxRow.Items.Clear();
            toolStripComboBoxRow.Items.Add("All");
            foreach (Row row in table.rows)
            {
                if (row.status != RowStatus.before)
                {
                    toolStripComboBoxRow.Items.Add(row.name);
                }
            }
            this.toolStripComboBoxRow.SelectedIndex = 0;
            this.toolStripComboBoxRow.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxRow_SelectedIndexChanged);
        }

        #region DISPLAY
        //
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }
        //
        private void Form_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && this.Visible)
            {
                this.Visible = false;
                notifyIcon1.Visible = true;
            }
        }
        #endregion

    }
}