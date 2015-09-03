using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

namespace Utils.WorkWithDB.Connections
{
    
    public class DBConnectionCL : DBConnectionBase
    {

        public override DbDataReader ExecuteReaderCommand(string sql)
        {
            string error;
            if (!OpenConnection(out error))
            {
                throw new Exception(error);
            }
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sql;
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);

        }

        public override int NonQueryCommand(string sql)
        {
            string error;
            if (!OpenConnection(out error))
            {
                throw new Exception(error);
            }
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sql;
            try
            {
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }

        public override object ScalarCommand(string sql)
        {
            string error;
            if (!OpenConnection(out error))
            {
                throw new Exception(error);
            }
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sql;
            object outObject = null;
            try
            {
                outObject = cmd.ExecuteScalar();
            }
            finally
            {
                CloseConnection();
            }


            return outObject;
        }

        public override DbParameterCollection ExecuteNonQueryWithParams(string sql, List<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public override DataSet FillDataset(string sql, string tableName, List<DBParameter> parameters)
        {
            throw new NotSupportedException();
        }

        public override void UpdateDataSet(DataSet changed)
        {
            throw new NotSupportedException();
        }

        public override void InsertTable(DataTable table)
        {
            throw new NotSupportedException();
        }
    }
}
