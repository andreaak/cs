using System;
using System.Collections.Generic;
using System.Linq;
using Utils.WorkWithDB;
using System.Data;
using Utils.WorkWithDB.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;

namespace Tests
{
    [TestClass]
    static class DBTest
    {
        static ADBWrapper dbWrapper = null;

        [TestMethod]
        public static void Test7()
        {

            string dataBasePath = @"eplan.mdb";
            DBConnectionDL dbConnection = new DBConnectionDL();
            //string connectionString="Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|eplan.mdb;Integrated Security=True;Connect Timeout=30;User Instance=True" ;
            dbConnection.PrepareOleDbConnection(System.Windows.Forms.Application.StartupPath + "\\" + dataBasePath);
            //dbConnection.PrepareSQLServerConnection(@"SQLSERVER\TEST", @"Eplan", "Andrea", "1");
            //Имя таблицы и команда выборки для нее
            Dictionary<string, string> commands = new Dictionary<string, string>();
            commands.Add("tblPart", "SELECT * FROM tblPart");
            commands.Add("tblFreeProperty", "SELECT * FROM tblFreeProperty");
            commands.Add("tblFunctionTemplate", "SELECT * FROM tblFunctionTemplate");
            commands.Add("tblMechanic", "SELECT * FROM tblMechanic");
            commands.Add("tblVariant", "SELECT * FROM tblVariant");
            Dictionary<string, DbDataAdapter> adapters = dbConnection.CreateAdapters(commands);
            DataSet ds = dbConnection.FillDataset(adapters);

            //Создание отношений
            List<Relation> data = new List<Relation>();
            data.Add(new Relation { Name = "tblFreeProperty", BaseTable = "tblPart", BaseColumn = "id", RelTable = "tblFreeProperty", RelColumn = "id" });
            data.Add(new Relation { Name = "tblFunctionTemplate", BaseTable = "tblPart", BaseColumn = "id", RelTable = "tblFunctionTemplate", RelColumn = "id" });
            data.Add(new Relation { Name = "tblMechanic", BaseTable = "tblPart", BaseColumn = "id", RelTable = "tblMechanic", RelColumn = "id" });
            data.Add(new Relation { Name = "tblVariant", BaseTable = "tblPart", BaseColumn = "id", RelTable = "tblVariant", RelColumn = "id" });
            dbConnection.CreateRelations(data, ds);
            //
            String DataPartNumber = @"SIE.3NA3807";
            var rows = ds.Tables["tblPart"].AsEnumerable();
            var QueryResult =
            from n in rows
            where n["partnr"].ToString().Trim() == DataPartNumber
            select n;
            if (QueryResult.Count() > 0)
            {
                foreach (DataRow rowForOperation in QueryResult)
                {

                    Dictionary<string, string> relationAndTablesNames = new Dictionary<string, string>();
                    relationAndTablesNames.Add("tblFreeProperty", "tblFreeProperty");
                    relationAndTablesNames.Add("tblFunctionTemplate", "tblFunctionTemplate");
                    relationAndTablesNames.Add("tblMechanic", "tblMechanic");
                    relationAndTablesNames.Add("tblVariant", "tblVariant");
                    foreach (string relationName in relationAndTablesNames.Keys)
                    {
                        if (ds.Relations.Contains(relationName))
                        {
                            foreach (DataRow row in rowForOperation.GetChildRows(ds.Relations[relationName]))
                            {
                                dbConnection.DeleteRow(row, relationAndTablesNames[relationName]);
                            }
                        }
                    }
                    dbConnection.DeleteRow(rowForOperation, "tblPart");
                }
            }
            Dictionary<string, string> replaces = new Dictionary<string, string>();
            replaces.Add(" class,", " [class],");
            replaces.Add(" create,", " [create],");
            replaces.Add(" note,", " [note],");
            replaces.Add(" usage,", " [usage],");

            replaces.Add(" color,", " [color],");
            replaces.Add(" connection,", " [connection],");
            replaces.Add(" variant,", " [variant],");
            replaces.Add(" thread,", " [thread],");


            dbConnection.UpdateDataset("tblPart");
            dbConnection.UpdateDataset("tblFreeProperty");
            dbConnection.UpdateDataset("tblFunctionTemplate");
            dbConnection.UpdateDataset("tblMechanic");
            dbConnection.UpdateDataset("tblVariant");

        }
        
        [TestMethod]
        public static void Test8()
        {
            string DataBasePath = @"C:\eplan.mdb";
            DBConnectionCL dbConnection = new DBConnectionCL();
            dbConnection.PrepareOleDbConnection(DataBasePath);

            string cmd = "SELECT * FROM tblPart";
            DbDataReader dr = dbConnection.ExecuteReaderCommand(cmd);
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            string filter = @"MANUFACTURER = 'Allen Bradley'";
            DataRow[] rows = dt.Select(filter);
            dbConnection.ScalarCommand(cmd);
        }

        [TestMethod]
        public static void Test9()
        {
            dbWrapper = WrapperFactory.GetWrapper();

            string error;
            if (!dbWrapper.DBConnection.OpenConnection(out error))
            {
                return;
            }
            string SQLscript = global::Tests.Properties.Resource1.DBCreation.ToString();
            ExecuteScript(SQLscript, null);

            string cmd = string.Format("SELECT count(*) FROM orders WHERE subscriber_id={0} AND activity<>{1}", 0, 0);
            int ret = (int)dbWrapper.ScalarMethod(cmd);



            dbWrapper.DBConnection.CloseConnection();
        }

        public static string GetDBScheme(string name, string[] restrictions)
        {
            return dbWrapper.GetDBScheme(name, restrictions);
        }
        public static string GetTableSheme(string name)
        {
            return dbWrapper.GetTableScheme(name);
        }
        public static string GetTableData(string name)
        {
            string cmd = string.Format("SELECT * FROM {0}", name);
            return dbWrapper.SelectTableString(cmd, name);
        }
        public static string GetFunctionText(string name)
        {
            return dbWrapper.GetFunctionText(name);
        }
        public static string[] GetNamesByType(string type, string owner)
        {
            return dbWrapper.GetNamesByType(type);
        }
        public static string GetAuthors()
        {
            string cmd = string.Format("SELECT id, surname, name, surname||' '||name INITIALS FROM author ORDER BY surname, name");
            string tableName = "author";
            return dbWrapper.SelectMethodToString(cmd, tableName, null);
        }
        public static int AddAuthor(string surname, string name)
        {
            string cmd = string.Format("INSERT INTO author (surname, name) values ('{0}', '{1}')"
                , surname, name);
            return dbWrapper.NonQueryMethod(cmd);
        }
        public static void ExecuteScript(string script, string procName)
        {
            dbWrapper.ScriptMethod(script);
        }
        public static void SetChanges(string changedDataset, string schemeDataset)
        {

            DataSet changed = new DataSet();

            if (!DBUtils.GetDatasetFromXML(changedDataset, schemeDataset, changed))
            {
                throw new SystemException("Restore dataset error");
            }

            SetAdded(changed, "author");
            SetAdded(changed, "book");
            SetAdded(changed, "subscriber");
            SetAdded(changed, "orders");

            dbWrapper.UpdateDataSet(changed);

        }

        public static void SetAdded(DataSet storage, string tableName)
        {
            DataTable dt = storage.Tables[tableName];
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            string cmd = string.Format("SELECT {0}_SEQ.NEXTVAL FROM DUAL", tableName.ToUpper());
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    decimal id = (decimal)dbWrapper.ScalarMethod(cmd); ;
                    row["ID"] = id;
                }
            }
        }
        public static void AddTable(string tableScheme, string tableData)
        {

            DataTable dataTable = new DataTable();
            DataTable schemeTable = new DataTable();

            if (!DBUtils.GetDataTableFromXML(tableScheme, tableData, ref schemeTable, ref dataTable))
            {
                throw new SystemException("Restore dataset error");
            }
            dbWrapper.CreateTable(schemeTable);
            dbWrapper.InsertTable(dataTable);
        }

    }
}
