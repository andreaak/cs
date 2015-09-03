using System;
using System.Text;
using Utils.WorkWithDB.Connections;

namespace GrabDatabase
{
    public static class DBConstants
    {
        public enum DbCommand
        {
            select,
            insert,
            update,
            delete,
            drop,
            alter,
            create,
        }
        
        public const string SEPARATORE = "-->";
        public const string HELP_TABLE = "HELP";
        public const string DATABASE_TABLE = "DATABASE";
        public const string COMMAND_TABLE = "COMMAND";
        public const string SELECT_TEMPLATE = "SELECT {column1,column2} FROM {table_name}";
        public const string INSERT_TEMPLATE = "INSERT INTO {table_name} ({column1,column2}) VALUES ({value1, value2})";
        public const string UPDATE_TEMPLATE = "UPDATE {table_name} SET {column1=value1} , {column2=value2} WHERE {some_column=some_value}";
        public const string DELETE_TEMPLATE = "DELETE FROM {table_name} WHERE {some_column=some_value}";
        public const string ADD_COLUMN_TEMPLATE = "ALTER TABLE {table_name} ADD {column_name} {datatype}";
        public const string DROP_COLUMN_TEMPLATE = "ALTER TABLE {table_name} DROP COLUMN {column_name}";
        public const string MODIFY_COLUMN_TEMPLATE = @"ALTER TABLE {table_name} MODIFY COLUMN {column_name} {datatype}; (for Oracle);
ALTER TABLE {table_name} ALTER COLUMN {column_name} {datatype}; (for SQLServer)";
        public const string INNER_JOIN_TEMPLATE = @"SELECT {column_name(s)}
 FROM {table1}
 INNER JOIN {table2}
 ON {table1.column_name=table2.column_name}";
        public const string LEFT_RIGHT_JOIN_TEMPLATE = @"SELECT {column_name(s)}
 FROM {table1}
 LEFT/RIGHT JOIN {table2}
 ON {table1.column_name=table2.column_name}";

        public const string CREATE_TABLE_TEMPLATE = @"CREATE TABLE {table_name}(
{column_name1} {datatype} {PRIMARY KEY} {NOT NULL},
{column_name2} {datatype} {PRIMARY KEY} {NOT NULL},
{column_name3} {datatype} {PRIMARY KEY} {NOT NULL}
)";

        const string SQLiteCreationScript = @"
            CREATE TABLE HELP(
                ID INTEGER  PRIMARY KEY NOT NULL,
                DB_TYPE_ID INTEGER NOT NULL,
                COMMAND_ID INTEGER NOT NULL,                
                DATA TEXT
            );

            CREATE TABLE DATABASE(
                ID INTEGER  PRIMARY KEY NOT NULL,
                TYPE TEXT NOT NULL
            );
            CREATE TABLE COMMAND(
                ID INTEGER  PRIMARY KEY NOT NULL,
                TYPE TEXT NOT NULL
            );        
        ";

        public static string GetHelpScript(DataBaseType dbType)
        {
            string outStr = null;
            switch (dbType)
            {
                case DataBaseType.SQLite:
                    outStr = GetCreationScript(dbType) + GetDataScript();
                    break;

                default:
                    throw new Exception("Script not defined");
            }
            return outStr.Replace(Environment.NewLine, " ").Trim();
        }


        public static string GetCreationScript(DataBaseType dbType)
        {
            string outStr = null;
            switch (dbType)
            {
                case DataBaseType.SQLite:
                    outStr = SQLiteCreationScript;
                    break;

                default:
                    throw new Exception("Script not defined");
            }
            return outStr.Replace(Environment.NewLine, " ").Trim();
        }

        private static string GetDataScript()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int id = 1;
            foreach (string database in Enum.GetNames(typeof(DataBaseType)))
            {
                stringBuilder.Append(string.Format(" INSERT INTO DATABASE VALUES ({0}, '{1}');", id++, database));
            }

            id = 1;
            foreach (string cmd in Enum.GetNames(typeof(DbCommand)))
            {
                stringBuilder.Append(string.Format(" INSERT INTO COMMAND VALUES ({0}, '{1}');", id++, cmd));
            }
            return stringBuilder.ToString();
        }

        public static string GetCommandExistQuery()
        {
            return "SELECT ID, TYPE FROM COMMAND WHERE ID = -1";
        }

        public static string GetDatabaseExistQuery()
        {
            return "SELECT ID, TYPE FROM DATABASE WHERE ID = -1";
        }

        public static string GetHelpDataExistQuery()
        {
            return "SELECT ID, DB_TYPE_ID, COMMAND_ID, DATA FROM HELP WHERE ID = -1";
        }

    }
}
