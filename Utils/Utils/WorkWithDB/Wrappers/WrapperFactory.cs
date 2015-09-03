using System;
using System.Collections.Generic;
using Utils.WorkWithDB.Wrappers;

namespace Utils.WorkWithDB.Wrappers
{
    public static class WrapperFactory
    {
        private static Dictionary<string, IDBWrapper> providers;
        public static Dictionary<string, IDBWrapper> Providers
        {
            get 
            {
                if (providers == null)
                {
                    providers = new Dictionary<string, IDBWrapper>();
                }
                return providers; 
            }
        }

        private static IDBWrapper CreateWrapper(string provider, string connString)
        {
            IDBWrapper wrapper = null;
            switch (provider)
            {
                case "System.Data.OracleClient":
                    wrapper = new OracleWrapper(provider, connString);
                    providers.Add(provider + connString, wrapper);
                    break;
                case "System.Data.SqlClient":
                    wrapper = new SqlServerWrapper(provider, connString);
                    providers.Add(provider + connString, wrapper);
                    break;
                case "System.Data.OleDb":
                    wrapper = new OleDBWrapper(provider, connString);
                    providers.Add(provider + connString, wrapper);
                    break;
                case "System.Data.SQLite":
                    wrapper = new SQLiteDBWrapper(provider, connString);
                    providers.Add(provider + connString, wrapper);
                    break;
                default:
                    break;
            }
            return wrapper;
        }

        public static IDBWrapper GetWrapper()
        {
            string provider = OptionsUtils.Provider;
            string connString = OptionsUtils.ConnectionString;
            return GetWrapper(provider, connString);
        }

        public static IDBWrapper GetWrapper(string provider, string connString)
        {
            if (string.IsNullOrEmpty(provider) || string.IsNullOrEmpty(connString))
            {
                throw new Exception("Database wrapper initialization error");
            }
            string key = provider + connString;
            if (Providers.ContainsKey(key))
            {
                return providers[key];
            }
            return CreateWrapper(provider, connString);
        }
    }
}
