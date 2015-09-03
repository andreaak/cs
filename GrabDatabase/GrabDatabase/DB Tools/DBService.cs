using System;
using System.Collections.Generic;
using System.Data;
using DBLinks;
using Utils.WorkWithDB;
using Utils.WorkWithDB.Connections;
using Utils.WorkWithDB.Wrappers;

namespace GrabDatabase
{

    public class DBService
    {
        private IDBWrapper dbWrapper = null;

        public bool IsValidConnection
        {
            get
            {
                try
                {
                    return dbWrapper.OpenConnection();
                }
                finally
                {
                    dbWrapper.CloseConnection();
                }
            }
        }

        public DataBaseType DatabaseType
        {
            get
            {
                return dbWrapper.DbType;
            }
        }

        public string RowRestrictionQuery
        {
            get
            {
                return dbWrapper.RowLimitQuery;
            }
        }

        public DBService()
        {
            dbWrapper = WrapperFactory.GetWrapper();
        }

        public DBService(string provider, string connString)
        {
            dbWrapper = WrapperFactory.GetWrapper(provider, connString);
        }

        #region SELECT XML

        public string GetDBScheme(string name, string[] restrictions)
        {
            return dbWrapper.GetDBScheme(name, restrictions);
        }

        public string GetTableSheme(string name)
        {
            return dbWrapper.GetTableScheme(name);
        }

        public string GetTableData(string name)
        {
            string cmd = string.Format("SELECT * FROM {0}", name);
            return dbWrapper.SelectTableString(cmd, name);
        }

        public string GetFunctionText(string name)
        {
            return dbWrapper.GetFunctionText(name);
        }

        public string[] GetNamesByType(string type, string owner)
        {
            return dbWrapper.GetNamesByType(type);
        }

        #endregion

        #region CONNECTED LAYER

        public void ExecuteScript(string script, string procName)
        {
            dbWrapper.ExecuteScript(script);
        }

        public object ScalarQuery(string query)
        {
            return dbWrapper.ScalarCommand(query);
        }

        public int CRUDQuery(string query)
        {
            return dbWrapper.NonQueryCommand(query);
        }

        #endregion

        public DataSet SelectQuery(string query)
        {
            return dbWrapper.SelectMethodDataset(query, "SELECT", null);
        }

        public void AddTable(string tableScheme, string tableData)
        {

            DataTable dataTable = new DataTable();
            DataTable schemeTable = new DataTable();

            if (!DBUtils.GetDataTableFromXML(tableScheme, tableData, ref schemeTable, ref dataTable))
            {
                throw new SystemException("Restore dataset error");
            }
            dbWrapper.CreateTable(schemeTable);
            dbWrapper.InsertTable(dataTable);
        }

        public Table CreateTable(string tableScheme, string tableData)
        {
            if (string.IsNullOrEmpty(tableScheme))
            {
                return null;
            }

            DataTable dataTable = new DataTable();
            DataTable schemeTable = new DataTable();

            if (!DBUtils.GetDataTableFromXML(tableScheme, tableData, ref schemeTable, ref dataTable))
            {
                throw new SystemException("Restore dataset error");
            }
            Table table = new Table(schemeTable.TableName);
            foreach (DataRow row in schemeTable.Rows)
            {
                Type tp = Type.GetType(row["DataType"].ToString(), false, true);
                Field field = new Field(row["ColumnName"].ToString(), tp.Name);
                table.AddField(field);
            }
            return table;
        }

        public long GetId(string tableName)
        {
            string idColumnName = "ID";
            return dbWrapper.GetNextId(tableName, idColumnName);
        }

        public bool CreateHelpDB(string file)
        {
            return dbWrapper.CreateDB(file, DBConstants.GetHelpScript(dbWrapper.DbType));
        }

        public string GetConnectionDescription()
        {
            return string.Format(" {0}", dbWrapper.ConnectionString);
        }

        public string GetMetaDataColumn(DBObjects dbObj, string key)
        {
            return dbWrapper.GetMetaDataColumn(dbObj, key);
        }

        public string GetMetaDataObject(DBObjects dbObj)
        {
            return dbWrapper.GetMetaDataObject(dbObj);
        }

        public IEnumerable<string> GetVisibleColumns(DBObjects selItem)
        {
            return dbWrapper.GetVisibleColumns(selItem);
        }

        public void FillTables(DataSet dataset, DbStructureDataset dbStructure)
        {
            dbWrapper.FillTables(dataset, dbStructure);
        }

        public void FillViews(DataSet dataset, DbStructureDataset dbStructure)
        {
            dbWrapper.FillViews(dataset, dbStructure);
        }

        public void FillTriggers(DataSet dataset, DbStructureDataset dbStructure)
        {
            dbWrapper.FillTriggers(dataset, dbStructure);
        }

        public void Close()
        {
            dbWrapper.CloseConnection();
        }
    }
}