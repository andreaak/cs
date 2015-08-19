using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using DataManager.Domain;
using DbLinq.Sqlite;

namespace DataManager.Repository
{
    public class LinqToSqlRepository : IDataRepository
    {
        private SQLiteConnection connection = null;
        private NoteDataContext db = null;
        public const int WRONG_POSITION = -1;
        private const int START_POSITION = 0;

        public LinqToSqlRepository(string connectionString)
        {
            connection = new SQLiteConnection(connectionString);
            db = new NoteDataContext(connection, new SqliteVendor());
        } 
       
        #region IDataRepository Members

        public bool IsProperDB
        {
            get
            {
                try
                {
                    db.ExecuteQuery<Entity>(DBConstants.GetEntityExistQuery()).ToArray();
                    db.ExecuteQuery<EntityData>(DBConstants.GetEntityExistQuery()).ToArray();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }


        public List<Entity> Descriptions
        {
            get
            {
                return db.Entity.ToList();
            }
        }

        public List<EntityData> Data
        {
            get
            {
                return db.EntityData.ToList();
            }
        }

        public IList<object> Updates
        {
            get
            {
                return db.GetChangeSet().Updates;
            }
        }

        public void Init(){}

        public long InsertNode(long parentId, string description, DataTypes type)
        {
            var maxPos = db.Entity
                .Where(itm => itm.ParentID == parentId)
                .Select(itm => itm.OrderPosition)
                .Max();
            Entity item = new Entity
            {
                ParentID = parentId,
                Description = description,
                OrderPosition = maxPos.HasValue ? maxPos + 1 : 0,
                Type = (byte) type,
                ModDate = DateTime.Now.ToString()
            };
            db.Entity.InsertOnSubmit(item);
            db.SubmitChanges();

            if (type == DataTypes.NOTE)
            {
                EntityData itemData = new EntityData();
                itemData.ID = item.ID;
                itemData.ModDate = DateTime.Now.ToString();
                db.EntityData.InsertOnSubmit(itemData);
                db.SubmitChanges();
            }
            return item.ID; ;
        }

        public void DeleteNode(long id)
        {
            Entity item = db.Entity.FirstOrDefault(row => row.ID == id);
            if (item != null)
            {
                Entity[] subItems = db.Entity
                    .Where(subItem => subItem.ParentID == item.ID)
                    .SelectMany(RecurseSelect).ToArray();
                if (subItems.Length != 0)
                {
                    long[] ids = subItems.Select(subItem => subItem.ID).ToArray();
                    IEnumerable<EntityData> subItemsData = db.EntityData
                        .Where(subItemData => ids.Contains(subItemData.ID));
                    db.EntityData.DeleteAllOnSubmit(subItemsData);
                    db.Entity.DeleteAllOnSubmit(subItems);
                }

                EntityData itemData = db.EntityData.FirstOrDefault(subItem => subItem.ID == item.ID);
                if (itemData != null)
                {
                    db.EntityData.DeleteOnSubmit(itemData);
                }
                db.Entity.DeleteOnSubmit(item);
                db.SubmitChanges();
            }
        }

        public bool IsCanChangeNodeLevel(long position, long parentId, Direction direction)
        {
            if (direction == Direction.UP)
            {
                return !IsMaxLevel(parentId);
            }
            return !IsMinLevel(position, parentId);
        }

        public bool ChangeNodeLevel(long position, long parentId, long id, Direction direction)
        {
            Entity currentitem = db.Entity.FirstOrDefault(item => item.ID == id);
            if (currentitem == null)
            {
                return false;
            }

            long? destItemId;
            if (direction == Direction.UP)
            {
                Entity levelUpItem = db.Entity.FirstOrDefault(item => item.ID == currentitem.ParentID && item.Type == (int)DataTypes.DIR);
                if (levelUpItem == null)
                {
                    return false;
                }
                destItemId = levelUpItem.ParentID;
            }
            else
            {
                Entity destItem = db.Entity
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

            long destPosition = db.Entity
                .Where(item => item.ParentID == destItemId)
                .Select(item => item.OrderPosition)
                .Max() + 1 ?? START_POSITION;

            if (destItemId.HasValue)
            {
                currentitem.ParentID = destItemId;
                currentitem.OrderPosition = destPosition;
                currentitem.ModDate = DateTime.Now.ToString();
                db.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool IsCanMoveNode(long position, long parentId, Direction direction)
        {
            if (direction == Direction.UP)
            {
                return !IsMinPosition(position, parentId);
            }
            return !IsMaxPosition(position, parentId);
        }

        public bool MoveNode(long currentPosition, long parentId, long id, Direction direction)
        {
            Entity currentitem = db.Entity.FirstOrDefault(item => item.ID == id);
            if (currentitem == null)
            {
                return false;
            }

            long destPosition;
            if (direction == Direction.UP)
            {
                destPosition = db.Entity
                    .Where(item => item.ParentID == parentId && item.OrderPosition < currentPosition)
                    .Select(item => item.OrderPosition)
                    .Max() ?? WRONG_POSITION;
            }
            else
            {
                destPosition = db.Entity
                    .Where(item => item.ParentID == parentId && item.OrderPosition > currentPosition)
                    .Select(item => item.OrderPosition)
                    .Min() ?? WRONG_POSITION;
            }
            if (destPosition == WRONG_POSITION)
            {
                return false;
            }

            Entity switchItem = db.Entity.FirstOrDefault(item => item.ParentID == parentId && item.OrderPosition == destPosition);
            if (switchItem != null)
            {
                currentitem.OrderPosition = destPosition;
                currentitem.ModDate = DateTime.Now.ToString();
                switchItem.OrderPosition = currentPosition;
                switchItem.ModDate = DateTime.Now.ToString();
                db.SubmitChanges();
                return true;
            }
            return false;
        }

        public string GetData(long id)
        {
            EntityData item = db.EntityData.FirstOrDefault(row => row.ID == id);
            if (item != null)
            {
                return item.Data;
            }
            return null;
        }

        public bool UpdateData(long id, string data, string textData)
        {
            EntityData entityData = db.EntityData.FirstOrDefault(item => item.ID == id);
            if (entityData != null && data != entityData.Data)
            {
                entityData.Data = data;
                entityData.TextData = textData;
                entityData.ModDate = DateTime.Now.ToString();
                db.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool UpdateDescription(long id, string description)
        {
            Entity entity = db.Entity.FirstOrDefault(item => item.ID == id);
            if (entity != null && entity.Description != description)
            {
                entity.Description = description;
                entity.ModDate = DateTime.Now.ToString();
                db.SubmitChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Tuple<Entity, DataStatus>> GetModifiedDescriptions(IDataRepository comparedRepository)
        {
            List<Tuple<Entity, DataStatus>> list = new List<Tuple<Entity, DataStatus>>();
            //added
            foreach (Entity comparedItem in comparedRepository.Descriptions)
            {
                bool isExist = db.Entity.Any(item => item.ID == comparedItem.ID);
                if (!isExist)
                {
                    AddDescription(list, comparedItem, DataStatus.Added);
                    AddParentDescriptions(comparedItem, list);
                }
            }
            //deleted
            foreach (Entity baseItem in Descriptions)
            {
                bool isExist = comparedRepository.Descriptions.Any(item => item.ID == baseItem.ID);
                if (!isExist)
                {
                    AddDescription(list, baseItem, DataStatus.Deleted);
                    AddParentDescriptions(baseItem, list);
                }
            }
            
            foreach (Entity comparedItem in comparedRepository.Descriptions)
	        {
                Entity baseItem = db.Entity.FirstOrDefault(item => item.ID == comparedItem.ID);
                if (baseItem != null
                    && !string.IsNullOrEmpty(comparedItem.ModDate) 
                    && (string.IsNullOrEmpty(baseItem.ModDate) || DateTime.Parse(comparedItem.ModDate) > DateTime.Parse(baseItem.ModDate)))
                {

                    AddDescription(list, comparedItem, DataStatus.Modified);
                    AddParentDescriptions(comparedItem, list);
                }
	        }

            foreach (Entity comparedItem in comparedRepository.Descriptions)
            {
                Entity baseItem = db.Entity.FirstOrDefault(item => item.ID == comparedItem.ID);
                if (baseItem != null
                    && !string.IsNullOrEmpty(baseItem.ModDate)
                    && (string.IsNullOrEmpty(comparedItem.ModDate) || DateTime.Parse(comparedItem.ModDate) < DateTime.Parse(baseItem.ModDate)))
                {

                    AddDescription(list, comparedItem, DataStatus.Obsolete);
                    AddParentDescriptions(comparedItem, list);
                }
            }

            foreach (EntityData comparedData in comparedRepository.Data)
            {
                EntityData baseData = db.EntityData.FirstOrDefault(item => item.ID == comparedData.ID);
                if (baseData != null
                    && !string.IsNullOrEmpty(comparedData.ModDate) 
                    && (string.IsNullOrEmpty(baseData.ModDate) || DateTime.Parse(comparedData.ModDate) > DateTime.Parse(baseData.ModDate)))
                {
                    Entity comparedItem = comparedRepository.Descriptions.FirstOrDefault(item => item.ID == comparedData.ID);
                    if (comparedItem != null)
                    {
                        AddDescription(list, comparedItem, DataStatus.Modified);
                        AddParentDescriptions(comparedItem, list);
                    }
                }
            }

            foreach (EntityData comparedData in comparedRepository.Data)
            {
                EntityData baseData = db.EntityData.FirstOrDefault(item => item.ID == comparedData.ID);
                if (baseData != null
                    && !string.IsNullOrEmpty(baseData.ModDate)
                    && (string.IsNullOrEmpty(comparedData.ModDate) || DateTime.Parse(comparedData.ModDate) < DateTime.Parse(baseData.ModDate)))
                {
                    Entity comparedItem = comparedRepository.Descriptions.FirstOrDefault(item => item.ID == comparedData.ID);
                    if (comparedItem != null)
                    {
                        AddDescription(list, comparedItem, DataStatus.Obsolete);
                        AddParentDescriptions(comparedItem, list);
                    }
                }
            }
            return list.Distinct(new EntityComparer());
        }

        private void AddDescription(List<Tuple<Entity, DataStatus>> list, Entity entity, DataStatus dataStatus)
        {
            Tuple<Entity, DataStatus> item =
                new Tuple<Entity, DataStatus>(entity, dataStatus);
            list.Add(item);
        }

        private void AddParentDescriptions(Entity entity, List<Tuple<Entity, DataStatus>> list)
        {
            while (entity.ParentID.HasValue && entity.ParentID >= 0)
            {
                Entity ent = db.Entity.FirstOrDefault(item => item.ID == entity.ParentID.Value);
                if (ent != null)
                {
                    Tuple<Entity, DataStatus> item = 
                        new Tuple<Entity, DataStatus>(ent, DataStatus.Parent);
                    list.Add(item);
                    entity = ent;
                }
                else
                {
                    break;
                }
            }
        
        }

        #endregion

        private IEnumerable<Entity> RecurseSelect(Entity item)
        {
            return (new [] { item }).Union(db.Entity
            .Where(subItem => subItem.ParentID == item.ID)
            .SelectMany(RecurseSelect));
        }

        private bool IsMinPosition(long position, long parentId)
        {
            return !db.Entity.Any(item => item.ParentID == parentId && item.OrderPosition < position);
        }

        private bool IsMaxPosition(long position, long parentId)
        {
            return !db.Entity.Any(item => item.ParentID == parentId && item.OrderPosition > position);
        }

        private bool IsMaxLevel(long parentId)
        {
            return parentId <= DBConstants.BASE_LEVEL;
        }

        private bool IsMinLevel(long position, long parentId)
        {
            return !db.Entity.Any(item => item.ParentID == parentId
                        && item.Type == (int)DataTypes.DIR
                        && item.OrderPosition > position);
        }
    
    }

    public enum DataStatus
    {
        Added,
        Modified,
        Obsolete,
        Deleted,
        Parent,
    }
}
