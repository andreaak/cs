using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using NUnit.Framework;

namespace CSTest._21_Database._01_ADO._01_ConnectedLayer
{
    [TestFixture]
    public class Test
    {
        string providerName = null;//System.Configuration.ConfigurationManager.ConnectionStrings["CSTest.Properties.Settings.ShopDBConnectionString"].ProviderName;
        string connectionString = null; //System.Configuration.ConfigurationManager.ConnectionStrings["CSTest.Properties.Settings.ShopDBConnectionString"].ConnectionString;

        [Test]
        public void TestDataReader()
        {
            DbProviderFactory df = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = df.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT CUST_NUM, COMPANY, CREDIT_LIMIT FROM CUSTOMERS";
            try
            {
                connection.Open();
                DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    Debug.WriteLine("Id: {0} Company: {1} Limit:{2}", reader.GetValue(reader.GetOrdinal("CUST_NUM")), reader["COMPANY"], reader.GetValue(2));
                }
                reader.Close();
            }
            finally
            {
                connection.Close();
            }
            /*
            Id: 2101 Company: Jones Mfg. Limit:65000.0000
            Id: 2102 Company: First Corp. Limit:65000.0000
            Id: 2103 Company: Acme Mfg. Limit:50000.0000
            Id: 2105 Company: AAA Investments Limit:45000.0000
            Id: 2106 Company: Fred Lewis Corp. Limit:65000.0000
            Id: 2107 Company: Ace International Limit:35000.0000
            Id: 2108 Company: Holm & Landis Limit:55000.0000
            Id: 2109 Company: Chen Associates Limit:25000.0000
            Id: 2111 Company: JCP Inc. Limit:50000.0000
            Id: 2112 Company: Zetacorp Limit:50000.0000
            Id: 2113 Company: Ian & Schmidt Limit:20000.0000
            Id: 2114 Company: Orion Corp. Limit:20000.0000
            Id: 2115 Company: Smithson Corp. Limit:20000.0000
            Id: 2117 Company: J.P. Sinclair Limit:35000.0000
            Id: 2118 Company: Midwest Systems Limit:60000.0000
            Id: 2119 Company: Solomon Inc. Limit:25000.0000
            Id: 2120 Company: Rico Enterprises Limit:50000.0000
            Id: 2121 Company: QMA Assoc. Limit:45000.0000
            Id: 2122 Company: Three Way Lines Limit:30000.0000
            Id: 2123 Company: Carter & Sons Limit:40000.0000
            Id: 2124 Company: Peter Brothers Limit:40000.0000
            */
        }

        [Test]
        public void TestDataReaderBatch()
        {
            DbProviderFactory df = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = df.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT CUST_NUM, COMPANY, CREDIT_LIMIT FROM CUSTOMERS;" +
                "SELECT EMPL_NUM, NAME, TITLE FROM SALESREPS;";
            try
            {
                connection.Open();
                DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                do
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine("Id: {0} Description: {1} Other: {2}", reader.GetValue(0), reader.GetValue(1), reader.GetValue(2));
                    }
                } while (reader.NextResult());

                reader.Close();
            }
            finally
            {
                connection.Close();
            }
            /*
            Id: 2101 Description: Jones Mfg. Other: 65000.0000
            Id: 2102 Description: First Corp. Other: 65000.0000
            Id: 2103 Description: Acme Mfg. Other: 50000.0000
            Id: 2105 Description: AAA Investments Other: 45000.0000
            Id: 2106 Description: Fred Lewis Corp. Other: 65000.0000
            Id: 2107 Description: Ace International Other: 35000.0000
            Id: 2108 Description: Holm & Landis Other: 55000.0000
            Id: 2109 Description: Chen Associates Other: 25000.0000
            Id: 2111 Description: JCP Inc. Other: 50000.0000
            Id: 2112 Description: Zetacorp Other: 50000.0000
            Id: 2113 Description: Ian & Schmidt Other: 20000.0000
            Id: 2114 Description: Orion Corp. Other: 20000.0000
            Id: 2115 Description: Smithson Corp. Other: 20000.0000
            Id: 2117 Description: J.P. Sinclair Other: 35000.0000
            Id: 2118 Description: Midwest Systems Other: 60000.0000
            Id: 2119 Description: Solomon Inc. Other: 25000.0000
            Id: 2120 Description: Rico Enterprises Other: 50000.0000
            Id: 2121 Description: QMA Assoc. Other: 45000.0000
            Id: 2122 Description: Three Way Lines Other: 30000.0000
            Id: 2123 Description: Carter & Sons Other: 40000.0000
            Id: 2124 Description: Peter Brothers Other: 40000.0000
            Id: 101 Description: Dan Roberts Other: Sales Rep
            Id: 102 Description: Sue Smith Other: Sales Rep
            Id: 103 Description: Paul Cruz Other: Sales Rep
            Id: 104 Description: Bob Smith Other: Sales Mgr
            Id: 105 Description: Bill Adams Other: Sales Rep
            Id: 106 Description: Sam Clark Other: VP Sales
            Id: 107 Description: Nancy Angelli Other: Sales Rep
            Id: 108 Description: Larry Fitch Other: Sales Mgr
            Id: 109 Description: Mary Jones Other: Sales Rep
            Id: 110 Description: Tom Snyder Other: Sales Rep
            */
        }

        [Test]
        public void TestDataReaderWithParam()
        {
            DbProviderFactory df = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = df.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT CUST_NUM, COMPANY, CREDIT_LIMIT  FROM CUSTOMERS WHERE CUST_NUM=@Id";

            DbParameter param = cmd.CreateParameter();
            param.ParameterName = "@Id";
            param.Direction = ParameterDirection.Input;
            param.Value = 2101;
            cmd.Parameters.Add(param);

            try
            {
                connection.Open();
                DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    Debug.WriteLine("Id: {0} Company: {1} Limit:{2}", reader.GetValue(reader.GetOrdinal("CUST_NUM")), reader.GetValue(1), reader.GetValue(2));
                }
                reader.Close();
            }
            finally
            {
                connection.Close();
            }
            /*
            Id: 2101 Company: Jones Mfg. Limit:65000.0000
            */
        }

        /*
        Выполнение запроса, не возвращающего записей. Запрос, не возвращающий набор результатов, 
        обычно называется командным (action query). Основных видов командных запросов два:
        - DML-запросы (Data Manipulation Language, язык управления данными). 
            Также называются обновлениями под управлением запросов. Они изменяют содержимое БД:
            UPDATE Customers  SET  CompanyName  =  'NewConpanyNane' WHERE CustomerlD  =  'ALFKI'
            INSERT  INTO  Customers  (CustomerlD,  CompanyName) VALUES('NewID',  'NewCustomer')
            DELETE  FROM  Customers  WHERE  CustomerlD  =  'ALFKI'
        
        - DDL-запросы (Data Definition Language, язык определения данных). Эти запросы изменяют структуру БД:
            CREATE TABLE  Table1(
            Field1  int  NOT  NULL CONSTRAINT  PK_Table1  PRIMARY KEY,
            Field2  varchar(32))
            ALTER VIEW View1 
            AS  SELECT  Field1,  Field2  FROM Table1
            DROP  PROCEDURE  StoredProcedura1
        */
        [Test]
        public void TestExecuteNonQuery()
        {
            DbProviderFactory df = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = df.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"CREATE TABLE  Table1(
                            Field1  int  NOT  NULL CONSTRAINT  PK_Table1  PRIMARY KEY,
                            Field2  varchar(32))";
            try
            {
                connection.Open();
                int result = cmd.ExecuteNonQuery();
                Debug.WriteLine("Table created");

                cmd.CommandText = "INSERT INTO Table1 VALUES(0, 'Test')";
                result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    Debug.WriteLine("Row inserted");
                }
                else
                {
                    Debug.WriteLine("Row not inserted");
                }

                cmd.CommandText = "DROP TABLE  Table1";
                result = cmd.ExecuteNonQuery();
                Debug.WriteLine("Table droped");
            }
            finally
            {
                connection.Close();
            }
        }

        //Выполнение запроса, возвращающего одно значение Предположим, вы хотите выполнить запрос 
        //и получить одну ячейку (одну запись, одно поле) данных.
        [Test]
        public void TestExecuteScalar()
        {
            DbProviderFactory df = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = df.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT COUNT(*) FROM CUSTOMERS";
            try
            {
                connection.Open();
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                Debug.WriteLine("Result: " + result);
            }
            finally
            {
                connection.Close();
            }

            /*
            21
            */
        }

        [Test]
        public void TestProcedureWithParam()
        {
            DbProviderFactory df = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = df.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "spCustomerByName";
            cmd.CommandType = CommandType.StoredProcedure;

            DbParameter param = cmd.CreateParameter();
            param.ParameterName = "@Company";
            param.Direction = ParameterDirection.Input;
            param.Value = "Jones Mfg.";
            cmd.Parameters.Add(param);

            try
            {
                connection.Open();
                DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    Debug.WriteLine("Id: {0} Company: {1} Rep: {2} Limit:{3}", reader.GetValue(reader.GetOrdinal("CUST_NUM")),
                                    reader.GetValue(1), reader.GetValue(2), reader.GetValue(3));
                }
                reader.Close();
            }
            finally
            {
                connection.Close();
            }
            /*
            Id: 2101 Company: Jones Mfg. Rep: 106 Limit:65000.0000
            */
        }

        [Test]
        public void TestProcedureWithParam2()
        {
            DbProviderFactory df = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = df.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "spCustomerByName2";
            cmd.CommandType = CommandType.StoredProcedure;

            DbParameter param1 = cmd.CreateParameter();
            param1.ParameterName = "@RetVal";
            param1.Direction = ParameterDirection.ReturnValue;
            param1.DbType = DbType.Int32;
            cmd.Parameters.Add(param1);

            DbParameter param = cmd.CreateParameter();
            param.ParameterName = "@Company";
            param.Direction = ParameterDirection.Input;
            param.Value = "Jones Mfg.";
            cmd.Parameters.Add(param);

            try
            {
                connection.Open();
                DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    Debug.WriteLine("Id: {0} Company: {1} Rep: {2} Limit:{3}", reader.GetValue(reader.GetOrdinal("CUST_NUM")),
                                    reader.GetValue(1), reader.GetValue(2), reader.GetValue(3));
                }
                Debug.WriteLine("@RetVal: " + param1.Value);
                reader.Close();
            }
            finally
            {
                connection.Close();
            }
            /*
            Id: 2101 Company: Jones Mfg. Rep: 106 Limit:65000.0000
            @RetVal:
            */
        }

        [Test]
        public void TestProcedureWithOutParam()
        {
            DbProviderFactory df = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = df.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "spCustomerById";
            cmd.CommandType = CommandType.StoredProcedure;

            DbParameter param1 = cmd.CreateParameter();
            param1.ParameterName = "@RetVal";
            param1.Direction = ParameterDirection.ReturnValue;
            param1.DbType = DbType.Int32;
            cmd.Parameters.Add(param1);

            DbParameter param2 = cmd.CreateParameter();
            param2.ParameterName = "@Id";
            param2.Direction = ParameterDirection.Input;
            param2.Value = 2102;
            cmd.Parameters.Add(param2);

            DbParameter param3 = cmd.CreateParameter();
            param3.ParameterName = "@Company";
            param3.DbType = DbType.String;
            param3.Size = 20;
            param3.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param3);

            DbParameter param4 = cmd.CreateParameter();
            param4.ParameterName = "@Limit";
            param4.DbType = DbType.Decimal;
            param4.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param4);


            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();

                Debug.WriteLine("@RetVal: " + param1.Value);
                Debug.WriteLine("@Company: " + param3.Value);
                Debug.WriteLine("@Limit: " + param4.Value);
            }
            finally
            {
                connection.Close();
            }
            /*
            @RetVal: 777
            @Company: First Corp.
            @Limit: 65000
            */
        }

        [Test]
        public void TestProcedureWithOutParam2()
        {
            DbProviderFactory df = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = df.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "spCustomerById";
            cmd.CommandType = CommandType.StoredProcedure;

            DbParameter param2 = cmd.CreateParameter();
            param2.ParameterName = "@Id";
            param2.Direction = ParameterDirection.Input;
            param2.Value = 2102;
            cmd.Parameters.Add(param2);

            DbParameter param3 = cmd.CreateParameter();
            param3.ParameterName = "@Company";
            param3.DbType = DbType.String;
            param3.Size = 20;
            param3.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param3);

            DbParameter param4 = cmd.CreateParameter();
            param4.ParameterName = "@Limit";
            param4.DbType = DbType.Decimal;
            param4.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param4);


            try
            {
                connection.Open();
                object result = cmd.ExecuteScalar();

                Debug.WriteLine("@RetVal: " + result);
                Debug.WriteLine("@Company: " + param3.Value);
                Debug.WriteLine("@Limit: " + param4.Value);
            }
            finally
            {
                connection.Close();
            }
            /*
            @RetVal:
            @Company: First Corp.
            @Limit: 65000
            */
        }

        [Test]
        public void TestFunction()
        {
            DbProviderFactory df = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = df.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "sfCustomerCompanyById";
            cmd.CommandType = CommandType.StoredProcedure;

            DbParameter param2 = cmd.CreateParameter();
            param2.ParameterName = "@Id";
            param2.Direction = ParameterDirection.Input;
            param2.Value = 2102;
            cmd.Parameters.Add(param2);

            DbParameter param1 = cmd.CreateParameter();
            param1.ParameterName = "@RetVal";
            param1.Direction = ParameterDirection.ReturnValue;
            param1.DbType = DbType.Int32;
            cmd.Parameters.Add(param1);

            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();

                Debug.WriteLine("@RetVal: " + param1.Value);
            }
            finally
            {
                connection.Close();
            }
            /*
            @RetVal: First Corp.
            */
        }

        [Test]
        public void TestFunction2()
        {
            DbProviderFactory df = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = df.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM sfCustomerInfoById(@Id);";

            DbParameter param2 = cmd.CreateParameter();
            param2.ParameterName = "@Id";
            param2.Direction = ParameterDirection.Input;
            param2.Value = 2102;
            cmd.Parameters.Add(param2);

            try
            {
                connection.Open();
                DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    Debug.WriteLine("Id: {0} Company: {1} Rep: {2} Limit:{3}", reader.GetValue(reader.GetOrdinal("CUST_NUM")),
                                    reader.GetValue(1), reader.GetValue(2), reader.GetValue(3));
                }
                reader.Close();
            }
            finally
            {
                connection.Close();
            }
            /*
            Id: 2102 Company: First Corp. Rep: 101 Limit:65000.0000
            */
        }

        [Test]
        public void TestTransaction1()
        {
            /*
            Вы хотите использовать транзакции, чтобы несколько операций обраба­тывались  как  атомарная  единица,  
            то  есть  либо  все  они  выполняются  успешно, либо все заканчиваются неудачей.
            Решение. Вы можете создать транзакцию на основе объекта-соединения. 
            После этого нужно включать ее в каждую выполняемую команду.
            */

            DbProviderFactory df = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = df.CreateConnection();
            connection.ConnectionString = connectionString;

            using (connection)
            {
                connection.Open();
                using (DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM CUSTOMERS WHERE CUST_NUM = 3000";
                    cmd.ExecuteNonQuery();
                }

                Debug.WriteLine("Before attempted inserts:  ");
                using (DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT CUST_NUM, COMPANY, CREDIT_LIMIT FROM CUSTOMERS";

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.WriteLine("Id: {0} Company: {1} Limit:{2}", reader.GetValue(reader.GetOrdinal("CUST_NUM")), reader["COMPANY"], reader.GetValue(2));
                        }
                    }
                }

                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (DbCommand cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "INSERT INTO CUSTOMERS (CUST_NUM, COMPANY, CREDIT_LIMIT) VALUES (3000, 'BOND', 0)";
                            cmd.ExecuteNonQuery();  //  Это должно работать
                        }
                        //  Если программа дошла до этого места,  значит,
                        //  все в порядке,  можно продолжить  и  выполнить  транзакцию
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("Exception occurred,  rolling back");
                        transaction.Rollback();
                    }
                }

                Debug.WriteLine("After attempted inserts");
                using (DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT CUST_NUM, COMPANY, CREDIT_LIMIT FROM CUSTOMERS";

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.WriteLine("Id: {0} Company: {1} Limit:{2}", reader.GetValue(reader.GetOrdinal("CUST_NUM")), reader["COMPANY"], reader.GetValue(2));
                        }
                    }
                }
            }

            /*
            Before attempted inserts:  
            Id: 2101 Company: Jones Mfg. Limit:65000.0000
            ...
            Id: 2124 Company: Peter Brothers Limit:40000.0000
            After attempted inserts
            Id: 2101 Company: Jones Mfg. Limit:65000.0000
            ...
            Id: 2124 Company: Peter Brothers Limit:40000.0000
            Id: 3000 Company: BOND Limit:0.0000
            */
        }

        [Test]
        public void TestTransaction2()
        {
            DbProviderFactory df = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = df.CreateConnection();
            connection.ConnectionString = connectionString;

            using (connection)
            {
                connection.Open();
                using (DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM CUSTOMERS WHERE CUST_NUM = 3000";
                    cmd.ExecuteNonQuery();
                }

                Debug.WriteLine("Before attempted inserts:  ");
                using (DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT CUST_NUM, COMPANY, CREDIT_LIMIT FROM CUSTOMERS";

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.WriteLine("Id: {0} Company: {1} Limit:{2}", reader.GetValue(reader.GetOrdinal("CUST_NUM")), reader["COMPANY"], reader.GetValue(2));
                        }
                    }
                }

                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (DbCommand cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "INSERT INTO CUSTOMERS (CUST_NUM, COMPANY, CREDIT_LIMIT) VALUES (3001, 'BOND', 'kkk')";
                            cmd.ExecuteNonQuery();  //  Это HE должно работать
                        }
                        //  Если программа дошла до этого места,  значит,
                        //  все в порядке,  можно продолжить  и  выполнить  транзакцию
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("Exception occurred, rolling back");
                        transaction.Rollback();
                    }
                }

                Debug.WriteLine("After attempted inserts");
                using (DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT CUST_NUM, COMPANY, CREDIT_LIMIT FROM CUSTOMERS";

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.WriteLine("Id: {0} Company: {1} Limit:{2}", reader.GetValue(reader.GetOrdinal("CUST_NUM")), reader["COMPANY"], reader.GetValue(2));
                        }
                    }
                }
            }

            /*
            Before attempted inserts:  
            Id: 2101 Company: Jones Mfg. Limit:65000.0000
            ...
            Id: 2124 Company: Peter Brothers Limit:40000.0000
            A first chance exception of type 'System.Data.SqlClient.SqlException' occurred in System.Data.dll
            Exception occurred, rolling back
            After attempted inserts
            Id: 2101 Company: Jones Mfg. Limit:65000.0000
            ...
            Id: 2124 Company: Peter Brothers Limit:40000.0000           
            */
        }
    }
}
