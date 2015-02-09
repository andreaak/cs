using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Utils.ActionWindow;

namespace GrabDatabase
{
    public partial class CreateTable : DevExpress.XtraEditors.XtraForm
    {
        DBService dbService;
        string folder;
        List<string> tables;
        private string schemeExtension = "_scheme";
        private string dataExtension = "_data";

        public CreateTable(DBService dbService)
        {
            InitializeComponent();
            this.dbService = dbService;
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

                dbService.AddTable(scheme, data);
            }
            DisplayMessage.ShowInfo(DisplayMessage.INFO_DONE_CONST);
        }

        private void simpleButtonOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Choose tables directory...";
            folderDialog.ShowNewFolderButton = false;

            if (folderDialog.ShowDialog(this) != DialogResult.OK || folderDialog.SelectedPath.Length == 0)
            {
                return;
            }
            folder = folderDialog.SelectedPath;
            if (!Directory.Exists(folder))
            {
                DisplayMessage.ShowWarning("Wrong folder");
                return;
            }
            tables = new List<string>();
            foreach (string file in Directory.GetFiles(folder))
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string extension = Path.GetExtension(file);

                if (!fileName.EndsWith(schemeExtension))
                {
                    continue;
                }

                string tableName = fileName.Replace(schemeExtension, string.Empty);
                tables.Add(tableName);
            }
            gridControl1.DataSource = tables;
        }
    }
}