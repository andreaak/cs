using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Threading;

namespace Utils.WorkWithDB.Wrappers
{
    enum UpdateType
    {
        Insert,
        Update, 
        Delete,
    }
    public class DBConnectionFactory : ADBConnection
    {
        private DbProviderFactory df;

        public DBConnectionFactory(string provider, string connString)
        {
            InitConnection(provider, connString);
        }

        public DBConnectionFactory()
        {
            InitConnectionFromConfig();
        }

        private void InitConnectionFromConfig()
        {
            string dp = OptionsUtils.Provider;
            string cnStr = OptionsUtils.ConnectionString;
            InitConnection(dp, cnStr);
        }

        private void InitConnection(string provider, string connString)
        {
            df = DbProviderFactories.GetFactory(provider);
            Connection = df.CreateConnection();
            Connection.ConnectionString = connString;
        }

        public override DataTable GetTableScheme(string tableName)
        {
            string sql = string.Format("SELECT * FROM {0}", tableName);
            CreateDbCommand(sql);
            DbDataReader dr = Command.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable dtScheme = dr.GetSchemaTable();
            dtScheme.TableName = tableName;
            return dtScheme;
        }

        #region CL

        public override DbDataReader ExecuteReaderCommand(string sql)
        {
            CreateDbCommand(sql);
            DbDataReader dr = Command.ExecuteReader();
            return dr;
        }

        public override int NonQueryCommand(string sql)
        {
            CreateDbCommand(sql);
            return Command.ExecuteNonQuery();
        }

        public override object ScalarCommand(string sql)
        {
            CreateDbCommand(sql);
            return Command.ExecuteScalar();
        }

        public override DbParameterCollection MethodWithParams(string sql, List<DbParameter> parameters)
        {
            CreateDbCommand(sql);
            Command.Parameters.AddRange(parameters.ToArray());
            Command.ExecuteNonQuery();

            return Command.Parameters;
        }

        #endregion

        #region FACTORY

        public DbProviderFactory DBfactory
        {
            get
            {
                if (df == null)
                {
                    InitConnectionFromConfig();
                }
                return df;
            }
        }
        
        private void CreateDbCommand(string sql)
        {
            Command = df.CreateCommand();
            Command.Connection = Connection;
            Command.CommandText = sql;
            Command.CommandType = CommandType.Text;
        }

        private DbParameter CreateDbParameter(Parameter param)
        {
            DbParameter outParam = df.CreateParameter();
            outParam.ParameterName = param.Name;
            outParam.DbType = param.Type;
            outParam.Value = param.Value;
            outParam.Direction = param.Direction;
            return outParam;
        }

        private void CreateDbProcCommand(string sql)
        {
            Command = df.CreateCommand();
            Command.Connection = Connection;
            Command.CommandText = sql;
            Command.CommandType = CommandType.StoredProcedure;
        }

        public DbParameter GetParameter()
        {
            return df.CreateParameter();
        }

        #endregion

        #region DL

        public override DataSet FillDataset(string sql, string tableName, List<Parameter> parameters = null)
        {
            CreateDbCommand(sql);
            if (parameters != null && parameters.Count > 0)
            {
                Command.Parameters.AddRange(parameters.ConvertAll(CreateDbParameter).ToArray());
            }
            DbDataAdapter dataAdapter = df.CreateDataAdapter();
            dataAdapter.SelectCommand = Command;
            DataSet ds = new DataSet();
            try
            {
                dataAdapter.Fill(ds, tableName);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Fill Dataset: " + ex);
            }
            finally
            {
                dataAdapter.Dispose();
            }
            return ds;
        }

        public override void UpdateDataSet(DataSet changed)
        {
            UpdateDataSet(changed, null, null, null);
        }

        public void UpdateDataSet(DataSet changed, List<string> deleteQueue,
                                List<string> addedQueue, List<string> modifiedQueue)
        {
            //queue- list affected tables;
            UpdateDataSet(changed, deleteQueue, DataRowState.Deleted);

            UpdateDataSet(changed, addedQueue, DataRowState.Added);

            UpdateDataSet(changed, modifiedQueue, DataRowState.Modified);

        }

        public override void InsertTable(DataTable table)
        {
            UpdateTable(table, DataRowState.Added);
        }

        private void UpdateDataSet(DataSet changed, List<string> queue, DataRowState state)
        {
            if (queue != null)
            {
                foreach (string tableName in queue)
                {
                    UpdateTable(state, changed.Tables[tableName]);
                }
            }
            else
            {
                foreach (DataTable table in changed.Tables)
                {
                    UpdateTable(state, table);
                }
            }
        }

        private void UpdateTable(DataRowState state, DataTable table)
        {
            DataTable operationTable = table.GetChanges(state);
            if (IsValidTable(operationTable))
            {
                UpdateTable(operationTable, state);
            }
        }

        private void UpdateTable(DataTable table, DataRowState state)
        {
            if (table == null || table.Rows.Count == 0)
            {
                return;
            }
            string sql = string.Format("SELECT * FROM {0}", table.TableName);
            CreateDbCommand(sql);

            DbDataAdapter dataAdapter = df.CreateDataAdapter();
            dataAdapter.SelectCommand = Command;

            DbCommandBuilder builder = df.CreateCommandBuilder();
            builder.DataAdapter = dataAdapter;
            SetDACommand(state, builder, dataAdapter);

            UpdateDB(table, builder, dataAdapter, state);
        }

        private void SetDACommand(DataRowState state, DbCommandBuilder builder, DbDataAdapter dataAdapter)
        {
            switch (state)
            {
                case DataRowState.Added:
                    dataAdapter.InsertCommand = builder.GetInsertCommand();
                    break;
                case DataRowState.Modified:
                    dataAdapter.UpdateCommand = builder.GetUpdateCommand();
                    break;
                case DataRowState.Deleted:
                    dataAdapter.DeleteCommand = builder.GetDeleteCommand();
                    break;
                default:
                    dataAdapter.InsertCommand = builder.GetInsertCommand();
                    dataAdapter.UpdateCommand = builder.GetUpdateCommand();
                    dataAdapter.DeleteCommand = builder.GetDeleteCommand();
                    break;
            }
        }

        private void UpdateDB(DataTable table, DbCommandBuilder builder, DbDataAdapter dataAdapter, DataRowState state)
        {
            try
            {
                dataAdapter.Update(table);
            }
            catch (System.Exception ex)
            {
                throw new Exception(string.Format("ERROR in {0} row: {1}", state, ex));
            }
            finally
            {
                builder.Dispose();
            }
        }

        #endregion
 
        private bool IsValidTable(DataTable operationTable)
        {
            return operationTable != null && operationTable.Rows.Count != 0;
        }
    }
}