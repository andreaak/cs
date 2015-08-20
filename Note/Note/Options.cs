using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using DataManager;
using Utils.ActionWindow;

namespace Note
{
    public enum DocTypes
    {
        None,
        Doc,
        Rtf,
        Html,
        Mht,
    }

    public enum ExportDocTypes
    {
        Doc,
        Docx,
        Rtf,
        Html,
        Pdf,
        Txt,
    }
    
    public class Options
    {


        private static string dbFile = null;
        public static string DbFile
        {
            get
            {
                if (dbFile == null)
                {
                    DatabaseManager dataManager = new DatabaseManager();
                    dbFile = dataManager.GetDBFile();
                }
                return dbFile;
            }
            set { dbFile = value; }
        }

        private static DocTypes dbFormatType = DocTypes.None;
        public static DocTypes DbFormatType
        {
            get
            {
                if (dbFormatType == DocTypes.None)
                {
                    string temp = ConfigurationManager.AppSettings["dbFormatType"];
                    dbFormatType = GetType(temp, "Wrong db type parameter");
                }
                return dbFormatType;
            }
        }

        private static DocTypes inType = DocTypes.None;
        public static DocTypes InType
        {
            get
            {
                if (inType == DocTypes.None)
                {
                    string temp = ConfigurationManager.AppSettings["inType"];
                    inType = GetType(temp, "Wrong in parameter");
                }
                return inType;
            }
        }

        private static DocTypes outType = DocTypes.None;
        public static DocTypes OutType
        {
            get
            {
                if (outType == DocTypes.None)
                {
                    string temp = ConfigurationManager.AppSettings["outType"];
                    outType = GetType(temp, "Wrong out parameter");
                }
                return outType;
            }
        }

        private static DocTypes GetType(string typeStr, string errorMessage)
        {
            DocTypes type = DocTypes.None;
            try
            {
                type = (DocTypes)Enum.Parse(typeof(DocTypes), typeStr, true);
            }
            catch (Exception)
            {
                DisplayMessage.ShowError(errorMessage);
            }
            return type;
        }

        private static bool? idexPrefix;
        public static bool IndexPrefix
        {
            get
            {
                if (!idexPrefix.HasValue)
                {
                    idexPrefix = ConfigurationManager.AppSettings["indexPrefix"].ToLower() == "y";
                }
                return idexPrefix.Value;
            }
        }

        private static string dictionaryPath;
        public static string DictionaryPath
        {
            get
            {
                if (dictionaryPath == null)
                {
                    dictionaryPath = ConfigurationManager.AppSettings["dictionaryPath"];
                }
                return dictionaryPath;
            }
        }

        private static string alphabetPath;
        public static string AlphabetPath
        {
            get
            {
                if (alphabetPath == null)
                {
                    alphabetPath = ConfigurationManager.AppSettings["alphabetPath"];
                }
                return alphabetPath;
            }
        }
    }
}
