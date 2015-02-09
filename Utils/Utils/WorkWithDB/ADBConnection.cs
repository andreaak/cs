using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
#if FIREBIRD
using FirebirdSql.Data.FirebirdClient;
#endif

using System.Data;
using System.Collections.Generic;

namespace Utils.WorkWithDB
{
    public enum DataBaseType
    {
        OleDb,
        SQLExpress,
        Firebird,
        SQLServer,
        Oracle,
        SQLite
    }
    
    public abstract class ADBConnection
    {

        
        private DbConnection connection;
        public DbConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }
        
        private DbCommand command;
        public DbCommand Command
        {
            get { return command; }
            set { command = value; }
        }

        bool isActiveConnection;
        public bool IsActiveConnection
        {
            get { return isActiveConnection; }
            set { isActiveConnection = value; }
        }

        private DataBaseType dbType;
        public DataBaseType DbType
        {
            get { return dbType; }
            set { dbType = value; }
        }

        public void PrepareOleDbConnection(String DataBasePath)
        {
            string sConnect = String.Format( @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}", DataBasePath);
            connection = new OleDbConnection(sConnect);
            dbType = DataBaseType.OleDb;
        }
        public void PrepareSQLExpressConnection(String DataBasePath)
        {
            string sConnect = String.Format(@"Data Source=.\SQLEXPRESS;AttachDbFilename={0};Integrated Security=True;Connect Timeout=30;User Instance=True", DataBasePath);
            connection = new SqlConnection(sConnect);
            dbType = DataBaseType.SQLExpress;
        }
        public void PrepareSQLServerConnection(String serverName, String dbName, String Login, String Password)
        {
            string sConnect = String.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3}", serverName, dbName, Login, Password);
            connection = new SqlConnection(sConnect);
            dbType = DataBaseType.SQLServer;
        } 
        #if FIREBIRD
        public void PrepareFirebirdConnection(String DataBasePath)
        {
            string sConnect = String.Format(@"User=SYSDBA;Password=masterkey;Database={0};DataSource=localhost;Charset=NONE;", DataBasePath);
            connection = new FbConnection(sConnect);
            dbType = DataBaseType.FirebirdType;
        }
#endif
        public bool OpenConnection(out string error)
        {
            error = null;
            if (isActiveConnection)
            {
                return true;
            }
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
            isActiveConnection = true;
            return true;
        }

        public bool CloseConnection()
        {
            if (!isActiveConnection)
            {
                return true;
            }
            try
            {
                connection.Close();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            { 
                isActiveConnection = false;
            }
            return true;
        }

        public DataTable GetDBScheme(string name, string[] restrictions)
        {
            DataTable dtScheme = null;
            if (string.IsNullOrEmpty(name))
            {
                dtScheme = Connection.GetSchema();
            }
            else if (restrictions == null || restrictions.Length == 0)
            {
                dtScheme = Connection.GetSchema(name);
            }
            else
            {
                dtScheme = Connection.GetSchema(name, restrictions);
            }
            return dtScheme;
        }

        public virtual DataTable GetTableScheme(string tableName)
        {
            string sql = string.Format("SELECT * FROM {0}", tableName);
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sql;
            DbDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable dtScheme = dr.GetSchemaTable();
            dtScheme.TableName = tableName;
            return dtScheme;
        }

        public abstract DbDataReader ExecuteReaderCommand(string sql);
        public abstract int NonQueryCommand(string sql);
        public abstract object ScalarCommand(string sql);
        public abstract DbParameterCollection MethodWithParams(string sql, List<DbParameter> parameters);


        public abstract DataSet FillDataset(string sql, string tableName, List<Parameter> parameters = null);
        public abstract void UpdateDataSet(DataSet changed);
        public abstract void InsertTable(DataTable table);
    }
}
