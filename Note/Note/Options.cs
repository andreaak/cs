using System;
using System.Configuration;
using DataManager;
using Utils.ActionWindow;
using Note.Properties;

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


        private static string dbFileName = null;
        public static string DbFileName
        {
            get
            {
                if (dbFileName == null)
                {
                    DatabaseManager dataManager = new DatabaseManager();
                    dbFileName = dataManager.GetDBFileName();
                }
                return dbFileName;
            }
            set
            {
                dbFileName = value;
            }
        }

        private static DocTypes dbFormatType = DocTypes.None;
        public static DocTypes DbFormatType
        {
            get
            {
                if (dbFormatType == DocTypes.None)
                {
                    string temp = ConfigurationManager.AppSettings["dbFormatType"];
                    dbFormatType = GetType(temp, Resources.WrongDbFormatType);
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
                    inType = GetType(temp, Resources.WrongInType);
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
                    outType = GetType(temp, Resources.WrongOutType);
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
