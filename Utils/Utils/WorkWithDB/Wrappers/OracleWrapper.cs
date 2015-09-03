using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Utils.WorkWithDB.Connections;

namespace Utils.WorkWithDB.Wrappers
{

    public class OracleWrapper : DBWrapperBase, IDBWrapper
    {
        static Dictionary<int, string> typeList = null;

        static OracleWrapper()
        {
            typeList = new Dictionary<int, string>();
            int count = 1;
            typeList.Add(count++, "BFILE");
            typeList.Add(count++, "BLOB");
            typeList.Add(count++, "CHAR");
            typeList.Add(count++, "CLOB");
            ++count;
            typeList.Add(count++, "DATE");
            typeList.Add(count++, "INTERVAL DAY TO SECOND");
            typeList.Add(count++, "INTERVAL YEAR TO MONTH");
            typeList.Add(count++, "LONG RAW");
            typeList.Add(count++, "LONG");
            typeList.Add(count++, "NCHAR");
            typeList.Add(count++, "NCLOB");
            typeList.Add(count++, "NUMBER");
            typeList.Add(count++, "NVARCHAR2");
            typeList.Add(count++, "RAW");
            typeList.Add(count++, "ROWID");
            ++count;
            typeList.Add(count++, "TIMESTAMP");
            typeList.Add(count++, "TIMESTAMP WITH LOCAL TIME ZONE");
            typeList.Add(count++, "TIMESTAMP WITH TIME ZONE");
            ++count;
            typeList.Add(count++, "VARCHAR2");
            typeList.Add(29, "FLOAT");
        }        
        
        public OracleWrapper()
        {
            DbType = DataBaseType.Oracle;
            Init();
        }

        public OracleWrapper(string provider, string connString)
            :base(provider, connString)
        {
            DbType = DataBaseType.Oracle;
            Init();
        }

        protected override void Init()
        {
            metaDataObjectKeys = new Dictionary<DBObjects, IDictionary<string, string>>();
            IDictionary<string, string> metaDataKeys = new Dictionary<string, string>();
            metaDataKeys.Add("TABLE_NAME", "TABLE_NAME");
            metaDataObjectKeys.Add(DBObjects.Tables, metaDataKeys);

            metaDataKeys = new Dictionary<string, string>();
            metaDataKeys.Add("COLUMN_NAME", "COLUMN_NAME");
            metaDataKeys.Add("ID", "ID");
            metaDataKeys.Add("DATA_TYPE", "DATATYPE");
            metaDataKeys.Add("LENGTH", "LENGTH");
            metaDataKeys.Add("NUMERIC_PRECISION", "PRECISION");
            metaDataKeys.Add("NUMERIC_SCALE", "SCALE");
            metaDataKeys.Add("IS_NULLABLE", "NULLABLE");
            metaDataKeys.Add("OWNER", "OWNER");
            metaDataObjectKeys.Add(DBObjects.Columns, metaDataKeys);

            metaDataKeys = new Dictionary<string, string>();
            metaDataKeys.Add("VIEW_NAME", "VIEW_NAME");
            metaDataKeys.Add("VIEW_DEFINITION", "TEXT");
            metaDataObjectKeys.Add(DBObjects.Views, metaDataKeys);

        }

        public override void CreateTable(DataTable schemeTable)
        {
            if (schemeTable == null)
            {
                return;
            }

            OpenConnection();
            string sql = string.Format("DROP TABLE {0}", schemeTable.TableName);
            try
            {
                NonQueryCommand(sql);
            }
            catch (Exception)
            {

            }
            sql = string.Format("CREATE TABLE {0} (", schemeTable.TableName);
            foreach (DataRow row in schemeTable.Rows)
            {
                string name = row["ColumnName"].ToString();

                int typeIndex = int.Parse(row["ProviderType"].ToString());

                string type;
                if (!GetOracleType(typeIndex, row, out type))
                {
                    throw new Exception("Wrong provider type index");
                }

                sql += string.Format("\"{0}\" {1} ", name, type);

                if (row["AllowDBNull"].ToString().ToLower() == "false")
                {
                    sql += string.Format("NOT NULL ENABLE ");
                }
                sql += ", ";
            }
            sql = sql.Trim().TrimEnd(',');
            sql += ")";

            try
            {
                NonQueryCommand(sql);
            }
            finally
            {
                CloseConnection();
            }
        }

        private bool GetOracleType(int typeIndex, DataRow row, out string type)
        {
            type = null;
            if (!typeList.ContainsKey(typeIndex))
            {
                return false;
            }

            type = typeList[typeIndex];
            switch (type)
            {
                case "NVARCHAR2":
                case "VARCHAR2":
                case "RAW":
                    type += string.Format("({0})", row["ColumnSize"]);
                    break;
                case "NUMBER":
                    if (int.Parse(row["NumericPrecision"].ToString()) > 38 || row["NumericScale"].ToString() == "129")
                    {
                        break;
                    }
                    type += string.Format("({0},{1})", row["NumericPrecision"], row["NumericScale"]);

                    break;
                case "BLOB":
                case "CLOB":
                case "CHAR":
                case "DATE":
                case "TIMESTAMP":
                    break;
                default:
                    break;
            }
            return true;
        }

        public override string GetTime(DateTime dateTime)
        {
            string ind = "AM";
            int hour = dateTime.Hour;
            if (hour > 12)
            {
                ind = "PM";
                hour -= 12;
            }
            return string.Format("{0}:{1}:{2} {3}", hour, dateTime.Minute, dateTime.Second, ind);
        }

        public override string GetDate(DateTime dateTime)
        {
            return string.Format("{0}-{1}-{2}", dateTime.Day, ((OracleMonth)dateTime.Month).ToString().ToUpper(), dateTime.Year);
        }

        enum OracleMonth
        {
            jan = 1,
            feb,
            mar,
            apr,
            may,
            jun,
            jul,
            aug,
            sep,
            oct,
            nov,
            dec
        }

        public override string[] GetNamesByType(string type)
        {
            OpenConnection();

            string sql;
            if (string.IsNullOrEmpty(type))
            {
                sql = string.Format("select distinct type from USER_SOURCE");
            }
            else
            {
                sql = string.Format("select distinct name from USER_SOURCE where type = '{0}' order by name", type);
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

        public override string GetFunctionText(string name)
        {
            OpenConnection();

            string sql = string.Format("select text from USER_SOURCE where name = '{0}' order by line", name);

            DbDataReader dr = ExecuteReaderCommand(sql);
            string temp = string.Empty;
            while (dr.Read())
            {
                temp += dr.GetValue(0).ToString();
            }

            CloseConnection();

            return temp;
        }

        public override string RowLimitQuery
        {
            get { return "SELECT * FROM ({0}) WHERE ROWNUM < {1}"; }
        }

        public override string GetMetaDataObject(DBObjects dbObj)
        {
            switch (dbObj)
            {
                case DBObjects.Tables:
                case DBObjects.Columns:
                case DBObjects.Views:
                    return dbObj.ToString();
                case DBObjects.Triggers:
                default:
                    return null;
            }
        }
    }

}
