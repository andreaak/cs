using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Threading;

namespace Utils.WorkWithDB.Connections
{
    enum UpdateType
    {
        Insert,
        Update, 
        Delete,
    }

    public class DBConnection : DBConnectionBase, IDBConnection
    {
        private DbProviderFactory df;

        public string ConnectionString
        {
            get
            {
                return Connection.ConnectionString;
            }
        }

        public DBConnection()
            : this(OptionsUtils.Provider, OptionsUtils.ConnectionString)
        {
        }
        
        public DBConnection(string provider, string connString)
        {
            df = DbProviderFactories.GetFactory(provider);
            Connection = df.CreateConnection();
            Connection.ConnectionString = connString;
        }

        #region CONNECTED LAYER

        public override DbDataReader ExecuteReaderCommand(string sql)
        {
            DbCommand command = CreateDbCommand(sql);
            DbDataReader dr = command.ExecuteReader();
            return dr;
        }

        public override int NonQueryCommand(string sql)
        {
            DbCommand command = CreateDbCommand(sql);
            return command.ExecuteNonQuery();
        }

        public override object ScalarCommand(string sql)
        {
            DbCommand command = CreateDbCommand(sql);
            return command.ExecuteScalar();
        }

        public override DbParameterCollection ExecuteNonQueryWithParams(string sql, List<DbParameter> parameters)
        {
            DbCommand command = CreateDbCommand(sql);
            command.Parameters.AddRange(parameters.ToArray());
            command.ExecuteNonQuery();

            return command.Parameters;
        }

        #endregion

        #region COMMAND

        protected override DbCommand CreateDbCommand(string sql)
        {
            DbCommand command = df.CreateCommand();
            command.Connection = Connection;
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            return command;
        }

        private DbParameter CreateDbParameter(DBParameter param)
        {
            DbParameter outParam = df.CreateParameter();
            outParam.ParameterName = param.Name;
            outParam.DbType = param.Type;
            outParam.Value = param.Value;
            outParam.Direction = param.Direction;
            return outParam;
        }

        private DbCommand CreateDbProcCommand(string sql)
        {
            DbCommand command = df.CreateCommand();
            command.Connection = Connection;
            command.CommandText = sql;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        public DbParameter GetParameter()
        {
            return df.CreateParameter();
        }

        #endregion

        #region DL

        public override DataSet FillDataset(string sql, string tableName, List<DBParameter> parameters = null)
        {
            DbCommand command = CreateDbCommand(sql);
            if (parameters != null && parameters.Count > 0)
            {
                command.Parameters.AddRange(parameters.ConvertAll(CreateDbParameter).ToArray());
            }
            DbDataAdapter dataAdapter = df.CreateDataAdapter();
            dataAdapter.SelectCommand = command;
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

            DbDataAdapter dataAdapter = df.CreateDataAdapter();
            DbCommand command = CreateDbCommand(sql);
            dataAdapter.SelectCommand = command;

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

        private bool IsValidTable(DataTable operationTable)
        {
            return operationTable != null && operationTable.Rows.Count != 0;
        }

        #endregion
    }
}