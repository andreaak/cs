using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Resources;
using System.Globalization;
using System.Reflection;
using System.Configuration;
using System.Threading;
using System.IO;
using Utils.WorkWithDB;

namespace Utils
{
    class CancelException : Exception
    {
    }

    public class OptionsUtils
    {
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

        public static string ConnectionName
        {
            get;
            set;
        }

        private static string provider = null;
        public static string Provider
        {
            get
            {
                if (provider == null)
                {
                    string connectionName = string.IsNullOrEmpty(ConnectionName) ? GetConnectionName() : ConnectionName;
                    if (!string.IsNullOrEmpty(connectionName))
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
                    string connectionName = ConnectionName == null ? GetConnectionName() : ConnectionName;
                    if (!string.IsNullOrEmpty(connectionName))
                    {
                        connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                    }
                }
                return connectionString;
            }
            set { connectionString = value; }
        }


        public static void ClearDbData()
        {
            OptionsUtils.Provider = null;
            OptionsUtils.ConnectionName = null;
            OptionsUtils.ConnectionString = null;
        }
    }
}
