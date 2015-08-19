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
    public enum DataTypes
    {
        DIR,
        NOTE,
    }

    public enum Direction
    {
        UP,
        DOWN,
    }
    
    public class NoteDataManager
    {
        private readonly ADBWrapper dbWrapper = null;
        private readonly IDataRepository dataRepository = null;

        public NoteDataManager()
        {
            dbWrapper = WrapperFactory.GetWrapper();
            //DLDataService _dataRepository = new DLDataService(dbWrapper);
            this.dataRepository = new LinqToSqlRepository(dbWrapper.DBConnection.Connection.ConnectionString);
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

        public bool CreateDb(string errorMessage)
        {
            string message = string.Format("{0} {1}Do you want to create new database: {2}?",
                                            errorMessage, Environment.NewLine, GetDBFile());
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

        public IEnumerable<Entity> Description
        {
            get
            {
                return dataRepository.Descriptions;
            }
        }

        public IEnumerable<EntityData> Data
        {
            get
            {
                return dataRepository.Data;
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

        public bool IsCanChangeNodeLevel(long position, long parentId, Direction direction)
        {
            return dataRepository.IsCanChangeNodeLevel(position, parentId, direction);
        }

        public bool ChangeNodeLevel(long position, long parentId, long id, Direction direction)
        {
            return dataRepository.ChangeNodeLevel(position, parentId, id, direction);
        } 

        public bool IsCanMoveNode(long position, long parentId, Direction direction)
        {
            return dataRepository.IsCanMoveNode(position, parentId, direction);
        }

        public bool MoveNode(long currentPosition, long parentId, long id, Direction direction)
        {
            return dataRepository.MoveNode(currentPosition, parentId, id, direction);
        }

        public string GetData(long id)
        {
            return dataRepository.GetData(id);
        }

        public bool UpdateData(long id, string data, string textData)
        {
            return dataRepository.UpdateData(id, data, textData);
        }

        public bool UpdateDescription(long id, string description)
        {
            return dataRepository.UpdateDescription(id, description);
        }
        
        public IEnumerable<Tuple<Entity, DataStatus>> GetModifiedDescriptions(NoteDataManager dbServiceLocal)
        {
            return dataRepository.GetModifiedDescriptions(dbServiceLocal.dataRepository);
        }
       
        #endregion

    }

}
