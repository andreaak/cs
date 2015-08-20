using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace Utils.WorkWithDB.Wrappers
{
    public class SQLiteDBWrapper : ADBWrapper
    {
        public SQLiteDBWrapper()
        {
            DBConnection.DbType = DataBaseType.SQLite;
            Init();
        }

        public SQLiteDBWrapper(string provider, string connString)
            :base(provider, connString)
        {
            DBConnection.DbType = DataBaseType.SQLite;
            Init();
        }

        protected override void Init()
        {
            metaDataObjectKeys = new Dictionary<DBObjects, IDictionary<string, string>>();
            IDictionary<string, string> metaDataKeys = new Dictionary<string, string>();
            metaDataKeys.Add("TABLE_NAME", "TABLE_NAME");
            metaDataKeys.Add("TABLE_DEFINITION", "TABLE_DEFINITION");
            metaDataObjectKeys.Add(DBObjects.Tables, metaDataKeys);

            metaDataKeys = new Dictionary<string, string>();
            metaDataKeys.Add("COLUMN_NAME", "COLUMN_NAME");
            metaDataKeys.Add("ID", "ORDINAL_POSITION");
            metaDataKeys.Add("DATA_TYPE", "DATA_TYPE");
            metaDataKeys.Add("LENGTH", "CHARACTER_MAXIMUM_LENGTH");
            metaDataKeys.Add("NUMERIC_PRECISION", "NUMERIC_PRECISION");
            metaDataKeys.Add("NUMERIC_SCALE", "NUMERIC_SCALE");
            metaDataKeys.Add("IS_PRIMARY_KEY", "PRIMARY_KEY");
            metaDataKeys.Add("IS_NULLABLE", "IS_NULLABLE");
            metaDataKeys.Add("IS_UNIQUE", "UNIQUE");
            metaDataKeys.Add("DEFAULT_VALUE", "COLUMN_DEFAULT");
            metaDataKeys.Add("OWNER", "TABLE_SCHEMA");
            metaDataKeys.Add("EDM_TYPE", "EDM_TYPE");
            metaDataObjectKeys.Add(DBObjects.Columns, metaDataKeys);

            metaDataKeys = new Dictionary<string, string>();
            metaDataKeys.Add("VIEW_NAME", "TABLE_NAME");
            metaDataKeys.Add("VIEW_DEFINITION", "VIEW_DEFINITION");
            metaDataObjectKeys.Add(DBObjects.Views, metaDataKeys);
            
            metaDataKeys = new Dictionary<string, string>();
            metaDataKeys.Add("TRIGGER_NAME", "TRIGGER_NAME");
            metaDataKeys.Add("TRIGGER_DEFINITION", "TRIGGER_DEFINITION");
            metaDataKeys.Add("TABLE_NAME", "TABLE_NAME");
            metaDataObjectKeys.Add(DBObjects.Triggers, metaDataKeys);
        }

        public override string GetFunctionText(string name)
        {
            throw new NotImplementedException();
        }

        public override string[] GetNamesByType(string type)
        {
            OpenConnection();

            string sql;
            if (string.IsNullOrEmpty(type))
            {
                sql = string.Format("select distinct type from sqlite_master");            
            }
            else
            {
                sql = string.Format("select distinct name from sqlite_master where type = '{0}' order by name", type);
            }

            List<string> temp = new List<string>();
            DbDataReader dr = DBConnection.ExecuteReaderCommand(sql);

            while (dr.Read())
            {
                temp.Add(dr.GetValue(0).ToString());
            }

            DBConnection.CloseConnection();

            return temp.ToArray();
        }

        public override void CreateTable(DataTable schemeTable)
        {
            throw new NotImplementedException();
        }

        public override bool CreateDB(string file, string script)
        {
            return CreateDBFile(file) && CreateDBStructure(script);
        }

        public override bool CreateDBFile(string file)
        {
            SQLiteConnection.CreateFile(file);
            return true;
        }

        public override string RowLimitQuery
        {
            get { return "SELECT * FROM ({0}) LIMIT {1}"; }
        }

        private const string DATASOURCE = "Data Source";
        public override bool OpenConnection()
        {
            string file = GetDBFileName();
            if (string.IsNullOrEmpty(file))
            {
                throw new Exception("Wrong Connection String");
            }

            if (!File.Exists(System.Windows.Forms.Application.StartupPath + Path.DirectorySeparatorChar + file)
                && !File.Exists(file))
            {
                throw new DbFileNotExistException();
            }

            string error;
            bool res = DBConnection.OpenConnection(out error);
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
            return res;
        }

        public override string GetDBFileName()
        {
            return new SQLiteConnectionStringBuilder(DBConnection.Connection.ConnectionString).DataSource;
        }

        public override bool IsDBOnline()
        {
            try 
	        {	        
                return OpenConnection();
	        }
	        finally
	        {
                DBConnection.CloseConnection();
	        }
        }
    }
}
