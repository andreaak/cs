using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.IO;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DBLinks;
using GrabDatabase.Links;
using Utils;
using GrabDatabase.Grab;
using ParseQuery;
using System.Threading;
using Utils.WorkWithDB;
using Utils.ActionWindow;
using Utils.WorkWithFiles;

namespace GrabDatabase
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        private DBService dbService;
        Dictionary<string, DBLinks.Table> tables;
        DbStructureDataset dbStructure;
        private const string TITLE = "Grab Database";

        public Main()
        {
            InitializeComponent();
            EnableControls(false);
        }

        #region SERVICE

        private void barButtonConnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Connect(true);
        }

        private void barButtonItemSelectConnection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OptionsUtils.ClearDbData();
            Connect(true);
        }

        #endregion

        #region BUTTON_HANDLERS_GRAB

        private void barButtonItemCreateTables_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CreateTable form = new CreateTable(dbService);
            form.ShowDialog();
        }

        private void barButtonItemGrabData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MetaData form = new MetaData(dbService);
            form.Show();
        }

        private void barButtonItemGrabText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TextData form = new TextData(dbService);
            form.Show();
        }

        #endregion

        #region BUTTON_HANDLERS_LINKS

        private void barButtonItemImportTables_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ImportTablesForm form = new ImportTablesForm(dbService);
            form.ShowDialog();
            tables = form.Tables;
        }
          
        private void barButtonItemReadLinks_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tables = UTILS.ReadData(null);
        }  

        private void barButtonItemSaveLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UTILS.SaveData(null, tables);
        }    
  
        private void barButtonItemLoadLinks_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string[] extensions = new string[]
            {
            "TXT Files (*.txt)|*.txt|",
            "All Files (*.*)|*.*"
            };
            string title = "Load Data";
            string filePath = string.Empty;
            if (!SelectFile.OpenFile(title, string.Empty, ref filePath, extensions))
            {
                return;
            }
            Utils.ReadInfo.Open(filePath);
            List<string> list = new List<string>();
            do
            {
                string temp = Utils.ReadInfo.ReadLine();
                if (temp == null || temp.Equals(string.Empty))
                {
                    break;
                }
                Parse(temp);
            } while (true);
            Utils.ReadInfo.Close();
        }

        private void barButtonItemCreateLinksFromKeys_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tables == null || tables.Count == 0)
            {
                return;
            }
            WorkWithKeys form = new WorkWithKeys(tables);
            form.ShowDialog();
        }

        private void barButtonItemDeleteLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RemoveLinkForm form = new RemoveLinkForm(tables);
            form.ShowDialog();
        }

        private void barButtonItemPrintLinks_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string[] extensions = new string[]
            {
             "TXT Files (*.txt)|*.txt|",
             "All Files (*.*)|*.*"
            };
            string title = "Save Data";
            string filePath = string.Empty;
            if (!SelectFile.SaveFile(title, string.Empty, ref filePath, extensions))
            {
                return;
            }

            List<string> list = new List<string>();
            foreach (Table table in tables.Values)
            {
                foreach (Field field in table.Fields.Values)
                {
                    foreach (Link link in field.Links)
                    {
                        if (!list.Contains(string.Format("{0}.{1}{4}{2}.{3}", link.TableName, link.FieldName, table.Name, field.Name, DBConstants.SEPARATORE)))
                        {
                            list.Add(string.Format("{0}.{1}{4}{2}.{3}", table.Name, field.Name, link.TableName, link.FieldName, DBConstants.SEPARATORE));
                        }
                    }

                }
            }
            list.Sort();

            Logger logger = Logger.GetInstance();
            logger.Init(filePath);
            foreach (string path in list)
            {
                logger.WriteLine(path);
            }
            logger.Close();
        }

        private void barButtonItemFindField_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tables == null || tables.Count == 0)
            {
                return;
            }
            FindFieldForm form = new FindFieldForm(tables);
            form.ShowDialog();
        }

        private void barButtonItemFindLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tables == null || tables.Count == 0)
            {
                return;
            }
            FindLinkForm form = new FindLinkForm(tables);
            form.ShowDialog();
        }

        #endregion

        #region BUTTON_HANDLERS_QUERY

        private void barButtonItemReadTablesData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DbStructureDataset ds = UTILS.ReadDbStructure(dbService);
            if (ds != null)
            {
                dbStructure = ds;
            }
        }

        private void barButtonItemSqlCommand_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SqlCommand form = new SqlCommand(dbService, dbStructure);
            form.Show();
        }

        private void barButtonItemParseQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ParseQueryForm form = new ParseQueryForm();
            form.Show();
        }

        #endregion

        #region BUTTON_HANDLERS_HELP

        private void barButtonItemHelp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RtfForm form = new RtfForm();
            form.Show();
        }

        #endregion


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

        #region BIND GRID

        private void gridControl1_ViewRegistered(object sender, ViewOperationEventArgs e)
        {
            GridView gv = e.View as GridView;
            if (gv != gridControl1.MainView && gv.Columns["ID"] != null)
            {
                gv.Columns["ID"].Visible = false;
            }
        }

        #endregion

        #region UTILS

        private bool Connect(bool isReconnect = false)
        {
            if (isReconnect && !Reconnect())
            {
                return false;
            }
            bool dbConnectionOk = false;
            string errorMessage = null;
            if (dbService != null)
            {
                dbService.Close();
            }
            dbService = new DBService();
            try
            {
                dbConnectionOk = dbService.IsValidConnection;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            EnableControls(dbConnectionOk);
            if (!dbConnectionOk)
            {
                DisplayMessage.ShowError(errorMessage, "Connection error");
            }
            else
            {
                this.Text = string.Format("{0} {1}", TITLE, dbService.GetConnectionDescription());
            }
            return dbConnectionOk;
        }

        private bool Reconnect()
        {
            try
            {
                DialogResult res = new DBDataForm().ShowDialog(this);
                return res == DialogResult.OK;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }

        private void EnableControls(bool isEnable)
        {
            barButtonItemCreateTables.Enabled = isEnable;
            barButtonItemGrabMetaData.Enabled = isEnable;
            barButtonItemGrabText.Enabled = isEnable;
            
            barButtonItemReadTablesData.Enabled = isEnable;
            barButtonItemSqlCommand.Enabled = isEnable;
        }

        private void Parse(string temp)
        {
            string[] sep = new string[] { DBConstants.SEPARATORE };
            string[] temps = temp.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            if (temps.Length < 2)
            {
                return;
            }
            string[] tables_field1 = temps[0].Split('.');
            string[] tables_field2 = temps[1].Split('.');

            if (tables_field1.Length < 2 || tables_field2.Length < 2)
            {
                return;
            }
            if (!(
                tables.ContainsKey(tables_field1[0]) && tables[tables_field1[0]].Fields.ContainsKey(tables_field1[1])
                && tables.ContainsKey(tables_field2[0]) && tables[tables_field2[0]].Fields.ContainsKey(tables_field2[1])
               ))
            {
                return;
            }
            tables[tables_field1[0]].Fields[tables_field1[1]].AddLink(new Link(tables_field2[0], tables_field2[1]));
            tables[tables_field2[0]].Fields[tables_field2[1]].AddLink(new Link(tables_field1[0], tables_field1[1]));
        }

        

        #endregion
    }
}