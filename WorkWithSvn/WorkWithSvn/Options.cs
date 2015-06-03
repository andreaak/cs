using System;
using System.Collections.Generic;
using System.Resources;
using System.Globalization;
using System.Reflection;
using System.Configuration;
using System.Threading;
using System.IO;
using WorkWithSvn.RepositoryProviders;

namespace WorkWithSvn
{
    public class Options
    {
        static Options instance;
        private static ResourceManager rm;
        private static CultureInfo culture;
        private static AProvider provider;

        public static Options GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Options();
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
                    rm = new ResourceManager(Constants.RES, Assembly.GetExecutingAssembly());
                    SetCulture();
                }
                return rm;
            }
        }

        private static void SetCulture()
        {
            string cultureStr = ConfigurationManager.AppSettings[Constants.CULTURE];
            if (string.IsNullOrEmpty(cultureStr))
            {
                culture = Thread.CurrentThread.CurrentCulture;
            }
            else
            {
                culture = CultureInfo.GetCultureInfo(cultureStr);
            }
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
        }

        public static AProvider Provider
        {
            get
            {
                if (provider == null)
                {
                    string prov = ConfigurationManager.AppSettings[Constants.PROVIDER];
                    switch (prov)
                    { 
                        case "SVN":
                            provider = new SvnProvider();
                            break;
                        default:
                            provider = new SvnProvider();
                            break;
                    }
                }
                return provider;
            }
        }

        private string workingCopyPath;
        public string WorkingCopyPath
        {
            get { return workingCopyPath; }
            set { workingCopyPath = value; }
        }

        private string switchDir;
        public string SwitchDir
        {
            get { return switchDir; }
        }

        private string tempDir;
        
        private string diffDir;
        public string DiffDir
        {
            get { return diffDir; }
        }

        List<string> ignoredFilesExtension;

        public List<string> IgnoredFilesExtension
        {
            get { return ignoredFilesExtension; }
        }

        List<string> ignoredDirectories;
        public List<string> IgnoredDirectories
        {
            get { return ignoredDirectories; }
        }

        bool isDisplayDir;
        public bool IsDisplayDir
        {
            get { return isDisplayDir; }
        }

        private Options()
        {
            workingCopyPath = ConfigurationManager.AppSettings[Constants.INPUT_DIR];

            string temp = ConfigurationManager.AppSettings[Constants.IGNORED_FILES];
            ignoredFilesExtension = new List<string>();
            ignoredFilesExtension.AddRange(temp.Trim().Split(new char[] { Constants.SEPARATOR }, StringSplitOptions.RemoveEmptyEntries));

            temp = ConfigurationManager.AppSettings[Constants.IGNORED_DIRS];
            ignoredDirectories = new List<string>();
            ignoredDirectories.AddRange(temp.Trim().Split(new char[] { Constants.SEPARATOR }, StringSplitOptions.RemoveEmptyEntries));

            tempDir = ConfigurationManager.AppSettings[Constants.OUTPUT_DIR];
            switchDir = tempDir + Path.DirectorySeparatorChar + Constants.SWITCH_DIR;
            diffDir = tempDir + Path.DirectorySeparatorChar + Constants.DIFF_DIR;

            bool.TryParse(ConfigurationManager.AppSettings[Constants.IS_DISPLAY_DIRS], out isDisplayDir);
            
            rm = Options.ResManager;
            culture = Options.Culture;
        }
    }
}
