using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DataManager.Domain;
using DataManager.Repository;
using Utils.ActionWindow;
using Utils.WorkWithDB;
using Utils.WorkWithDB.Wrappers;

namespace DataManager
{
    public class DatabaseManager
    {
        private readonly ADBWrapper dbWrapper = null;
        private readonly IDataRepository dataRepository = null;

        public DatabaseManager()
        {
            dbWrapper = WrapperFactory.GetWrapper();
            //dataRepository = new DLRepository(dbWrapper);
            dataRepository = new LinqToSqlRepository(dbWrapper.DBConnection.Connection.ConnectionString);
        }

        public bool IsDBOnline()
        {
            return dbWrapper.IsDBOnline();
        }

        public string GetConnectionDescription()
        {
            return string.Format(" {0}", dbWrapper.DBConnection.Connection.ConnectionString);
        }

        public void VacuumDb()
        {
            dbWrapper.NonQueryMethod("vacuum;");
        }

        public bool CreateDb()
        {
            string message = string.Format("Database fault!{0}Do you want to create new database: {1}?",
                                            Environment.NewLine, GetDBFileName());
            if (DisplayMessage.ShowWarningYesNO(message) == DialogResult.Yes)
            {
                return dbWrapper.CreateDB(Application.StartupPath + "\\" + GetDBFileName(), DBConstants.GetScript(dbWrapper.DBConnection.DbType));
            }
            return false;
        }

        public string GetDBFileName()
        {
            return dbWrapper.GetDBFileName();
        }

        #region IDataRepository Members

        public bool IsProperDB
        {
            get
            {
                return dataRepository.IsProperDB;
            }
        }

        public IEnumerable<Description> Descriptions
        {
            get
            {
                return dataRepository.Descriptions;
            }
        }

        public IEnumerable<TextData> Texts
        {
            get
            {
                return dataRepository.Texts;
            }
        }

        public IList<object> Updates
        {
            get
            {
                return dataRepository.Updates;
            }
        }

        public void Init()
        {
            dataRepository.Init();
        }

        public long Insert(long parentId, string description,  DataTypes type)
        {
            return dataRepository.Insert(parentId, description,  type);
        }

        public void Delete(long id)
        {
            dataRepository.Delete(id);
        }

        public bool IsCanChangeLevel(int position, long parentId, Direction direction)
        {
            return dataRepository.IsCanChangeLevel(position, parentId, direction);
        }

        public bool ChangeLevel(int position, long parentId, long id, Direction direction)
        {
            return dataRepository.ChangeLevel(position, parentId, id, direction);
        } 

        public bool IsCanMove(int position, long parentId, Direction direction)
        {
            return dataRepository.IsCanMove(position, parentId, direction);
        }

        public bool Move(int position, long parentId, long id, Direction direction)
        {
            return dataRepository.Move(position, parentId, id, direction);
        }

        public string GetTextData(long id)
        {
            return dataRepository.GetTextData(id);
        }

        public bool UpdateTextData(long id, string editValue, string plainText)
        {
            return dataRepository.UpdateTextData(id, editValue, plainText);
        }

        public bool UpdateDescription(long id, string description)
        {
            return dataRepository.UpdateDescription(id, description);
        }

        public IEnumerable<Tuple<Description, DataStatus>> GetModifiedDescriptions(DatabaseManager dataManagerLocal)
        {
            return dataRepository.GetModifiedDescriptions(dataManagerLocal.dataRepository);
        }
       
        #endregion
    }
}
