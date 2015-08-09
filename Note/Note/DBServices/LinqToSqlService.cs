using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using DbLinq.Data.Linq;
using DbLinq.Sqlite;
using Note;
using Utils.WorkWithDB.Wrappers;

namespace DBServices
{
    public class LinqToSqlService : IDataService
    {
        private SQLiteConnection connection = null;
        private NoteDataContext db = null;
        private const int WRONG_POSITION = -1;
        private const int START_POSITION = 0;
        DBServiceDataset.EntityDataTable table = null; 

        public LinqToSqlService(string connectionString)
        {
            connection = new SQLiteConnection(connectionString);
            db = new NoteDataContext(connection, new SqliteVendor());
            table = new DBServiceDataset.EntityDataTable();
        } 
       
        #region IDataService Members

        public object BindEntity
        {
            get
            {
                UpdateBindingTable();
                return table;
            }
        }

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

        public List<Entity> Entity
        {
            get
            {
                return db.Entity.ToList();
            }
        }

        public List<EntityData> EntityData
        {
            get
            {
                return db.EntityData.ToList();
            }
        }

        public long InsertNode(long parentId, string description, DataTypes type)
        {
            Entity item = new Entity();
            item.ParentID = parentId;
            item.Description = description;
            var maxPos = db.Entity
                .Where(itm => itm.ParentID == parentId)
                .Select(itm => itm.OrderPosition)
                .Max();
            item.OrderPosition = maxPos != null? maxPos + 1 : 0;
            item.Type = (byte)type;
            item.ModDate = DateTime.Now.ToString();
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
            UpdateBindingTable();
            return item.ID; ;
        }

        public void DeleteNode(long id)
        {
            Entity item = db.Entity.FirstOrDefault(row => row.ID == id);
            if (item != null)
            {
                EntityData itemData = db.EntityData.SingleOrDefault(subItem => subItem.ID == item.ID);
                IEnumerable<Entity> subItems = db.Entity
                    .Where(subItem => subItem.ParentID == item.ID)
                    .SelectMany(RecurseSelect);
                long[] ids = subItems.Select(subItem => subItem.ID).ToArray();
                IEnumerable<EntityData> subItemsData = db.EntityData
                    .Where(subItemData => ids.Contains(subItemData.ID));

                if (subItemsData != null)
                {
                    db.EntityData.DeleteAllOnSubmit(subItemsData);
                }
                if (subItems != null)
                {
                    db.Entity.DeleteAllOnSubmit(subItems);
                }

                if (itemData != null)
                {
                    db.EntityData.DeleteOnSubmit(itemData);
                }
                db.Entity.DeleteOnSubmit(item);
               
                db.SubmitChanges();
                UpdateBindingTable();
            }
        }

        public bool IsCanMoveNode(long position, long parentId, Direction direction)
        {
            if (direction == Direction.UP)
            {
                return !IsMinPosition(position, parentId);
            }
            else
            {
                return !IsMaxPosition(position, parentId);
            }
        }

        public void MoveNode(long currentPosition, long parentId, long id, Direction direction)
        {
            Entity currentitem = db.Entity.FirstOrDefault(item => item.ID == id);
            if (currentitem == null)
            {
                return;
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
                return;
            }

            Entity switchItem = db.Entity.FirstOrDefault(item => item.ParentID == parentId && item.OrderPosition == destPosition);
            if (switchItem != null)
            {
                currentitem.OrderPosition = destPosition;
                currentitem.ModDate = DateTime.Now.ToString();
                switchItem.OrderPosition = currentPosition;
                switchItem.ModDate = DateTime.Now.ToString();
                UpdateBindingTable();
                db.SubmitChanges();
            }
        }

        public void InitEntity(){}

        public string GetEntityData(long id)
        {
            EntityData item = db.EntityData.FirstOrDefault(row => row.ID == id);
            if (item != null)
            {
                return item.Data;
            }
            return null;
        }

        public void UpdateNode(long id, bool isNoteNode, string data, string textData)
        {
            bool isChanged = UpdateNodeDescription(id, false);
            isChanged |= UpdateNodeData(id, data, textData, false);
            if (isChanged)
            {
                db.SubmitChanges();
            }
        }

        public bool UpdateNodeData(long id, string data, string textData)
        {
            return UpdateNodeData(id, data, textData, true);
        }

        public bool UpdateNodeDescription(long id)
        {
            return UpdateNodeDescription(id, true);
        }

        public void ConvertData(ControlWrapper.IWrapper controlWrapper)
        {
            
        }


        public bool IsCanChangeNodeLevel(long position, long parentId, Direction direction)
        {
            if (direction == Direction.UP)
            {
                return !IsMaxLevel(position, parentId);
            }
            else
            {
                return !IsMinLevel(position, parentId);
            }
        }

        public void ChangeNodeLevel(long position, long parentId, long id, Direction direction)
        {
            Entity currentitem = db.Entity.FirstOrDefault(item => item.ID == id);
            if (currentitem == null)
            {
                return;
            }

            long? destItemId;
            if (direction == Direction.UP)
            {
                Entity levelUpItem = db.Entity.FirstOrDefault(item => item.ID == currentitem.ParentID && item.Type == (int)DataTypes.DIR);
                if (levelUpItem == null)
                {
                    return;
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
                    return;
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
                UpdateBindingTable();
                db.SubmitChanges();
            }
        }

        public IEnumerable<Tuple<Entity, DataStatus>> GetModifiedEntities(IDataService comparedService)
        {
            List<Tuple<Entity, DataStatus>> list = new List<Tuple<Entity, DataStatus>>();
            //added
            foreach (Entity comparedItem in comparedService.Entity)
            {
                bool isExist = db.Entity.Any(item => item.ID == comparedItem.ID);
                if (!isExist)
                {
                    AddEntity(list, comparedItem, DataStatus.Added);
                    AddParentEntities(comparedItem, list);
                }
            }
            //deleted
            foreach (Entity baseItem in Entity)
            {
                bool isExist = comparedService.Entity.Any(item => item.ID == baseItem.ID);
                if (!isExist)
                {
                    AddEntity(list, baseItem, DataStatus.Deleted);
                    AddParentEntities(baseItem, list);
                }
            }
            
            foreach (Entity comparedItem in comparedService.Entity)
	        {
                Entity baseItem = db.Entity.FirstOrDefault(item => item.ID == comparedItem.ID);
                if (baseItem != null
                    && !string.IsNullOrEmpty(comparedItem.ModDate) 
                    && (string.IsNullOrEmpty(baseItem.ModDate) || DateTime.Parse(comparedItem.ModDate) > DateTime.Parse(baseItem.ModDate)))
                {

                    AddEntity(list, comparedItem, DataStatus.Modified);
                    AddParentEntities(comparedItem, list);
                }
	        }

            foreach (Entity comparedItem in comparedService.Entity)
            {
                Entity baseItem = db.Entity.FirstOrDefault(item => item.ID == comparedItem.ID);
                if (baseItem != null
                    && !string.IsNullOrEmpty(baseItem.ModDate)
                    && (string.IsNullOrEmpty(comparedItem.ModDate) || DateTime.Parse(comparedItem.ModDate) < DateTime.Parse(baseItem.ModDate)))
                {

                    AddEntity(list, comparedItem, DataStatus.Obsolete);
                    AddParentEntities(comparedItem, list);
                }
            }

            foreach (EntityData comparedData in comparedService.EntityData)
            {
                EntityData baseData = db.EntityData.FirstOrDefault(item => item.ID == comparedData.ID);
                if (baseData != null
                    && !string.IsNullOrEmpty(comparedData.ModDate) 
                    && (string.IsNullOrEmpty(baseData.ModDate) || DateTime.Parse(comparedData.ModDate) > DateTime.Parse(baseData.ModDate)))
                {
                    Entity comparedItem = comparedService.Entity.FirstOrDefault(item => item.ID == comparedData.ID);
                    if (comparedItem != null)
                    {
                        AddEntity(list, comparedItem, DataStatus.Modified);
                        AddParentEntities(comparedItem, list);
                    }
                }
            }

            foreach (EntityData comparedData in comparedService.EntityData)
            {
                EntityData baseData = db.EntityData.FirstOrDefault(item => item.ID == comparedData.ID);
                if (baseData != null
                    && !string.IsNullOrEmpty(baseData.ModDate)
                    && (string.IsNullOrEmpty(comparedData.ModDate) || DateTime.Parse(comparedData.ModDate) < DateTime.Parse(baseData.ModDate)))
                {
                    Entity comparedItem = comparedService.Entity.FirstOrDefault(item => item.ID == comparedData.ID);
                    if (comparedItem != null)
                    {
                        AddEntity(list, comparedItem, DataStatus.Obsolete);
                        AddParentEntities(comparedItem, list);
                    }
                }
            }
            return list.Distinct(new EntityComparer());
        }

        private void AddEntity(List<Tuple<DBServices.Entity, DataStatus>> list, DBServices.Entity entity, DataStatus dataStatus)
        {
            Tuple<Entity, DataStatus> item =
                new Tuple<DBServices.Entity, DataStatus>(entity, dataStatus);
            list.Add(item);
        }

        private void AddParentEntities(Entity entity, List<Tuple<Entity, DataStatus>> list)
        {
            while (entity.ParentID.HasValue && entity.ParentID >= 0)
            {
                Entity ent = db.Entity.FirstOrDefault(item => item.ID == entity.ParentID.Value);
                if (ent != null)
                {
                    Tuple<Entity, DataStatus> item = 
                        new Tuple<DBServices.Entity, DataStatus>(ent, DataStatus.Parent);
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

        private void UpdateBindingTable()
        {
            IEnumerable<long> tableIds = table.Where(rw => UTILS.IsActiveRow(rw)).Select(row => row.ID);
            IEnumerable<long> entityIds = db.Entity.Select(item => item.ID);
            long[] idsForAdding = entityIds.Except(tableIds).ToArray();
            long[] idsForDeleting = tableIds.Except(entityIds).ToArray();

            foreach (var item in db.Entity.Where(item => idsForAdding.Contains(item.ID)))
            {
                table.AddEntityRow(
                    item.ID,
                    (long)item.ParentID,
                    item.Description,
                    (byte)item.Type,
                    (long)item.OrderPosition,
                    item.ModDate
                    );
            }

            table
                .Where(row => UTILS.IsActiveRow(row) && idsForDeleting.Contains(row.ID))
                .ToList()
                .ForEach(row => table.RemoveEntityRow(row));
            foreach (var item in db.GetChangeSet().Updates)
	        {
                Entity itm = item as Entity;
                if (itm != null)
                {
                    var rw = table.FirstOrDefault(row => row.ID == itm.ID);
                    if (rw != null)
                    {
                        rw.Description = itm.Description;
                        rw.OrderPosition = itm.OrderPosition ?? WRONG_POSITION;
                        rw.ParentID = itm.ParentID ?? WRONG_POSITION;
                    }
                }
	        }
            table.AcceptChanges();
        }

        private IEnumerable<Entity> RecurseSelect(Entity item)
        {
            return (new Entity[] { item }).Union(db.Entity
            .Where(subItem => subItem.ParentID == item.ID)
            .SelectMany(RecurseSelect));
        }

        private bool UpdateNodeData(long id, string data, string textData, bool isSubmitChanges)
        {
            EntityData entityData = db.EntityData.FirstOrDefault(item => item.ID == id);
            if (entityData != null && data != entityData.Data)
            {
                entityData.Data = data;
                entityData.TextData = textData;
                entityData.ModDate = DateTime.Now.ToString();
                if (isSubmitChanges)
                {
                    db.SubmitChanges();
                }
                return true;
            }
            return false;
        }

        private bool UpdateNodeDescription(long id, bool isSubmitChanges)
        {
            Entity entity = db.Entity.FirstOrDefault(item => item.ID == id);
            var row = table.FirstOrDefault(rw => UTILS.IsActiveRow(rw) && rw.ID == id);
            table.AcceptChanges();
            if (entity != null && row != null && entity.Description != row.Description)
            {
                entity.Description = row.Description;
                entity.ModDate = DateTime.Now.ToString();
                if (isSubmitChanges)
                {
                    db.SubmitChanges();
                }
                return true;
            }
            return false;
        }

        private bool IsMinPosition(long position, long parentId)
        {
            return !db.Entity.Any(item => item.ParentID == parentId && item.OrderPosition < position);
        }

        private bool IsMaxPosition(long position, long parentId)
        {
            return !db.Entity.Any(item => item.ParentID == parentId && item.OrderPosition > position);
        }

        private bool IsMaxLevel(long position, long parentId)
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
