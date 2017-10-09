using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System.IO;
using System.Linq;
using DevExpress.Utils.Serializing;
using Utils.WorkWithDB;
using Utils;
using Utils.ActionWindow;

namespace GrabDatabase
{
    public partial class MetaData : DevExpress.XtraEditors.XtraForm
    {
        DataSet items;
        DataSet dataset;
        DBService dbService;

        public MetaData(DBService dbService)
        {
            InitializeComponent();
            this.dbService = dbService;
            string tmp = this.dbService.GetDBScheme(null, null);
            items = new DataSet();
            DBUtils.GetDatasetFromXML(tmp, items);

            bindingSource1.DataSource = items.Tables["MetaDataCollections"];
            rtLookUpEdit.Properties.DataSource = bindingSource1;
            rtLookUpEdit.Properties.DisplayMember = "CollectionName";
            LookUpColumnInfoCollection coll = rtLookUpEdit.Properties.Columns;
            coll.Add(new LookUpColumnInfo("CollectionName", 0));
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

            itemName = ((DataRowView)rtLookUpEdit.GetSelectedDataRow()).Row["CollectionName"].ToString();
            if (string.IsNullOrEmpty(itemName))
            {
            }
            int[] sel = null;
            switch (itemName)
            {
                case "Tables":
                    sel = gridView1.GetSelectedRows();
                    if (sel != null && sel.Length > 0)
                    {
                        SaveTablesData(sel);
                    }
                    break;
                case "Functions":
                case "Procedures":
                case "Packages":
                    sel = gridView1.GetSelectedRows();
                    if (sel != null && sel.Length > 0)
                    {
                        SaveGridData(sel, itemName);
                        SaveFunctionText(sel, itemName);
                    }
                    break;
                default:
                    sel = gridView1.GetSelectedRows();
                    if (sel != null && sel.Length > 0)
                    {
                        SaveGridData(sel, itemName);
                    }
                    break;
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
                DataRow tempRow = gridView1.GetDataRow(i);
                string name = string.Empty;
                switch (itemName)
                {
                    default:
                        name = tempRow["OBJECT_NAME"].ToString();
                        break;
                }

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

        private void SaveTablesData(int[] sel)
        {
            string dir = string.Format("{1}{0}{2}", Path.DirectorySeparatorChar, Directory.GetCurrentDirectory(), "Tables");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            foreach (int i in sel)
            {
                DataRow tempRow = gridView1.GetDataRow(i);
                string name = tempRow["TABLE_NAME"].ToString();
                string file = string.Empty;
                string tmp = string.Empty;
                try
                {
                    file = string.Format("{1}{0}{2}", Path.DirectorySeparatorChar, dir, name + "_scheme.xml");
                    tmp = dbService.GetTableSheme(name);
                    DataSet tempDs = new DataSet();
                    DBUtils.GetDatasetFromXML(tmp, tempDs);
                    tempDs.WriteXml(file, XmlWriteMode.WriteSchema);

                    if (checkBoxGetScheme.Checked)
                    {
                        continue;
                    }

                    file = string.Format("{1}{0}{2}", Path.DirectorySeparatorChar, dir, name + "_data.xml");
                    tmp = dbService.GetTableData(name);
                    tempDs = new DataSet();
                    DBUtils.GetDatasetFromXML(tmp, tempDs);
                    tempDs.WriteXml(file, XmlWriteMode.WriteSchema);

                    file = string.Format("{1}{0}{2}", Path.DirectorySeparatorChar, dir, name + "_data.sql");
                    WriteSql(file, tempDs);
                }
                catch (Exception)
                {
                    File.Create(file);
                    continue;
                }
            }
        }

        private void WriteSql(string file, DataSet tempDs)
        {
            StreamWriter sw = File.CreateText(file);
            try
            {
                foreach (DataTable table in tempDs.Tables)
                {
                    WriteTableSql(sw, table);
                }
            }
            finally
            {
                sw.Close();
            }

        }

        private void WriteTableSql(StreamWriter sw, DataTable table)
        {
            sw.WriteLine("---------" + table.TableName + "---------");
            sw.WriteLine("");

            StringBuilder sb = new StringBuilder();
            foreach (DataColumn column in table.Columns)
            {
                if (sb.Length != 0)
                {
                    sb.Append(", ");
                }
                sb.Append(column.ColumnName);
            }
            sb.Insert(0, "INSERT INTO " + table.TableName + "(");
            sb.Append(") VALUES ( {0} )");
            string head = sb.ToString();
            foreach (DataRow row in table.Rows)
            {
                string values = string.Join(", ", row.ItemArray.Select(GetText));
                string res = string.Format(head, values);
                sw.WriteLine(res);
            }
            sw.WriteLine("");
        }

        private string GetText(object item)
        {
            if (item == DBNull.Value)
                return "NULL";
            if (item is string)
            {
                return "\'" + item.ToString().Trim() + "\'";
            }

            if (item is DateTime)
            {
                return "CAST(N\'" + Convert.ToString(item, CultureInfo.InvariantCulture).Trim() + "\' AS DateTime )";
            }

            if (Convert.ToString(item, CultureInfo.InvariantCulture).Contains(","))
            {
                return "\'" + Convert.ToString(item, CultureInfo.InvariantCulture).Trim() + "\'";
            }

            return Convert.ToString(item, CultureInfo.InvariantCulture);
        }


        private void SaveGridData(int[] sel, string itemName)
        {
            DataTable temp = ((gridControl1.DataSource as BindingSource).DataSource as DataSet).Tables[0];
            if (temp == null)
            {
                return;
            }

            string dir = string.Format("{1}{0}{2}", Path.DirectorySeparatorChar, Directory.GetCurrentDirectory(), itemName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string file = string.Format("{1}{0}{2}", Path.DirectorySeparatorChar, dir, itemName + "_grid.xml");
            temp.WriteXml(file, XmlWriteMode.WriteSchema);
        }

        private void simpleButtonShow_Click(object sender, EventArgs e)
        {
            if (rtLookUpEdit.GetSelectedDataRow() == null)
            {
                return;
            }
            DataRow tempRow = ((DataRowView)rtLookUpEdit.GetSelectedDataRow()).Row;
            string schemeName = tempRow["CollectionName"].ToString();
            string[] restrictions = null;
            if (!string.IsNullOrEmpty(textEditOwner.Text))
            {
                restrictions = new string[] { textEditOwner.Text };
            }

            string headerText = "Processing...";
            Func<string> func = () =>
            {
                return dbService.GetDBScheme(schemeName, restrictions);
            };

            string xml = CancelFormEx.ShowProgressWindow(func, headerText);
            if (string.IsNullOrEmpty(xml))
            {
                return;
            }

            dataset = new DataSet();
            DBUtils.GetDatasetFromXML(xml, dataset);
            UTILS.BindDataSet(dataset, gridControl1, gridView1);
            string itemName = ((DataRowView)rtLookUpEdit.GetSelectedDataRow()).Row["CollectionName"].ToString();
            if (itemName.Equals("Tables"))
            {
                checkBoxGetScheme.Checked = false;
                checkBoxGetScheme.Visible = true;
            }
            else
            {
                checkBoxGetScheme.Checked = false;
                checkBoxGetScheme.Visible = false;
            }
        }
    }
}
