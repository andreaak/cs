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

        public IList<LogData> Logs
        {
            get
            {
                return LinqToSqlConverter.Convert(dataContext.DataLog, dataContext);
            }
        }

        public void Init()
        {
        }

        public int Insert(int parentId, string description, DataTypes type)
        {
            var positions = dataContext.Entity
                .Where(itm => itm.ParentID == parentId)
                .Select(itm => itm.OrderPosition).ToArray();

            int position = positions.Length != 0 ? positions.Max() + 1 : DBConstants.START_POSITION;
            if (position < positions.Length)
            {
                position = positions.Length;
            }
            //    .Max();
            Entity item = new Entity
            {
                ParentID = parentId,
                Description = description,
                OrderPosition = position,
                Type = (byte)type,
                ModDate = DateConverter.GetCurrentDate()
            };
            dataContext.Entity.InsertOnSubmit(item);
            dataContext.SubmitChanges();

            if (type == DataTypes.NOTE)
            {
                EntityData itemData = new EntityData
                {
                    ID = item.ID,
                    ModDate = DateConverter.GetCurrentDate()
                };
                dataContext.EntityData.InsertOnSubmit(itemData);
                dataContext.SubmitChanges();
            }
            return item.ID;
            ;
        }

        public string GetTextData(int id)
        {
            EntityData item = dataContext.EntityData.FirstOrDefault(row => row.ID == id);
            if (item != null)
            {
                return item.Data;
            }
            return null;
        }

        public bool UpdateTextData(int id, string editValue, string plainText, string htmlText)
        {
            EntityData entityData = dataContext.EntityData.FirstOrDefault(item => item.ID == id);
            if (entityData != null 
                && (editValue != entityData.Data 
                || string.IsNullOrEmpty(entityData.HtmlData) 
                || string.IsNullOrEmpty(entityData.TextData)))
            {
                entityData.Data = editValue;
                entityData.TextData = plainText;
                entityData.HtmlData = htmlText;
                entityData.ModDate = DateConverter.GetCurrentDate();
                dataContext.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool UpdateDescription(int id, string description)
        {
            Entity entity = dataContext.Entity.FirstOrDefault(item => item.ID == id);
            if (entity != null && entity.Description != description)
            {
                entity.Description = description;
                entity.ModDate = DateConverter.GetCurrentDate();
                dataContext.SubmitChanges();
                return true;
            }
            return false;
        }

        public void Delete(int id)
        {
            Entity item = dataContext.Entity.FirstOrDefault(row => row.ID == id);
            if (item != null)
            {
                Entity[] subItems = dataContext.Entity
                    .Where(subItem => subItem.ParentID == item.ID)
                    .SelectMany(RecurseSelect).ToArray();
                if (subItems.Length != 0)
                {
                    int[] ids = subItems.Select(subItem => subItem.ID).ToArray();
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

        public bool IsCanChangeLevel(int position, int parentId, Direction direction)
        {
            if (direction == Direction.UP)
            {
                return !IsMaxLevel(parentId);
            }
            return !IsMinLevel(position, parentId);
        }

        public bool ChangeLevel(int position, int parentId, int id, Direction direction)
        {
            Entity currentitem = dataContext.Entity.FirstOrDefault(item => item.ID == id);
            if (currentitem == null)
            {
                return false;
            }

            int? destItemId;
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

            if (destItemId.HasValue)
            {
                var positions = dataContext.Entity
                .Where(item => item.ParentID == destItemId)
                .Select(item => item.OrderPosition).ToArray();
                int destPosition = positions.Length != 0 ? positions.Max() + 1 : DBConstants.START_POSITION;

                currentitem.ParentID = destItemId.Value;
                currentitem.OrderPosition = destPosition;
                currentitem.ModDate = DateConverter.GetCurrentDate();
                dataContext.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool IsCanMove(int position, int parentId, Direction direction)
        {
            if (direction == Direction.UP)
            {
                return !IsMinPosition(position, parentId);
            }
            return !IsMaxPosition(position, parentId);
        }

        public bool Move(int position, int parentId, int id, Direction direction)
        {
            Entity currentitem = dataContext.Entity.FirstOrDefault(item => item.ID == id);
            if (currentitem == null)
            {
                return false;
            }

            int destPosition;
            if (direction == Direction.UP)
            {
                var positions = dataContext.Entity
                    .Where(item => item.ParentID == parentId && item.OrderPosition < position)
                    .Select(item => item.OrderPosition).ToArray();
                destPosition = positions.Length != 0 ? positions.Max() : WRONG_POSITION;
            }
            else
            {
                var positions = dataContext.Entity
                    .Where(item => item.ParentID == parentId && item.OrderPosition > position)
                    .Select(item => item.OrderPosition).ToArray();
                destPosition = positions.Length != 0 ? positions.Min() : WRONG_POSITION;
            }
            if (destPosition == WRONG_POSITION)
            {
                return false;
            }

            Entity switchItem = dataContext.Entity.FirstOrDefault(item => item.ParentID == parentId && item.OrderPosition == destPosition);
            if (switchItem != null)
            {
                currentitem.OrderPosition = destPosition;
                currentitem.ModDate = DateConverter.GetCurrentDate();
                switchItem.OrderPosition = position;
                switchItem.ModDate = DateConverter.GetCurrentDate();
                dataContext.SubmitChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<DescriptionWithStatus> GetModifiedDescriptions(IDataRepository comparedRepository)
        {
            List<DescriptionWithStatus> list = new List<DescriptionWithStatus>();
            //added
            foreach (var comparedItem in comparedRepository.Descriptions)
            {
                bool isExist = dataContext.Entity.Any(item => item.ID == comparedItem.ID
                    && item.Description == comparedItem.EditValue);
                if (!isExist)
                {
                    AddDescription(comparedItem, DataStatus.Added, list);
                    AddParentDescriptions(comparedRepository, comparedItem.ID, list);
                }
            }
            //deleted
            foreach (var baseItem in Descriptions)
            {
                bool isExist = comparedRepository.Descriptions.Any(item => item.ID == baseItem.ID
                    && item.EditValue == baseItem.EditValue);
                if (!isExist)
                {
                    AddDescription(baseItem, DataStatus.Deleted, list);
                    AddParentDescriptions(this, baseItem.ID, list);
                }
            }

            foreach (var comparedItem in comparedRepository.Descriptions)
            {
                var baseItem = dataContext.Entity.FirstOrDefault(item => item.ID == comparedItem.ID
                    && item.Description == comparedItem.EditValue);
                if (baseItem != null
                    && IsCanCompare(baseItem.ModDate, comparedItem.ModDate)
                    && (string.IsNullOrEmpty(baseItem.ModDate) || comparedItem.ModDate > DateConverter.Convert(baseItem.ModDate)))
                {

                    AddDescription(comparedItem, DataStatus.Modified, list);
                    AddParentDescriptions(comparedRepository, comparedItem.ID, list);
                }
            }

            foreach (var comparedItem in comparedRepository.Descriptions)
            {
                var baseItem = dataContext.Entity.FirstOrDefault(item => item.ID == comparedItem.ID
                    && item.Description == comparedItem.EditValue);
                if (baseItem != null
                    && !string.IsNullOrEmpty(baseItem.ModDate)
                    && comparedItem.ModDate < DateConverter.Convert(baseItem.ModDate))
                {

                    AddDescription(comparedItem, DataStatus.Obsolete, list);
                    AddParentDescriptions(comparedRepository, comparedItem.ID, list);
                }
            }

            foreach (TextData comparedData in comparedRepository.Texts)
            {
                var baseData = dataContext.EntityData.FirstOrDefault(item => item.ID == comparedData.ID);
                if (baseData != null
                    && IsCanCompare(baseData.ModDate, comparedData.ModDate)
                    && (string.IsNullOrEmpty(baseData.ModDate) || comparedData.ModDate > DateConverter.Convert(baseData.ModDate))
                    && baseData.Data != comparedData.RtfText)
                {
                    var comparedItem = comparedRepository.Descriptions.FirstOrDefault(item => item.ID == comparedData.ID);
                    if (comparedItem != null)
                    {
                        AddDescription(comparedItem, DataStatus.Modified, list);
                        AddParentDescriptions(comparedRepository, comparedItem.ID, list);
                    }
                }
            }

            foreach (TextData comparedData in comparedRepository.Texts)
            {
                var baseData = dataContext.EntityData.FirstOrDefault(item => item.ID == comparedData.ID);
                if (baseData != null
                    && !string.IsNullOrEmpty(baseData.ModDate)
                    && comparedData.ModDate < DateConverter.Convert(baseData.ModDate)
                    && baseData.Data != comparedData.RtfText)
                {
                    var comparedItem = comparedRepository.Descriptions.FirstOrDefault(item => item.ID == comparedData.ID);
                    if (comparedItem != null)
                    {
                        AddDescription(comparedItem, DataStatus.Obsolete, list);
                        AddParentDescriptions(comparedRepository, comparedItem.ID, list);
                    }
                }
            }
            return list.Distinct(new EntityComparer<DescriptionWithStatus>());
        }

        private bool IsCanCompare(string baseDate, DateTime comparedDate)
        {
            return !(string.IsNullOrEmpty(baseDate) && comparedDate == DateTime.MinValue);
        }

        public IEnumerable<DescriptionWithText> Find(string text)
        {
            List<DescriptionWithText> descriptions = new List<DescriptionWithText>();
            var items = dataContext.EntityData.Where(item => item.TextData.Contains(text));
            if (items != null)
            {
                foreach (EntityData data in items)
                {
                    Entity entity = dataContext.Entity.FirstOrDefault(item => item.ID == data.ID);
                    if (entity != null)
                    {
                        string trimedText = GetText(text, data.TextData);
                        AddDescription(entity, trimedText, descriptions);
                        AddParentDescriptions(entity, descriptions);
                    }
                }

            }
            return descriptions.Distinct(new EntityComparer<DescriptionWithText>());
        }

        #endregion

        private IEnumerable<Entity> RecurseSelect(Entity item)
        {
            return (new[] { item }).Union(dataContext.Entity
            .Where(subItem => subItem.ParentID == item.ID)
            .SelectMany(RecurseSelect));
        }

        private bool IsMinPosition(int position, int parentId)
        {
            return !dataContext.Entity.Any(item => item.ParentID == parentId && item.OrderPosition < position);
        }

        private bool IsMaxPosition(int position, int parentId)
        {
            return !dataContext.Entity.Any(item => item.ParentID == parentId && item.OrderPosition > position);
        }

        private bool IsMaxLevel(int parentId)
        {
            return parentId <= DBConstants.BASE_LEVEL;
        }

        private bool IsMinLevel(int position, int parentId)
        {
            return !dataContext.Entity.Any(item => item.ParentID == parentId
                        && item.Type == (int)DataTypes.DIR
                        && item.OrderPosition
                        > position);
        }

        private void AddDescription(Description entity, DataStatus dataStatus, List<DescriptionWithStatus> list)
        {
            DescriptionWithStatus desc = new DescriptionWithStatus();
            LinqToSqlConverter.Convert(desc, entity);
            desc.Status = dataStatus;
            list.Add(desc);
        }

        private void AddParentDescriptions(IDataRepository repository, int id, List<DescriptionWithStatus> list)
        {
            Description entity = repository.Descriptions.FirstOrDefault(item => item.ID == id);
            while (entity != null)
            {
                DescriptionWithStatus desc = new DescriptionWithStatus();
                LinqToSqlConverter.Convert(desc, entity);
                desc.Status = DataStatus.Parent;
                list.Add(desc);

                entity = repository.Descriptions.FirstOrDefault(item => item.ID == entity.ParentID);
            }
        }

        private string GetText(string text, string fullText)
        {
            int index = fullText.IndexOf(text, StringComparison.OrdinalIgnoreCase);
            int startIndex = index < Options.SymbolsForFind ? 0 : index - Options.SymbolsForFind;
            int length = index + text.Length + Options.SymbolsForFind > fullText.Length
                ? fullText.Length - startIndex : index - startIndex + text.Length + Options.SymbolsForFind;
            return fullText.Substring(startIndex, length)
                .Replace(Environment.NewLine, " ")
                .Replace("  ", " ");
        }

        private void AddDescription(Entity entity, string text, List<DescriptionWithText> list)
        {
            list.Add(LinqToSqlConverter.Convert(entity, text));
        }

        private void AddParentDescriptions(Entity entity, List<DescriptionWithText> list)
        {
            while (entity != null && entity.ParentID > DBConstants.BASE_LEVEL)
            {
                Entity parent = dataContext.Entity.FirstOrDefault(item => item.ID == entity.ParentID);
                if (parent != null)
                {
                    list.Add(LinqToSqlConverter.ConvertWithText(parent));
                }
                entity = parent;
            }
        }
    }
}
