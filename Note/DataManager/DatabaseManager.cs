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
                                            Environment.NewLine, GetDBFile());
            if (DisplayMessage.ShowWarningYesNO(message) == DialogResult.Yes)
            {
                return dbWrapper.CreateDB(Application.StartupPath + "\\" + GetDBFile(), DBConstants.GetScript(dbWrapper.DBConnection.DbType));
            }
            return false;
        }

        public string GetDBFile()
        {
            return dbWrapper.GetDBFile();
        }

        #region IDataRepository Members

        public bool IsProperDB
        {
            get
            {
                return dataRepository.IsProperDB;
            }
        }

        public IEnumerable<Description> Description
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

        public void InitEntity()
        {
            dataRepository.Init();
        }

        public long InsertNode(long parentId, string description,  DataTypes type)
        {
            return dataRepository.InsertNode(parentId, description,  type);
        }

        public void DeleteNode(long id)
        {
            dataRepository.DeleteNode(id);
        }

        public bool IsCanChangeNodeLevel(int position, long parentId, Direction direction)
        {
            return dataRepository.IsCanChangeNodeLevel(position, parentId, direction);
        }

        public bool ChangeNodeLevel(int position, long parentId, long id, Direction direction)
        {
            return dataRepository.ChangeNodeLevel(position, parentId, id, direction);
        } 

        public bool IsCanMoveNode(int position, long parentId, Direction direction)
        {
            return dataRepository.IsCanMoveNode(position, parentId, direction);
        }

        public bool MoveNode(int position, long parentId, long id, Direction direction)
        {
            return dataRepository.MoveNode(position, parentId, id, direction);
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
        
        public IEnumerable<Tuple<Description, DataStatus>> GetModifiedDescriptions(DatabaseManager dbServiceLocal)
        {
            return dataRepository.GetModifiedDescriptions(dbServiceLocal.dataRepository);
        }
       
        #endregion
    }
}
