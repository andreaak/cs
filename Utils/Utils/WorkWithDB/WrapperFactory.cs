using System;
using System.Collections.Generic;
using Utils.WorkWithDB.Wrappers;

namespace Utils.WorkWithDB
{
    public static class WrapperFactory
    {

        private static Dictionary<string, ADBWrapper> providers;

        public static Dictionary<string, ADBWrapper> CreatedProviders
        {
            get 
            {
                if (providers == null)
                {
                    providers = new Dictionary<string, ADBWrapper>();
                }
                return providers; 
            }
            
        }

        private static ADBWrapper CreateWrapper(string provider, string connString)
        {
            ADBWrapper wrapper = null;
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

        public static ADBWrapper GetWrapper()
        {
            string provider = OptionsUtils.Provider;
            string connString = OptionsUtils.ConnectionString;
            return GetWrapper(provider, connString);
        }

        public static ADBWrapper GetWrapper(string provider, string connString)
        {
            if (string.IsNullOrEmpty(provider) || string.IsNullOrEmpty(connString))
            {
                throw new Exception("Database wrapper initialization error");
            }
            string key = provider + connString;
            if (CreatedProviders.ContainsKey(key))
            {
                return providers[key];
            }
            return CreateWrapper(provider, connString);
        }
    }
}
