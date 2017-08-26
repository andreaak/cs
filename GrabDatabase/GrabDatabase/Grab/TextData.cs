using System;
using System.Text;
using System.IO;
using DevExpress.XtraEditors;
using Utils.ActionWindow;

namespace GrabDatabase
{
    public partial class TextData : XtraForm
    {
        DBService dbService;
        public TextData(DBService dbService)
        {
            InitializeComponent();
            this.dbService = dbService;
            string[] names = dbService.GetNamesByType(null, null);
            rtLookUpEdit.Properties.DataSource = names;
        }

        private void GetDbData_SizeChanged(object sender, EventArgs e)
        {
            gridControl1.Width = this.ClientRectangle.Width;
            gridControl1.Height = this.ClientRectangle.Height - gridControl1.Top;
        }

        private void simpleButtonSaveData_Click(object sender, EventArgs e)
        {
            string itemName = string.Empty;
            if (rtLookUpEdit.GetSelectedDataRow() == null)
            {
                return;
            }

            itemName = rtLookUpEdit.GetSelectedDataRow().ToString();
            if (string.IsNullOrEmpty(itemName))
            {
                return;
            }
            int[] sel = gridView1.GetSelectedRows();
            if (sel != null && sel.Length > 0)
            {
                SaveFunctionText(sel, itemName);
            }
            DisplayMessage.ShowInfo(DisplayMessage.INFO_DONE_CONST);
        }

        private void SaveFunctionText(int[] sel, string itemName)
        {
            string dir = string.Format("{1}{0}{2}", Path.DirectorySeparatorChar, Directory.GetCurrentDirectory(), itemName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            foreach (int i in sel)
            {
                string name = gridView1.GetRow(i).ToString();
                
                string file = string.Format("{1}{0}{2}", Path.DirectorySeparatorChar, dir, name + ".txt");
                string tmp = string.Empty;
                try
                {
                    tmp = dbService.GetFunctionText(name);
                }
                catch (Exception)
                {
                    File.Create(file);
                    continue;
                }

                File.WriteAllText(file, tmp, Encoding.Unicode);
            }
        }

        private void rtLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (rtLookUpEdit.GetSelectedDataRow() != null)
            {
                string dbObjectTypeName = rtLookUpEdit.GetSelectedDataRow().ToString();
                try
                {
                    string[] names = dbService.GetNamesByType(dbObjectTypeName, null);
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = names;
                    gridView1.Columns[0].Caption = "Item";
                }
                catch (Exception)
                {

                    return;
                }

            }
        }

        private void simpleButtonShow_Click(object sender, EventArgs e)
        {
            rtLookUpEdit_EditValueChanged(null, null);
        }
    }
}