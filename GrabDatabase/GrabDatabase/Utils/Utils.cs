using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DBLinks;
using Utils;
using System.Data;
using System;
using GrabDatabase.Grab;
using Utils.ActionWindow;
using Utils.WorkWithDB;

namespace GrabDatabase
{
    static class UTILS
    {
        public static void BindDataSet(DataSet ds, DevExpress.XtraGrid.GridControl gridControl, DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            BindingSource bindSource = new BindingSource();
            bindSource.DataSource = ds;
            bindSource.DataMember = ds.Tables[0].TableName;
            gridView.Columns.Clear();
            gridControl.DataSource = bindSource;
        }

        #region SERIALIZATION
        
        public static Dictionary<string, Table> ReadData(string filePath)
        {
            
            Dictionary<string, Table> tables = null;
            if (filePath == null)
            {
                string[] extensions = new string[]
                {
                "BIN Files (*.bin)|*.bin|",
                "All Files (*.*)|*.*"
                };
                string title = "Load Data";
                if (!SelectFile.OpenFile(title, string.Empty, ref filePath, extensions))
                {
                    return new Dictionary<string, Table>();
                }
            }
            if (File.Exists(filePath))
            {
                string error = string.Empty;
                bool code = true;
                tables = (Dictionary<string, Table>)SerializeBin.SerializeFrom(filePath, ref code, ref error);
                if (!code)
                {
                    DisplayMessage.ShowError(string.Format("Error open file {0}", error));
                    tables = new Dictionary<string, Table>();
                }
            }
            else
            {
                tables = new Dictionary<string, Table>();
            }
            return tables;
        }

        public static bool SaveData(string filePath, Dictionary<string, Table> tables)
        {
            if(tables == null)
            {
                return false;
            }

            if(filePath == null)
            {
                string[] extensions = new string[]
                {
                "BIN Files (*.bin)|*.bin|",
                "All Files (*.*)|*.*"
                };
                string title = "Save Data";
                if (!SelectFile.SaveFile(title, string.Empty, ref filePath, extensions))
                {
                    return false;
                }
            }
            string error = string.Empty;
            bool code = SerializeBin.SerializeTo(filePath, tables, ref error);
            return code;
        }

        #endregion

        public static string GetDbScheme(DBService dbService, string schemeName, string[] restrictions, ref string xml)
        {
            try
            {
                xml = dbService.GetDBScheme(schemeName, restrictions);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static DbStructureDataset ReadDbStructure(DBService dbService)
        {
            string headerText = "Processing...";
            Func<DbStructureDataset> func = () =>
            {
                DbStructureReader reader = new DbStructureReader(dbService);
                string error = reader.ReadData();
                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }
                return reader.DbStructure;
            };

            return CancelFormEx.ShowProgressWindow(func, headerText);
        }
    }
}
