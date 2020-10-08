using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace Utils.WorkWithDB.Helpers
{
    public interface IDBHelper
    {
        string Provider { get; set; }
        string ConnectionString { get; set; }
        string DbPath { get; }
        bool Initialize();
    }

    public class DBHelperFactory
    {
        public static IDBHelper GetHelper(string connectionName)
        {
            string providerName = ConfigurationManager.ConnectionStrings[connectionName].ProviderName;

            switch (providerName)
            {
                case SqlLiteHelper.ProviderName:
                    return new SqlLiteHelper(connectionName);
                default:
                    throw new NotSupportedException();
            }
        }
    }

    public class SqlLiteHelper : IDBHelper
    {
        public const string ProviderName = "System.Data.SQLite";
        private SQLiteConnectionStringBuilder builder;
        private string provider;
        private bool isInitialized;
        private string connectionName;

        public SqlLiteHelper(string connectionName)
        {
            this.connectionName = connectionName;
        }

        public string Provider
        {
            get
            {
                if (provider == null)
                {
                    provider = ConfigurationManager.ConnectionStrings[connectionName].ProviderName;
                }
                return provider;
            }
            set { provider = value; }
        }

        public string ConnectionString
        {
            get
            {
                return Builder.ConnectionString;
            }
            set
            {
                Builder = string.IsNullOrEmpty(value) ? null : new SQLiteConnectionStringBuilder(value);
            }
        }

        private SQLiteConnectionStringBuilder Builder
        {
            get
            {
                if (builder == null)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                    builder = new SQLiteConnectionStringBuilder(connectionString);
                }
                return builder;
            }
            set
            {
                builder = value;
            }
        }

        public string DbPath
        {
            get
            {
                FileInfo file = new FileInfo(Builder.DataSource);
                return file.Exists ?
                        file.FullName :
                        Environment.CurrentDirectory + Path.DirectorySeparatorChar + Builder.DataSource;
            }
        }

        public bool Initialize()
        {
            if(isInitialized)
            {
                return true;
            }
            string[] extensions = new string[]
                {
                "SQLite Files (*.db)|*.db|",
                "All Files (*.*)|*.*"
                };
            string filePath = null;
            string title = "Select database file";
            if (!SelectFile.OpenFile(title, string.Empty, ref filePath, extensions))
            {
                return false;
            }
            builder = new SQLiteConnectionStringBuilder(ConnectionString.Replace("{filePath}", filePath));
            OptionsUtils.Provider = Provider;
            OptionsUtils.ConnectionString = ConnectionString;

            isInitialized = true;
            return true;
        }
    }
}
