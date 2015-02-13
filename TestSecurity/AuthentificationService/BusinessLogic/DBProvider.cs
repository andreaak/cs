using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AuthentificationService.BusinessLogic
{
    public class DBProvider : IDisposable
    {
        private static DbProviderFactory factory;
        private static ConnectionStringSettings settings;
        
        private DbConnection conn;

        static DBProvider()
        {
            settings = ConfigurationManager.ConnectionStrings[Constants.connectionString];
            factory = DbProviderFactories.GetFactory(settings.ProviderName);
        }

        public DBProvider()
        {
            InitConnectionFromConfig();
        }

        #region PUBLIC

        public string TestConnection()
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        public bool OpenConnection()
        {
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool CloseConnection()
        {
            try
            {
                conn.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public DbDataReader ExecuteReader(string sql, params DbParameter[] parameters)
        {
            DbCommand command = CreateDbCommand(sql);
            if (parameters.Length != 0)
            {
                command.Parameters.AddRange(parameters);
            }
            return command.ExecuteReader();
        }

        public int NonQueryCommand(string sql, params DbParameter[] parameters)
        {
            DbCommand command = CreateDbCommand(sql);
            if (parameters.Length != 0)
            {
                command.Parameters.AddRange(parameters);
            }
            return command.ExecuteNonQuery();
        }

        public void BatchInsert(DataTable table)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(settings.ConnectionString))
            {
                bulkCopy.BulkCopyTimeout = 5; // in seconds
                bulkCopy.DestinationTableName = table.TableName;
                bulkCopy.WriteToServer(table);
            }
        }

        #endregion

        #region PRIVATE

        private void InitConnectionFromConfig()
        {
            conn = factory.CreateConnection();
            conn.ConnectionString = settings.ConnectionString;
        }

        private DbCommand CreateDbCommand(string sql)
        {
            DbCommand command = factory.CreateCommand();
            command.Connection = conn;
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            return command;
        }

        public DbParameter GetParameter()
        {
            return factory.CreateParameter();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            CloseConnection();
        }

        #endregion

    }
}