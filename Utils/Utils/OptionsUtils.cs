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
        public const string ProviderName = "provider";
        public const string ConnectionStringName = "conString";
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

        private static SQLiteConnectionStringBuilder builder;
        public static string ConnectionString
        {
            get
            {
                if (builder == null)
                {
                    if (!string.IsNullOrEmpty(ConnectionName))
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                        builder = new SQLiteConnectionStringBuilder(connectionString);
                    }
                }
                return builder?.ConnectionString;
            }
            set
            {
                builder = string.IsNullOrEmpty(value) ? null : new SQLiteConnectionStringBuilder(value);
            }
        }

        public static string GetConnectionString(string dataSource)
        {
            var builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = dataSource;
            return builder.ConnectionString;
        }

        public static string DbPath
        {
            get
            {
                FileInfo file = new FileInfo(builder.DataSource);
                return file.Exists ?
                        file.FullName :
                        Environment.CurrentDirectory + Path.DirectorySeparatorChar + builder.DataSource;
            }
        }

        public static void ClearDbData()
        {
            OptionsUtils.Provider = null;
            OptionsUtils.ConnectionName = null;
            builder = null;
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
