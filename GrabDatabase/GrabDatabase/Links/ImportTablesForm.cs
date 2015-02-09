using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GrabDatabase;
using DevExpress.XtraEditors.Controls;
using System.IO;
using DBLinks;
using Utils;
using Utils.ActionWindow;

namespace GrabDatabase
{
    public partial class ImportTablesForm : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<string, Table> tables;

        public Dictionary<string, Table> Tables
        {
            get { return tables; }
            set { tables = value; }
        }

        DBService dbService;
        string folder = null;
        List<string> tablesNames;
        private string schemeExtension = "_scheme";
        private string dataExtension = "_data";

        public ImportTablesForm(DBService dbService)
        {
            InitializeComponent();
            this.dbService = dbService;
            tables = new Dictionary<string, Table>();
        }

        private void GetDbData_SizeChanged(object sender, EventArgs e)
        {
            gridControl1.Width = this.ClientRectangle.Width;
            gridControl1.Height = this.ClientRectangle.Height - gridControl1.Top;
        }

        private void simpleButtonSaveData_Click(object sender, EventArgs e)
        {
            int[] sel = gridView1.GetSelectedRows();
            if (sel == null || sel.Length == 0)
            {
                return;
            }

            string extension = ".xml";
            tables = new Dictionary<string, Table>();
            foreach (int i in sel)
            {
                string tableName = gridView1.GetRow(i).ToString();

                string filePathWithoutExtension = folder + Path.DirectorySeparatorChar + tableName;
                string filePathData = filePathWithoutExtension + dataExtension + extension;
                string filePathScheme = filePathWithoutExtension + schemeExtension + extension;

                string scheme = File.ReadAllText(filePathScheme);

                string data = null;
                if (File.Exists(filePathData))
                {
                    data = File.ReadAllText(filePathData);
                }
                if (string.IsNullOrEmpty(scheme))
                {
                    continue;
                }

                Table table = dbService.CreateTable(scheme, data);
                if (table == null)
                {
                    continue;
                }
                tables.Add(table.Name, table);
            }
            DisplayMessage.ShowInfo(DisplayMessage.INFO_DONE_CONST);
        }

        private void simpleButtonOpen_Click(object sender, EventArgs e)
        {
            string folder = string.Empty;
            string description = "Choose tables directory...";
            if(!SelectFolder.Select(description, ref folder))
            {
                return;
            }
            
            if (!Directory.Exists(folder))
            {
                DisplayMessage.ShowWarning("Wrong folder");
                return;
            }
            tablesNames = new List<string>();
            foreach (string file in Directory.GetFiles(folder))
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string extension = Path.GetExtension(file);

                if (!fileName.EndsWith(schemeExtension))
                {
                    continue;
                }

                string tableName = fileName.Replace(schemeExtension, string.Empty);
                tablesNames.Add(tableName);
            }
            gridControl1.DataSource = tablesNames;
        }
    }
}