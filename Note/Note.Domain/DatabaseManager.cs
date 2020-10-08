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
            return dbWrapper.ConnectionString + " " + new FileInfo(dbWrapper.GetDBFileName()).Length / 1024 / 1024 + " MB";
        }

        public void VacuumDb()
        {
            dbWrapper.NonQueryCommand("vacuum;");
            dbWrapper.NonQueryCommand("DELETE FROM DATA_LOG");
        }

        public bool CreateDb()
        {
            string message = string.Format(Resources.DatabaseCreationQuestion, GetDBFileName());
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

        public IEnumerable<LogData> Logs
        {
            get
            {
                return dataRepository.Logs;
            }
        }

        public void Init()
        {
            dataRepository.Init();
        }

        public int Insert(int parentId, string description,  DataTypes type)
        {
            return dataRepository.Insert(parentId, description,  type);
        }

        public void Delete(int id)
        {
            dataRepository.Delete(id);
        }

        public bool IsCanChangeLevel(int position, int parentId, Direction direction)
        {
            return dataRepository.IsCanChangeLevel(position, parentId, direction);
        }

        public bool ChangeLevel(int position, int parentId, int id, Direction direction)
        {
            return dataRepository.ChangeLevel(position, parentId, id, direction);
        } 

        public bool IsCanMove(int position, int parentId, Direction direction)
        {
            return dataRepository.IsCanMove(position, parentId, direction);
        }

        public bool Move(int position, int parentId, int id, Direction direction)
        {
            return dataRepository.Move(position, parentId, id, direction);
        }

        public string GetTextData(int id)
        {
            return dataRepository.GetTextData(id);
        }

        public bool UpdateTextData(int id, string editValue, string plainText, string htmlText)
        {
            return dataRepository.UpdateTextData(id, editValue, plainText, htmlText);
        }

        public bool UpdateDescription(int id, string description)
        {
            return dataRepository.UpdateDescription(id, description);
        }

        public IEnumerable<DescriptionWithStatus> GetModifiedDescriptions(DatabaseManager dataManagerLocal)
        {
            return dataRepository.GetModifiedDescriptions(dataManagerLocal.dataRepository);
        }

        public IEnumerable<DescriptionWithText> Find(string text)
        {
            return dataRepository.Find(text);
        }

        public IEnumerable<EntityData> FindEntities(string text)
        {
            return dataRepository.FindEntities(text);
        }

        public IList<TextData> GetTextData()
        {
            return dataRepository.Texts;
        }

        #endregion
    }
}
