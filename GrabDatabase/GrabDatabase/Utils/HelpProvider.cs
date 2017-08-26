using System;
using Utils.WorkWithDB;
using System.Windows.Forms;
using Utils.ActionWindow;
using Utils.WorkWithDB.Connections;

namespace GrabDatabase
{
    class HelpProvider
    {
        private DBService dbService;

        public HelpProvider()
        {
            dbService = new DBService(Options.HelpProvider, Options.HelpConnString);
            InitDbConnection();
        }

        private void InitDbConnection()
        {
            bool dbConnectionOk = false;
            string errorMessage = "Help Database Error: ";
            try
            {
                if (dbService.IsValidConnection)
                {
                    if (!IsProperDB())
                    {
                        dbConnectionOk = CreateDb();
                    }
                    else
                    {
                        dbConnectionOk = true;
                    }
                }

            }
            catch (DbFileNotExistException)
            {
                try
                {
                    dbConnectionOk = CreateDb();
                }
                catch (Exception ex)
                {
                    errorMessage += ex.Message;
                }
            }
            catch (Exception ex)
            {
                errorMessage += ex.Message;
            }
            if (!dbConnectionOk)
            {
                DisplayMessage.ShowError(errorMessage);
            }
        }

        public string GetHelp(DataBaseType dataBaseType, DBConstants.DbCommand commandType)
        {
            string query = string.Format("SELECT H.DATA FROM HELP H, DATABASE DB, COMMAND C" +
            " WHERE H.DB_TYPE_ID = DB.ID AND H.COMMAND_ID = C.ID AND DB.TYPE = '{0}' AND C.TYPE = '{1}'",
            dataBaseType, commandType);
            string help = (String)dbService.ScalarQuery(query);
            return help;
        }

        public void SetHelp(DataBaseType dataBaseType, DBConstants.DbCommand commandType, string text)
        {
            try
            {
                string query = null;
                if (IsHelpExist(dataBaseType, commandType))
                {
                    query = string.Format("UPDATE HELP SET DATA = '{2}' WHERE DB_TYPE_ID = (SELECT ID FROM DATABASE WHERE TYPE = '{0}')" +
                    " AND COMMAND_ID = (SELECT ID FROM COMMAND WHERE TYPE = '{1}')",
                    dataBaseType, commandType, text);
                }
                else
                { 
                    query = string.Format("INSERT INTO HELP VALUES ({0}, (SELECT ID FROM DATABASE WHERE TYPE = '{1}')," +
                    "  (SELECT ID FROM COMMAND WHERE TYPE = '{2}'), '{3}')",
                    dbService.GetId("HELP"), dataBaseType, commandType, text);                    
                }
                
                int res = dbService.CRUDQuery(query);
                DisplayMessage.ShowInfo(DisplayMessage.INFO_DONE_CONST);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError(ex.Message);
            }
        }

        private bool IsHelpExist(DataBaseType dataBaseType, DBConstants.DbCommand commandType)
        {
            string query = string.Format("SELECT 1 FROM HELP H, DATABASE DB, COMMAND C" +
            " WHERE H.DB_TYPE_ID = DB.ID AND H.COMMAND_ID = C.ID AND DB.TYPE = '{0}' AND C.TYPE = '{1}'",
             dataBaseType, commandType);
            long? exist = (long?)dbService.ScalarQuery(query);
            return exist.HasValue;
        }

        public bool CreateDb()
        {
            return dbService.CreateHelpDB(Application.StartupPath + "\\help.db");
        }

        public bool IsProperDB()
        {
            try
            {
                dbService.SelectQuery(DBConstants.GetCommandExistQuery());
                dbService.SelectQuery(DBConstants.GetDatabaseExistQuery());
                dbService.SelectQuery(DBConstants.GetHelpDataExistQuery());            
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
