using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using Utils.WorkWithDB;

namespace Utils.WorkWithDB.Wrappers
{
    public class DbFileNotExistException : Exception
    {
        public DbFileNotExistException()
            : base("Database File Doesn't Exist")
        { }
        public DbFileNotExistException(string messsage)
        :base(messsage)
        { }
    }
    
    public enum DBObjects
    {
        Tables,
        Columns,
        Views,
        Triggers,
    }

    public abstract class ADBWrapper
    {


        protected IDictionary<DBObjects, IDictionary<string, string>> metaDataObjectKeys;
        private DBConnectionFactory dbConnection;
        public ADBConnection DBConnection
        {
            get { return dbConnection; }
        }

        public ADBWrapper()
        {
            dbConnection = new DBConnectionFactory();
            //dbConnection = new DBConnectionCL;
            //dbConnection = new DBConnectionDL;
        }

        public ADBWrapper(string provider, string connString)
        {
            dbConnection = new DBConnectionFactory(provider, connString);
        }

        protected virtual void Init()
        {
            metaDataObjectKeys = new Dictionary<DBObjects, IDictionary<string, string>>();
        }

        public virtual bool OpenConnection()
        { 
            string error;
            DBConnection.OpenConnection(out error);
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
            return true;
        }

        #region DB STRUCTURE AND CREATION
        
        public string GetDBScheme(string name, string[] restrictions)
        {
            try
            {
                OpenConnection();
                return DBUtils.GetStringFromDataTable(DBConnection.GetDBScheme(name, restrictions));
            }
            finally
            {
                DBConnection.CloseConnection();
            }
        }

        public string GetTableScheme(string tableName)
        {
            try
            {
                OpenConnection();
                DataTable dt = DBConnection.GetTableScheme(tableName);
                return DBUtils.GetStringFromDataTable(dt);
            }
            finally
            { 
                DBConnection.CloseConnection();
            }
        }

        public virtual bool CreateDB(string file, string script)
        {
            return false;
        }

        public virtual bool CreateDBFile(string file)
        {
            throw new NotSupportedException();
        }

        public virtual bool CreateDBStructure(string script)
        {
            return ScriptMethod(script);
        }

        public virtual void FillTables(DataSet dataset, DbStructureDataset dbStructure)
        {
            string tableNameColumn = GetMetaDataColumn(DBObjects.Tables, "TABLE_NAME");
            string tableDefColumn = GetMetaDataColumn(DBObjects.Tables, "TABLE_DEFINITION");

            string columnNameColumn = GetMetaDataColumn(DBObjects.Columns, "COLUMN_NAME");
            string idColumn = GetMetaDataColumn(DBObjects.Columns, "ID");
            string dataTypeColumn = GetMetaDataColumn(DBObjects.Columns, "DATA_TYPE");
            string lengthColumn = GetMetaDataColumn(DBObjects.Columns, "LENGTH");
            string numericPrecisionColumn = GetMetaDataColumn(DBObjects.Columns, "NUMERIC_PRECISION");
            string numericScaleColumn = GetMetaDataColumn(DBObjects.Columns, "NUMERIC_SCALE");
            string primaryKeyColumn = GetMetaDataColumn(DBObjects.Columns, "IS_PRIMARY_KEY");
            string isNullableColumn = GetMetaDataColumn(DBObjects.Columns, "IS_NULLABLE");
            string uniqueColumn = GetMetaDataColumn(DBObjects.Columns, "IS_UNIQUE");
            string defaultColumn = GetMetaDataColumn(DBObjects.Columns, "DEFAULT_VALUE");
            string ownerColumn = GetMetaDataColumn(DBObjects.Columns, "OWNER");
            string edmTypeColumn = GetMetaDataColumn(DBObjects.Columns, "EDM_TYPE");

            foreach (DataRow tableItem in dataset.Tables[DBObjects.Tables.ToString()]
                                        .AsEnumerable()
                                        .Where(row => TableFilter(row))
                                        .OrderBy(table => table.Field<string>(tableNameColumn)))
            {
                string tableName = tableItem.Field<string>(tableNameColumn);
                var tableRow = dbStructure.Table.AddTableRow(tableName, GetValue(tableDefColumn, tableItem));
                
                foreach (DataRow columnItem in dataset.Tables[DBObjects.Columns.ToString()].AsEnumerable()
                    .Where(row => row.Field<string>(tableNameColumn) == tableName)
                    .OrderBy(row => row.Field<string>(columnNameColumn)))
                {
                    dbStructure.Column.AddColumnRow(
                        columnItem.Field<string>(columnNameColumn)
                        , tableRow
                        , GetValue(idColumn, columnItem)
                        , columnItem.Field<string>(dataTypeColumn)
                        , GetValue(lengthColumn, columnItem)
                        , GetValue(numericPrecisionColumn, columnItem)
                        , GetValue(numericScaleColumn, columnItem)
                        , GetValue(primaryKeyColumn, columnItem)
                        , GetValue(isNullableColumn, columnItem)
                        , GetValue(uniqueColumn, columnItem)
                        , GetValue(defaultColumn, columnItem)
                        , GetValue(ownerColumn, columnItem)
                        , GetValue(edmTypeColumn, columnItem)
                        );
                }
            }
        }
        
        protected virtual bool TableFilter(DataRow row)
        {
            return true;
        }

        protected static string GetValue(string edmTypeColumn, DataRow columnItem)
        {
            return edmTypeColumn != null ? Convert.ToString(columnItem[edmTypeColumn]) : "";
        }

        public virtual void FillViews(DataSet dataset, DbStructureDataset dbStructure)
        {
            string viewNameColumn = GetMetaDataColumn(DBObjects.Views, "VIEW_NAME");
            string viewSqlColumn = GetMetaDataColumn(DBObjects.Views, "VIEW_DEFINITION");

            foreach (DataRow viewItem in dataset.Tables[DBObjects.Views.ToString()].AsEnumerable()
                .OrderBy(view => view.Field<string>(viewNameColumn)))
            {
                dbStructure.Views.AddViewsRow(viewItem.Field<string>(viewNameColumn)
                    , GetValue(viewSqlColumn, viewItem));
            }
        }

        public virtual void FillTriggers(DataSet dataset, DbStructureDataset dbStructure)
        {
            string triggerNameColumn = GetMetaDataColumn(DBObjects.Views, "TRIGGER_NAME");
            string triggerSqlColumn = GetMetaDataColumn(DBObjects.Views, "TRIGGER_DEFINITION");
            string tableNameColumn = GetMetaDataColumn(DBObjects.Tables, "TABLE_NAME");

            foreach (DataRow triggerItem in dataset.Tables[DBObjects.Triggers.ToString()].AsEnumerable()
                .OrderBy(trigger => trigger.Field<string>(triggerNameColumn)))
            {
                dbStructure.Triggers.AddTriggersRow(triggerItem.Field<string>(triggerNameColumn)
                    , GetValue(tableNameColumn, triggerItem)
                    , GetValue(triggerSqlColumn, triggerItem)
                );
            }
        }

        #endregion

        #region CL
        
        public int NonQueryMethod(string cmd)
        {
            try
            {
                OpenConnection();
                return DBConnection.NonQueryCommand(cmd);
            }
            finally
            {
                DBConnection.CloseConnection();
            }
        }

        public bool ScriptMethod(string script)
        {
            string error;
            DBConnection.OpenConnection(out error);
            string[] list = script.Split(new char[]{'/'}, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                foreach (string cmd in list)
                {
                    DBConnection.NonQueryCommand(cmd.Replace(Environment.NewLine, " ").Replace("  ", " ").Trim());
                }
            }
            finally
            {
                DBConnection.CloseConnection();
            }
            return string.IsNullOrEmpty(error);
        }

        public object ScalarMethod(string cmd)
        {
            try
            {
                OpenConnection();
                return DBConnection.ScalarCommand(cmd);
            }
            finally
            {
                DBConnection.CloseConnection();
            }
        }

        public virtual int MethodWithParams(string cmd, List<DbParameter> parameters)
        {
            try
            {
                OpenConnection();
                DbParameterCollection ret = DBConnection.MethodWithParams(cmd, parameters);
                return 1;
            }
            finally
            {
                DBConnection.CloseConnection();
            }
        }
        
        #endregion

        #region DL

        public string SelectMethodToString(string cmd, string tableName, List<Parameter> parameters)
        {
            return DBUtils.GetStringFromDataset(DBConnection.FillDataset(cmd, tableName, parameters));
        }

        public string SelectTableString(string cmd, string tableName)
        {
            return DBUtils.GetStringFromDataset(DBConnection.FillDataset(cmd, tableName));
        }

        public DataSet SelectMethodDataset(string cmd, string tableName, List<Parameter> parameters)
        {
            return DBConnection.FillDataset(cmd, tableName, parameters);
        }

        public virtual void UpdateDataSet(DataSet changed)
        {
            DBConnection.UpdateDataSet(changed);
        }        

        public virtual void InsertTable(DataTable dataTable)
        {
            DBConnection.InsertTable(dataTable);
        }

        #endregion

        public virtual string GetTime(DateTime dateTime)
        {
            return string.Format("{0}:{1}:{2}", dateTime.Hour, dateTime.Minute, dateTime.Second);
        }
        
        public virtual string GetDate(DateTime dateTime)
        {
            return string.Format("{0}/{1}/{2}", dateTime.Day, dateTime.Month, dateTime.Year);
        }
        
        public virtual long GetNextId(string tableName, string columnName)
        {
            string sql = string.Format("SELECT MAX({0}) FROM {1}", columnName, tableName);
            object ret = ScalarMethod(sql);
            if (ret != DBNull.Value)
                return Convert.ToInt64(ret) + 1;
            else
                return 0;
        }


        #region ABSTRACT
        
        public abstract string GetFunctionText(string name);

        public abstract string[] GetNamesByType(string type);

        public abstract void CreateTable(DataTable schemeTable);

        public abstract string RowLimitQuery{get;}
        
        #endregion
        
        #region METADATA

        public virtual string GetMetaDataColumn(DBObjects dbObj, string key)
        {
            if (metaDataObjectKeys.ContainsKey(dbObj))
            {
                IDictionary<string, string> metaDataKeys = metaDataObjectKeys[dbObj];
                if (metaDataKeys.ContainsKey(key))
                {
                    return metaDataKeys[key];
                }
            }
            return null;
        }

        public virtual string GetMetaDataObject(DBObjects dbObj)
        {
            switch (dbObj)
            {
                case DBObjects.Tables:
                case DBObjects.Columns:
                case DBObjects.Views:
                case DBObjects.Triggers:
                    return dbObj.ToString();
                default:
                    return null;
            }
        }
 
        public IEnumerable<string> GetVisibleColumns(DBObjects dbObj)
        {
            if (metaDataObjectKeys.ContainsKey(dbObj))
            {
                return metaDataObjectKeys[dbObj].Keys;
            }
            return new string[0];
        }

        #endregion

        public virtual string GetDBFileName()
        {
            return null;
        }

        public virtual bool IsDBOnline()
        {
            return false;
        }
    }
}


