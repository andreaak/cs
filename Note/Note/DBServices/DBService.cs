using System;
using Utils.WorkWithDB.Wrappers;
using System.Windows.Forms;
using System.IO;
using Utils.ActionWindow;
using Utils.WorkWithDB;
using Note;
using System.Collections.Generic;
using System.Linq;

namespace DBServices
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
    
    public class DBService
    {
        private ADBWrapper dbWrapper = null;
        private IDataService dataService = null;


        public DBService()
        {
            dbWrapper = WrapperFactory.GetWrapper();
            //DLDataService dataService = new DLDataService(dbWrapper);
            LinqToSqlService dataService = new LinqToSqlService(dbWrapper.DBConnection.Connection.ConnectionString);
            this.dataService = dataService;
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
                                            errorMessage, Environment.NewLine, Options.DbFile);
            if (DisplayMessage.ShowWarningYesNO(message) == DialogResult.Yes)
            {
                return dbWrapper.CreateDB(Application.StartupPath + "\\" + Options.DbFile, DBConstants.GetScript(dbWrapper.DBConnection.DbType));
            }
            return false;
        }

        public string GetDBFile()
        {
            return dbWrapper.GetDBFile();
        }

        public IEnumerable<Entity> GetNewestEntity(DBService dbServiceLocal)
        {
            return dataService.GetNewestEntity(dbServiceLocal.dataService);
        }

        #region IDataService Members

        public object BindEntity
        {
            get
            {
                return dataService.BindEntity;
            }
        }

        public bool IsProperDB
        {
            get
            {
                return dataService.IsProperDB;
            }
        }

        public long InsertNode(long parentId, string description,  DataTypes type)
        {
            return dataService.InsertNode(parentId, description,  type);
        }

        public void DeleteNode(long id)
        {
            dataService.DeleteNode(id);
        }

        public bool IsCanMoveNode(long position, long parentId, Direction direction)
        {
            return dataService.IsCanMoveNode(position, parentId, direction);
        }

        public void MoveNode(long currentPosition, long parentId, long id, Direction direction)
        {
            dataService.MoveNode(currentPosition, parentId, id, direction);
        }

        public void InitEntity()
        {
            dataService.InitEntity();
        }

        public string GetEntityData(long id)
        {
            return dataService.GetEntityData(id);
        }

        public void UpdateNode(long id, bool isNoteNode, string data, string textData)
        {
            dataService.UpdateNode(id, isNoteNode, data, textData);
        }

        public bool UpdateNodeData(long id, string data, string textData)
        {
            return dataService.UpdateNodeData(id, data, textData);
        }

        public bool UpdateNodeDescription(long id)
        {
            return dataService.UpdateNodeDescription(id);
        }

        public void ConvertData(ControlWrapper.IWrapper controlWrapper)
        {
            dataService.ConvertData(controlWrapper);
        }

        public bool IsCanChangeNodeLevel(long position, long parentId, Direction direction)
        {
            return dataService.IsCanChangeNodeLevel(position, parentId, direction);
        }

        public void ChangeNodeLevel(long position, long parentId, long id, Direction direction)
        {
            dataService.ChangeNodeLevel(position, parentId, id, direction);
        } 
       
        #endregion



    }

}
