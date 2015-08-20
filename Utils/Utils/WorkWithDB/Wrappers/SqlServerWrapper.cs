using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Data.SqlClient;

namespace Utils.WorkWithDB.Wrappers
{
    public class SqlServerWrapper : ADBWrapper
    {
        public SqlServerWrapper()
        {
            DBConnection.DbType = DataBaseType.SQLServer;
            Init();
        }

        public SqlServerWrapper(string provider, string connString)
            :base(provider, connString)
        {
            DBConnection.DbType = DataBaseType.SQLServer;
            Init();
        }

        protected override void Init()
        {
            metaDataObjectKeys = new Dictionary<DBObjects, IDictionary<string, string>>();
            IDictionary<string, string> metaDataKeys = new Dictionary<string, string>();
            metaDataKeys.Add("TABLE_NAME", "TABLE_NAME");
            metaDataObjectKeys.Add(DBObjects.Tables, metaDataKeys);

            metaDataKeys = new Dictionary<string, string>();
            metaDataKeys.Add("COLUMN_NAME", "COLUMN_NAME");
            metaDataKeys.Add("ID", "ORDINAL_POSITION");
            metaDataKeys.Add("DATA_TYPE", "DATA_TYPE");
            metaDataKeys.Add("LENGTH", "CHARACTER_MAXIMUM_LENGTH");
            metaDataKeys.Add("NUMERIC_PRECISION", "NUMERIC_PRECISION");
            metaDataKeys.Add("NUMERIC_PRECISION_RADIX", "NUMERIC_PRECISION_RADIX");
            metaDataKeys.Add("NUMERIC_SCALE", "NUMERIC_SCALE");
            metaDataObjectKeys.Add(DBObjects.Columns, metaDataKeys);

            metaDataKeys = new Dictionary<string, string>();
            metaDataKeys.Add("VIEW_NAME", "TABLE_NAME");
            metaDataObjectKeys.Add(DBObjects.Views, metaDataKeys);

            metaDataKeys = new Dictionary<string, string>();
            metaDataKeys.Add("TRIGGER_NAME", "TRIGGER_NAME");
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
                sql = string.Format("select distinct type_desc from sys.objects");
            }
            else
            {
                sql = string.Format("select distinct name from sys.objects where type_desc = '{0}' order by name", type);
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

        public override string RowLimitQuery
        {
            get { return "SELECT TOP {1}  * FROM ({0}) AS TB"; }
        }

        public override string GetMetaDataObject(DBObjects dbObj)
        {
            switch (dbObj)
            {
                case DBObjects.Tables:
                case DBObjects.Columns:
                case DBObjects.Views:
                    return dbObj.ToString();
                case DBObjects.Triggers:
                default:
                    return null;
            }
        }

        protected override bool TableFilter(DataRow row)
        {
            string tableType = row.Field<string>("TABLE_TYPE");
            return !string.IsNullOrEmpty(tableType) && tableType.Contains("TABLE");
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

        public override bool OpenConnection()
        {
            if (!IsSqlExpress())
            {
                return base.OpenConnection();
            }
            string file = GetDBFileName();
            if (string.IsNullOrEmpty(file))
            {
                throw new Exception("Wrong Connection String");
            }
            //file = file.Replace("DataDirectory", "").Replace("||", "|").Replace('|', '\\');
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
            return new SqlConnectionStringBuilder(DBConnection.Connection.ConnectionString).AttachDBFilename.Replace("DataDirectory", "").Replace("||", "|").Replace('|', '\\');
        }

        private bool IsSqlExpress()
        {
            return new SqlConnectionStringBuilder(DBConnection.Connection.ConnectionString).DataSource == @".\SQLEXPRESS";
        }
    }
}
