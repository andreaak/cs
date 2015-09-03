using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using DbLinq.Sqlite;
using Note.Domain.Common;
using Note.Domain.Concrete;
using Note.Domain.Concrete.LinqToSql;
using Note.Domain.Entities;

namespace Note.Domain.Repository
{
    public class LinqToSqlRepository : IDataRepository
    {
        private const int WRONG_POSITION = -1;
        
        private readonly NoteDataContext dataContext;

        public LinqToSqlRepository(string connectionString)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            dataContext = new NoteDataContext(connection, new SqliteVendor());
        } 
       
        #region IDataRepository Members

        public bool IsProperDB
        {
            get
            {
                try
                {
                    dataContext.ExecuteQuery<Entity>(DBConstants.GetEntityExistQuery()).ToArray();
                    dataContext.ExecuteQuery<EntityData>(DBConstants.GetEntityExistQuery()).ToArray();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public IList<Description> Descriptions
        {
            get
            {
                return LinqToSqlConverter.Convert(dataContext.Entity);
            }
        }

        public IList<TextData> Texts
        {
            get
            {
                return LinqToSqlConverter.Convert(dataContext.EntityData);
            }
        }

        public void Init(){}

        public long Insert(long parentId, string description, DataTypes type)
        {
            var maxPos = dataContext.Entity
                .Where(itm => itm.ParentID == parentId)
                .Select(itm => itm.OrderPosition)
                .Max();
            Entity item = new Entity
            {
                ParentID = parentId,
                Description = description,
                OrderPosition = maxPos.HasValue ? maxPos + 1 : DBConstants.START_POSITION,
                Type = (byte) type,
                ModDate = DateTime.Now.ToString()
            };
            dataContext.Entity.InsertOnSubmit(item);
            dataContext.SubmitChanges();

            if (type == DataTypes.NOTE)
            {
                EntityData itemData = new EntityData
                {
                    ID = item.ID,
                    ModDate = DateTime.Now.ToString()
                };
                dataContext.EntityData.InsertOnSubmit(itemData);
                dataContext.SubmitChanges();
            }
            return item.ID; ;
        }

        public string GetTextData(long id)
        {
            EntityData item = dataContext.EntityData.FirstOrDefault(row => row.ID == id);
            if (item != null)
            {
                return item.Data;
            }
            return null;
        }

        public bool UpdateTextData(long id, string editValue, string plainText)
        {
            EntityData entityData = dataContext.EntityData.FirstOrDefault(item => item.ID == id);
            if (entityData != null && editValue != entityData.Data)
            {
                entityData.Data = editValue;
                entityData.TextData = plainText;
                entityData.ModDate = DateTime.Now.ToString();
                dataContext.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool UpdateDescription(long id, string description)
        {
            Entity entity = dataContext.Entity.FirstOrDefault(item => item.ID == id);
            if (entity != null && entity.Description != description)
            {
                entity.Description = description;
                entity.ModDate = DateTime.Now.ToString();
                dataContext.SubmitChanges();
                return true;
            }
            return false;
        }

        public void Delete(long id)
        {
            Entity item = dataContext.Entity.FirstOrDefault(row => row.ID == id);
            if (item != null)
            {
                Entity[] subItems = dataContext.Entity
                    .Where(subItem => subItem.ParentID == item.ID)
                    .SelectMany(RecurseSelect).ToArray();
                if (subItems.Length != 0)
                {
                    long[] ids = subItems.Select(subItem => subItem.ID).ToArray();
                    IEnumerable<EntityData> subItemsData = dataContext.EntityData
                        .Where(subItemData => ids.Contains(subItemData.ID));
                    dataContext.EntityData.DeleteAllOnSubmit(subItemsData);
                    dataContext.Entity.DeleteAllOnSubmit(subItems);
                }

                EntityData itemData = dataContext.EntityData.FirstOrDefault(subItem => subItem.ID == item.ID);
                if (itemData != null)
                {
                    dataContext.EntityData.DeleteOnSubmit(itemData);
                }
                dataContext.Entity.DeleteOnSubmit(item);
                dataContext.SubmitChanges();
            }
        }

        public bool IsCanChangeLevel(int position, long parentId, Direction direction)
        {
            if (direction == Direction.UP)
            {
                return !IsMaxLevel(parentId);
            }
            return !IsMinLevel(position, parentId);
        }

        public bool ChangeLevel(int position, long parentId, long id, Direction direction)
        {
            Entity currentitem = dataContext.Entity.FirstOrDefault(item => item.ID == id);
            if (currentitem == null)
            {
                return false;
            }

            long? destItemId;
            if (direction == Direction.UP)
            {
                Entity levelUpItem = dataContext.Entity.FirstOrDefault(item => item.ID == currentitem.ParentID && item.Type == (int)DataTypes.DIR);
                if (levelUpItem == null)
                {
                    return false;
                }
                destItemId = levelUpItem.ParentID;
            }
            else
            {
                Entity destItem = dataContext.Entity
                    .Where(item => item.ParentID == currentitem.ParentID
                    && item.Type == (int)DataTypes.DIR
                    && item.OrderPosition > currentitem.OrderPosition)
                    .OrderBy(item => item.OrderPosition)
                    .FirstOrDefault();
                if (destItem == null)
                {
                    return false;
                }
                destItemId = destItem.ID;
            }

            long destPosition = dataContext.Entity
                .Where(item => item.ParentID == destItemId)
                .Select(item => item.OrderPosition)
                .Max() + 1 ?? DBConstants.START_POSITION;

            if (destItemId.HasValue)
            {
                currentitem.ParentID = destItemId;
                currentitem.OrderPosition = destPosition;
                currentitem.ModDate = DateTime.Now.ToString();
                dataContext.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool IsCanMove(int position, long parentId, Direction direction)
        {
            if (direction == Direction.UP)
            {
                return !IsMinPosition(position, parentId);
            }
            return !IsMaxPosition(position, parentId);
        }

        public bool Move(int position, long parentId, long id, Direction direction)
        {
            Entity currentitem = dataContext.Entity.FirstOrDefault(item => item.ID == id);
            if (currentitem == null)
            {
                return false;
            }

            long destPosition;
            if (direction == Direction.UP)
            {
                destPosition = dataContext.Entity
                    .Where(item => item.ParentID == parentId && item.OrderPosition < position)
                    .Select(item => item.OrderPosition)
                    .Max() ?? WRONG_POSITION;
            }
            else
            {
                destPosition = dataContext.Entity
                    .Where(item => item.ParentID == parentId && item.OrderPosition > position)
                    .Select(item => item.OrderPosition)
                    .Min() ?? WRONG_POSITION;
            }
            if (destPosition == WRONG_POSITION)
            {
                return false;
            }

            Entity switchItem = dataContext.Entity.FirstOrDefault(item => item.ParentID == parentId && item.OrderPosition == destPosition);
            if (switchItem != null)
            {
                currentitem.OrderPosition = destPosition;
                currentitem.ModDate = DateTime.Now.ToString();
                switchItem.OrderPosition = position;
                switchItem.ModDate = DateTime.Now.ToString();
                dataContext.SubmitChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Tuple<Description, DataStatus>> GetModifiedDescriptions(IDataRepository comparedRepository)
        {
            List<Tuple<Description, DataStatus>> list = new List<Tuple<Description, DataStatus>>();
            //added
            foreach (var comparedItem in comparedRepository.Descriptions)
            {
                bool isExist = dataContext.Entity.Any(item => item.ID == comparedItem.ID);
                if (!isExist)
                {
                    AddDescription(list, comparedItem, DataStatus.Added);
                    AddParentDescriptions(comparedItem.ID, list);
                }
            }
            //deleted
            foreach (var baseItem in Descriptions)
            {
                bool isExist = comparedRepository.Descriptions.Any(item => item.ID == baseItem.ID);
                if (!isExist)
                {
                    AddDescription(list, baseItem, DataStatus.Deleted);
                    AddParentDescriptions(baseItem.ID, list);
                }
            }

            foreach (var comparedItem in comparedRepository.Descriptions)
	        {
                var baseItem = dataContext.Entity.FirstOrDefault(item => item.ID == comparedItem.ID);
                if (baseItem != null
                    && (string.IsNullOrEmpty(baseItem.ModDate) || comparedItem.ModDate > DateTime.Parse(baseItem.ModDate)))
                {

                    AddDescription(list, comparedItem, DataStatus.Modified);
                    AddParentDescriptions(comparedItem.ID, list);
                }
	        }

            foreach (var comparedItem in comparedRepository.Descriptions)
            {
                var baseItem = dataContext.Entity.FirstOrDefault(item => item.ID == comparedItem.ID);
                if (baseItem != null
                    && !string.IsNullOrEmpty(baseItem.ModDate)
                    && comparedItem.ModDate < DateTime.Parse(baseItem.ModDate))
                {

                    AddDescription(list, comparedItem, DataStatus.Obsolete);
                    AddParentDescriptions(comparedItem.ID, list);
                }
            }

            foreach (TextData comparedData in comparedRepository.Texts)
            {
                var baseData = dataContext.EntityData.FirstOrDefault(item => item.ID == comparedData.ID);
                if (baseData != null
                    && (string.IsNullOrEmpty(baseData.ModDate) || comparedData.ModDate > DateTime.Parse(baseData.ModDate)))
                {
                    var comparedItem = comparedRepository.Descriptions.FirstOrDefault(item => item.ID == comparedData.ID);
                    if (comparedItem != null)
                    {
                        AddDescription(list, comparedItem, DataStatus.Modified);
                        AddParentDescriptions(comparedItem.ID, list);
                    }
                }
            }

            foreach (TextData comparedData in comparedRepository.Texts)
            {
                var baseData = dataContext.EntityData.FirstOrDefault(item => item.ID == comparedData.ID);
                if (baseData != null
                    && !string.IsNullOrEmpty(baseData.ModDate)
                    && comparedData.ModDate < DateTime.Parse(baseData.ModDate))
                {
                    var comparedItem = comparedRepository.Descriptions.FirstOrDefault(item => item.ID == comparedData.ID);
                    if (comparedItem != null)
                    {
                        AddDescription(list, comparedItem, DataStatus.Obsolete);
                        AddParentDescriptions(comparedItem.ID, list);
                    }
                }
            }
            return list.Distinct(new EntityComparer());
        }

        #endregion

        private IEnumerable<Entity> RecurseSelect(Entity item)
        {
            return (new [] { item }).Union(dataContext.Entity
            .Where(subItem => subItem.ParentID == item.ID)
            .SelectMany(RecurseSelect));
        }

        private bool IsMinPosition(long position, long parentId)
        {
            return !dataContext.Entity.Any(item => item.ParentID == parentId && item.OrderPosition < position);
        }

        private bool IsMaxPosition(long position, long parentId)
        {
            return !dataContext.Entity.Any(item => item.ParentID == parentId && item.OrderPosition > position);
        }

        private bool IsMaxLevel(long parentId)
        {
            return parentId <= DBConstants.BASE_LEVEL;
        }

        private bool IsMinLevel(long position, long parentId)
        {
            return !dataContext.Entity.Any(item => item.ParentID == parentId
                        && item.Type == (int)DataTypes.DIR
                        && item.OrderPosition > position);
        }

        private void AddDescription(List<Tuple<Description, DataStatus>> list, Description entity, DataStatus dataStatus)
        {
            Tuple<Description, DataStatus> item =
                new Tuple<Description, DataStatus>(entity, dataStatus);
            list.Add(item);
        }

        private void AddParentDescriptions(long id, List<Tuple<Description, DataStatus>> list)
        {
            var entity = dataContext.Entity.FirstOrDefault(item => item.ID == id);
            while (entity != null && entity.ParentID.HasValue && entity.ParentID > DBConstants.BASE_LEVEL)
            {
                Entity parent = dataContext.Entity.FirstOrDefault(item => item.ID == entity.ParentID.Value);
                if (parent != null)
                {
                    Tuple<Description, DataStatus> item =
                        new Tuple<Description, DataStatus>(LinqToSqlConverter.Convert(parent), DataStatus.Parent);
                    list.Add(item);
                }
                entity = parent;
            }
        }
    }
}
