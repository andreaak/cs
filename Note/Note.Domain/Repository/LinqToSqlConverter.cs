using System;
using System.Collections.Generic;
using System.Linq;
using Note.Domain.Common;
using Note.Domain.Concrete.LinqToSql;
using Note.Domain.Entities;

namespace Note.Domain.Repository
{
    public static class LinqToSqlConverter
    {
        public static List<Description> Convert(IEnumerable<Entity> table)
        {
            return table.Select(Convert).ToList();
        }

        public static Description Convert(Entity entity)
        {
            Description item = new Description();
            Convert(item, entity);
            return item;
        }

        public static IList<TextData> Convert(IEnumerable<EntityData> table)
        {
            return table.Select(Convert).ToList();
        }

        public static TextData Convert(EntityData item)
        {
            return new TextData
            {
                ID = item.ID,
                EditValue = item.Data,
                PlainText = item.TextData,
                ModDate = string.IsNullOrEmpty(item.ModDate) ? DateTime.MinValue : DateTime.Parse(item.ModDate),
            };
        }

        public static DescriptionWithText Convert(Entity entity, string text)
        {
            DescriptionWithText item = new DescriptionWithText();
            Convert(item, entity);
            item.Text = text;
            return item;
        }

        public static DescriptionWithText ConvertWithText(Entity entity)
        {
            DescriptionWithText item = new DescriptionWithText();
            Convert(item, entity);
            return item;
        }

        public static void Convert(Description item, Entity entity)
        {
            item.ID = entity.ID;
            item.ParentID = (long)entity.ParentID;
            item.OrderPosition = (int)entity.OrderPosition;
            item.Type = (DataTypes)entity.Type;
            item.EditValue = entity.Description;
            item.ModDate = string.IsNullOrEmpty(entity.ModDate) ? DateTime.MinValue : DateTime.Parse(entity.ModDate);
        }

        public static void Convert(Description item, Description entity)
        {
            item.ID = entity.ID;
            item.ParentID = entity.ParentID;
            item.OrderPosition = entity.OrderPosition;
            item.Type = entity.Type;
            item.EditValue = entity.EditValue;
            item.ModDate = entity.ModDate;
        }
    }
}
