using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Note.Domain.Common;
using Note.Domain.Concrete;
using Note.Domain.Properties;
using Note.Domain.Repository;
using Note.Domain.Entities;
using Utils.ActionWindow;
using Utils.WorkWithDB;
using Utils.WorkWithDB.Wrappers;

namespace Note.Domain
{
    public class DatabaseManager
    {
        private readonly IDBWrapper dbWrapper;
        private readonly IDataRepository dataRepository;

        public DatabaseManager(IDBWrapper dbWrapper, IDataRepository dataRepository)
        {
            this.dbWrapper = dbWrapper;
            this.dataRepository = dataRepository;
        }

        #region ADBWrapper Members

        public bool IsDBOnline()
        {
            return dbWrapper.IsDBOnline();
        }

        public string GetConnectionDescription()
        {
            return dbWrapper.ConnectionString;
        }

        public void VacuumDb()
        {
            dbWrapper.NonQueryCommand("vacuum;");
        }

        public bool CreateDb()
        {
            string message = string.Format(Resources.DatabaseCreationQuestion, Environment.NewLine, GetDBFileName());
            if (DisplayMessage.ShowWarningYesNO(message) == DialogResult.Yes)
            {
                string fileFullName = Application.StartupPath + Path.DirectorySeparatorChar + GetDBFileName();
                return dbWrapper.CreateDB(fileFullName, DBConstants.GetScript(dbWrapper.DbType));
            }
            return false;
        }

        public string GetDBFileName()
        {
            return dbWrapper.GetDBFileName();
        }

        #endregion

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
