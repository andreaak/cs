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
using Utils.WorkWithDB.Helpers;

namespace Utils
{
    public class OptionsUtils
    {
        public const string Other = "OTHER";
        public const string New = "NEW";
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

        private static string previousConnectionName;
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

        public static string Provider
        {
            get
            {
                return DBHelper.Provider;
            }
            set
            {
                DBHelper.Provider = value;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return DBHelper.ConnectionString;
            }
            set
            {
                DBHelper.ConnectionString = value;
            }
        }

        private static IDBHelper dbhelper;
        public static IDBHelper DBHelper => dbhelper ?? (dbhelper = DBHelperFactory.GetHelper(ConnectionName));

        public static string GetConnectionString(string dataSource)
        {
            var builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = dataSource;
            return builder.ConnectionString;
        }

        public static string DbPath => DBHelper.DbPath;

        public static void ClearDbData()
        {
            previousConnectionName = connectionName;
            ConnectionName = null;
            dbhelper = null;
        }

        public static void RestoreDbData()
        {
            ConnectionName = previousConnectionName;
        }

        public static string GetConnectionName()
        {
            string name;

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
                name = form.ConnectionName;
            }
            else if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                throw new OperationCanceledException();
            }
            else
            {
                name = string.Empty;
            }
            return name;
        }
    }
}
