using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Utils.WorkWithDB.Connections;

namespace Utils.WorkWithDB.Wrappers
{
    public class OleDBWrapper : DBWrapperBase, IDBWrapper
    {
        public OleDBWrapper()
        {
            DbType = DataBaseType.OleDb;
            Init();
        }

        public OleDBWrapper(string provider, string connString)
            :base(provider, connString)
        {
            DbType = DataBaseType.OleDb;
            Init();
        }

        public override string GetFunctionText(string name)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string[] GetNamesByType(string type)
        {
            OpenConnection();

            string sql;
            sql = "GRANT SELECT ON TABLE MSysObjects TO PUBLIC";
            NonQueryCommand(sql);

            if (string.IsNullOrEmpty(type))
            {
                sql = string.Format("select distinct type from MSysObjects");
            }
            else
            {
                sql = string.Format("select distinct name from MSysObjects where type = {0} order by name", type);
            }

            List<string> temp = new List<string>();
            DbDataReader dr = ExecuteReaderCommand(sql);


            while (dr.Read())
            {
                temp.Add(dr.GetValue(0).ToString());
            }

            CloseConnection();

            return temp.ToArray();
        }

        public override void CreateTable(DataTable schemeTable)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string RowLimitQuery
        {
            get { return "SELECT * FROM ({0}) WHERE ROWNUM < {1}"; }
        }
    }
}
