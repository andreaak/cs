using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Utils.WorkWithDB.Connections
{
    public interface IDBConnection
    {
        DataBaseType DbType
        {
            set;
            get;
        }

        string ConnectionString
        {
            get;
        }

        bool OpenConnection(out string error);
        
        bool CloseConnection();

        DataTable GetDBScheme(string name, string[] restrictions);

        DataTable GetTableScheme(string tableName);
        
        DbDataReader ExecuteReaderCommand(string sql);

        object ScalarCommand(string cmd);

        int NonQueryCommand(string cmd);

        DbParameterCollection ExecuteNonQueryWithParams(string cmd, List<DbParameter> parameters);

        DataSet FillDataset(string sql, string tableName, List<DBParameter> parameters = null);

        void UpdateDataSet(DataSet changed);

        void InsertTable(DataTable dataTable);
    }
}
