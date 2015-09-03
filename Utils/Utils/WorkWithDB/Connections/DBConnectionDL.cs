using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
#if FIREBIRD
using FirebirdSql.Data.FirebirdClient;
#endif

namespace Utils.WorkWithDB.Connections
{
    public class DBConnectionDL : DBConnectionBase
    {
        private Dictionary<string, DbDataAdapter> dataAdapters;

        private DataSet storage;
        public DataSet Storage
        {
            get { return storage; }
            protected set { storage = value; }
        }

        private DbDataAdapter dataAdapter;
        public DbDataAdapter DataAdapter
        {
            get { return dataAdapter; }
            set { dataAdapter = value; }
        }

        public Dictionary<string, DbDataAdapter> CreateAdapters(Dictionary<string, string> tablesNameAndCommands)
        {
            if (Connection == null)
            {
                throw new Exception("Connection is not initialized");
            }

            dataAdapters = new Dictionary<string, DbDataAdapter>();

            switch (DbType)
            {
                case DataBaseType.OleDb:
                    foreach (string tableName in tablesNameAndCommands.Keys)
                    {
                        dataAdapters.Add(tableName, new OleDbDataAdapter(tablesNameAndCommands[tableName], (OleDbConnection)Connection));
                    }
                    return dataAdapters;
                case DataBaseType.SQLExpress:
                case DataBaseType.SQLServer:
                    foreach (string tableName in tablesNameAndCommands.Keys)
                    {
                        dataAdapters.Add(tableName, new SqlDataAdapter(tablesNameAndCommands[tableName], (SqlConnection)Connection));
                    }
                    return dataAdapters;
                #if FIREBIRD
                case DataBaseType.FirebirdType:
                    foreach (string tableName in tablesNameAndCommands.Keys)
                    {
                        dataAdapters.Add(tableName, new FbDataAdapter(tablesNameAndCommands[tableName], (FbConnection)Connection));
                    }
                    return true;
                #endif
                default:
                    throw new Exception("Unknown Database type");
            }
        }

        public DataSet FillDataset(Dictionary<string, DbDataAdapter> dataAdapters)
        {
            DataSet temp = new DataSet();
            try
            {
                foreach (string tableName in dataAdapters.Keys)
                {
                    dataAdapters[tableName].Fill(temp);
                    //DataTable tempTable = temp.Tables[0].Copy();
                    //tempTable.TableName = tableName;
                    //Storage.Tables.Add(tempTable);
                }
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException("ERROR Fill dataset", ex);
            }
            return temp;
        }

        public void CreateRelations(List<Relation> data, DataSet ds)
        {
            foreach (Relation relation in data)
            {
                CreateRelation(relation, ds);
            }
        }        

        public void CreateRelation(Relation rel, DataSet ds)
        {
            if (string.IsNullOrEmpty(rel.Name))
            {
                rel.Name = string.Format("{0}.{1}->{2}.{3}", rel.BaseTable, rel.BaseColumn, rel.RelTable, rel.RelColumn);
            }

            ds.Relations.Add(rel.Name,
            ds.Tables[rel.BaseTable].Columns[rel.BaseColumn],
            ds.Tables[rel.RelTable].Columns[rel.RelColumn]);
        }
        ////Example
        //DBConnectionDL TempConnectionBase = new DBConnectionDL();
        //TempConnectionBase.OpenOleDbConnection((string)myset.GetSettings("MainDataBasePath"));
        //GetDataFromDB(TempConnectionBase);
        ////
        //BaseStorage = TempConnectionBase.Storage;
        ////
        //internal static void GetDataFromDB(DBConnectionDL Connection)
        //{
        //    //Имя таблицы и команда выборки для нее
        //    Dictionary<string, string> commands = new Dictionary<string, string>();
        //    commands.Add("tblPart", "SELECT * FROM tblPart");
        //    commands.Add("tblFreeProperty", "SELECT * FROM tblFreeProperty");
        //    commands.Add("tblFunctionTemplate", "SELECT * FROM tblFunctionTemplate");
        //    commands.Add("tblMechanic", "SELECT * FROM tblMechanic");
        //    commands.Add("tblVariant", "SELECT * FROM tblVariant");
        //    Connection.CreateAdapters(commands);
        //    //
        //    Connection.FillDataset();
        //    //Создание отношений
        //    List<string[]> data = new List<string[]>();
        //    string[] temp = new string[] { "tblFreeProperty", "tblPart", "id", "tblFreeProperty", "id" };
        //    data.Add(temp);
        //    temp = new string[] { "tblFunctionTemplate", "tblPart", "id", "tblFunctionTemplate", "id" };
        //    data.Add(temp);
        //    temp = new string[] { "tblMechanic", "tblPart", "id", "tblMechanic", "id" };
        //    data.Add(temp);
        //    temp = new string[] { "tblVariant", "tblPart", "id", "tblVariant", "id" };
        //    data.Add(temp);
        //    Connection.CreateRelations(data);
        //}

        //Вставка строки в таблицу
        public void InsertRow(DataRow TempRow, String TableName)
        {
            if(TempRow.RowState==DataRowState.Unchanged)
                TempRow.SetAdded();
            
            Storage.Tables[TableName].ImportRow(TempRow);

        }
        ////Example 
        //foreach (DataRow tblPartRow in QueryResult)
        //{
            
        //    tblPartRow.SetAdded();
        //    var rows2 = TempConnectionBase.Storage.Tables["tblPart"].AsEnumerable();
        //    var QueryResult2 =
        //    (from n in rows2
        //    select n["id"]).Max();
        //    Int32 Counter = ((Int32)QueryResult2) + 1;
        //    tblPartRow["id"] = Counter;
        //    TempConnectionBase.InsertRow(tblPartRow, "tblPart");

        //    Dictionary<string, string> relationAndTablesNames = new Dictionary<string, string>();
        //    relationAndTablesNames.Add("tblFreeProperty", "tblFreeProperty");
        //    relationAndTablesNames.Add("tblFunctionTemplate", "tblFunctionTemplate");
        //    relationAndTablesNames.Add("tblMechanic", "tblMechanic");
        //    relationAndTablesNames.Add("tblVariant", "tblVariant");
        //    foreach (string relationName in relationAndTablesNames.Keys)
        //    {
        //        if (DataStorage.Relations.Contains(relationName))
        //        {
        //            foreach (DataRow row in tblPartRow.GetChildRows(DataStorage.Relations[relationName]))
        //            {
        //                TempConnectionBase.InsertRow(row, relationAndTablesNames[relationName]);
        //            }
        //        }
        //    }
               
        //    break;
        //}
        
        //Удаление строки
        public void DeleteRow(DataRow TempRow, String TableName)
        {
            Int32 RowForDeleteIndex = Storage.Tables[TableName].Rows.IndexOf(TempRow);
            Storage.Tables[TableName].Rows[RowForDeleteIndex].Delete();
        } 
        ////Example 
        //foreach (DataRow tblPartRow in QueryResult)
        //{
        //    Dictionary<string, string> relationAndTablesNames = new Dictionary<string, string>();
        //    relationAndTablesNames.Add("tblFreeProperty", "tblFreeProperty");
        //    relationAndTablesNames.Add("tblFunctionTemplate", "tblFunctionTemplate");
        //    relationAndTablesNames.Add("tblMechanic", "tblMechanic");
        //    relationAndTablesNames.Add("tblVariant", "tblVariant");
        //    foreach (string relationName in relationAndTablesNames.Keys)
        //    {
        //        if (DataStorage.Relations.Contains(relationName))
        //        {
        //            foreach (DataRow row in tblPartRow.GetChildRows(DataStorage.Relations[relationName]))
        //            {
        //                TempConnectionBase.DeleteRow(row, relationAndTablesNames[relationName]);
        //            }
        //        }
        //    }

        //    RowsForDelete.Add(tblPartRow);
        //    break;
        //}
        //foreach (DataRow tblPartRow in RowsForDelete)
        //{
        //    TempConnectionBase.DeleteRow(tblPartRow, "tblPart");
        //}

        //Обновление базы
        public bool UpdateDataset(string tableName)
        {
            bool isOk = false;
            if(!dataAdapters.ContainsKey(tableName))
            {
                return isOk;
            }
            DataAdapter = dataAdapters[tableName];

            DbCommandBuilder builder = null;
            switch (DbType)
            {
                case DataBaseType.OleDb:
                    builder= new OleDbCommandBuilder((OleDbDataAdapter)DataAdapter);                    
                    break;
                case DataBaseType.SQLExpress:
                    builder = new SqlCommandBuilder((SqlDataAdapter)DataAdapter);
                    break;
                #if FIREBIRD
                case DataBaseType.FirebirdType:
                    builder = new FbCommandBuilder((FbDataAdapter)DataAdapter);
                    break;
                #endif
                default: 
                    return false;
            }
            //
            try
            {
                DataAdapter.Update(Storage.Tables[tableName]);
                isOk = true;
            }
            catch (System.Exception ex)
            {
                builder.Dispose();
                throw new ApplicationException("ERROR Update dataset", ex);
            }
            builder.Dispose();
            return isOk;
        }

        private static void ReplaceCommandText(Dictionary<string, string> replaces, DbCommand command)
        {
            if (replaces != null && command != null)
            {
                foreach (string replace in replaces.Keys)
                {
                    command.CommandText = command.CommandText.Replace(replace, replaces[replace]);
                }
            }
        }
        ////Example
        //Dictionary<string, string> commands = new Dictionary<string, string>();
        //commands.Add("Insert", "Insert");
        //commands.Add("Update", "Insert");
        //commands.Add("Delete", "Delete");

        //Dictionary<string, string> replaces = new Dictionary<string, string>();
        //replaces.Add(" class,", " [class],");
        //replaces.Add(" create,", " [create],");
        //replaces.Add(" note,", " [note],");
        //replaces.Add(" usage,", " [usage],");

        //replaces.Add(" color,", " [color],");
        //replaces.Add(" connection,", " [connection],");
        //replaces.Add(" variant,", " [variant],");
        //replaces.Add(" thread,", " [thread],");

        //TempConnectionBase.UpdateDataset("tblPart", commands, replaces);
        //TempConnectionBase.UpdateDataset("tblFreeProperty", commands, replaces);
        //TempConnectionBase.UpdateDataset("tblFunctionTemplate", commands, replaces);
        //TempConnectionBase.UpdateDataset("tblMechanic", commands, replaces);
        //TempConnectionBase.UpdateDataset("tblVariant", commands, replaces);

        //
        private DbCommand GetCommand(string commandType, DbCommandBuilder builder)
        {
            switch (commandType)
            {
                case "Insert":
                    return builder.GetInsertCommand();
                case "Update":
                    return builder.GetUpdateCommand();
                case "Delete":
                    return builder.GetDeleteCommand();
                default:
                    return null;
            }
        }

        public override DbDataReader ExecuteReaderCommand(string sql)
        {
            throw new NotSupportedException();
        }

        public override int NonQueryCommand(string sql)
        {
            throw new NotSupportedException();
        }

        public override object ScalarCommand(string sql)
        {
            throw new NotSupportedException();
        }

        public override DbParameterCollection ExecuteNonQueryWithParams(string sql, List<DbParameter> parameters)
        {
            throw new NotSupportedException();
        }

        public override DataSet FillDataset(string sql, string tableName, List<DBParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public override void UpdateDataSet(DataSet changed)
        {
            throw new NotImplementedException();
        }

        public override void InsertTable(DataTable table)
        {
            throw new NotImplementedException();
        }
    }

    public class Relation
    {
        public string Name { get; set; }
        public string BaseTable { get; set; }
        public string BaseColumn { get; set; }
        public string RelTable { get; set; }
        public string RelColumn { get; set; } 
    }
}
