using System;
using System.Collections.Generic;
using System.Resources;
using System.Globalization;
using System.Reflection;
using System.Configuration;
using System.Threading;
using Utils.WorkWithDB;
using System.Data.SQLite;
using System.IO;

namespace Utils
{
    public class OptionsUtils
    {
        public const string Other = "OTHER";
        static OptionsUtils instance;
        private static ResourceManager rm;
        private static CultureInfo culture;

        public static OptionsUtils GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OptionsUtils();
                }
                return instance;
            }
        }

        public static ResourceManager ResManager
        {
            get
            {
                if (rm == null)
                {
                    rm = new ResourceManager(ConstantsUtils.RES, Assembly.GetExecutingAssembly());
                    SetCulture();
                }
                return rm;
            }
        }

        private static void SetCulture()
        {
            culture = Thread.CurrentThread.CurrentCulture;
        }

        public static CultureInfo Culture
        {
            get
            {
                if (culture == null)
                {
                    SetCulture();
                }
                return culture;
            }
            set
            {
                culture = value;
            }
        }

        private OptionsUtils()
        {
            rm = OptionsUtils.ResManager;
            culture = OptionsUtils.Culture;
        }

        private static string connectionName;
        public static string ConnectionName
        {
            get
            {
                if (connectionName == null)
                {
                    connectionName = GetConnectionName();
                }
                return connectionName;
            }
            set
            {
                connectionName = value;
            }
        }

        private static string provider = null;
        public static string Provider
        {
            get
            {
                if (provider == null)
                {
                    if (!string.IsNullOrEmpty(ConnectionName))
                    {
                        provider = ConfigurationManager.ConnectionStrings[connectionName].ProviderName;
                    }
                }
                return provider;
            }
            set { provider = value; }
        }

        private static string connectionString = null;
        public static string ConnectionString
        {
            get
            {
                if (connectionString == null)
                {
                    if (!string.IsNullOrEmpty(ConnectionName))
                    {
                        connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                        DbPath = connectionString;
                    }
                }

                return connectionString;

            }
            set { connectionString = value; }
        }

        private static string dbPath;
        public static string DbPath
        {
            get
            {
                return dbPath;
            }
            private set
            {
                SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder(value);

                FileInfo file = new FileInfo(builder.DataSource);
                dbPath = file.Exists ?
                        file.FullName :
                        Environment.CurrentDirectory + Path.DirectorySeparatorChar + builder.DataSource;
            }
        }

        public static void ClearDbData()
        {
            OptionsUtils.Provider = null;
            OptionsUtils.ConnectionName = null;
            OptionsUtils.ConnectionString = null;
        }

        private static string GetConnectionName()
        {
            Dictionary<string, ConnectionStringSettings> connectionStrings = new Dictionary<string, ConnectionStringSettings>();
            foreach (ConnectionStringSettings item in ConfigurationManager.ConnectionStrings)
            {
                if (!string.IsNullOrEmpty(item.ProviderName) && item.ElementInformation.IsPresent)
                {
                    connectionStrings.Add(item.Name, item);
                }
            }
            SelectConnection form = new SelectConnection(connectionStrings);
            System.Windows.Forms.DialogResult result = form.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(form.ConnectionName))
            {
                ConnectionName = form.ConnectionName;
            }
            else if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                throw new OperationCanceledException();
            }
            else
            {
                ConnectionName = string.Empty;
            }
            return ConnectionName;
        }
    }
}
