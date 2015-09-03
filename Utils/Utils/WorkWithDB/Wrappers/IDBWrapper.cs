using System.Collections.Generic;
using System.Data;
using Utils.WorkWithDB.Connections;

namespace Utils.WorkWithDB.Wrappers
{
    public interface IDBWrapper
    {
        string ConnectionString
        {
            get;
        }

        string RowLimitQuery
        {
            get;
        }

        DataBaseType DbType
        {
            get;
        }

        bool CreateDB(string file, string script);

        bool OpenConnection();

        bool CloseConnection();

        object ScalarCommand(string query);

        int NonQueryCommand(string query);
        
        DataSet SelectMethodDataset(string cmd, string tableName, List<DBParameter> parameters);

        void CreateTable(System.Data.DataTable schemeTable);

        void InsertTable(System.Data.DataTable dataTable);

        long GetNextId(string tableName, string idColumnName);

        string GetMetaDataColumn(DBObjects dbObj, string key);

        string GetMetaDataObject(DBObjects dbObj);

        IEnumerable<string> GetVisibleColumns(DBObjects selItem);

        void FillTables(DataSet dataset, DbStructureDataset dbStructure);

        void FillViews(DataSet dataset, DbStructureDataset dbStructure);

        void FillTriggers(DataSet dataset, DbStructureDataset dbStructure);

        string GetFunctionText(string name);

        string[] GetNamesByType(string type);

        string GetDBScheme(string name, string[] restrictions);

        string GetTableScheme(string name);

        bool IsDBOnline();

        string GetDBFileName();

        string SelectTableString(string cmd, string name);

        bool ExecuteScript(string script);
    }
}
