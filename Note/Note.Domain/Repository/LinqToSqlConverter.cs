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

        public static Description Convert(Entity item)
        {
            return new Description
            {
                ID = item.ID,
                ParentID = (long)item.ParentID,
                OrderPosition = (int)item.OrderPosition,
                Type = (DataTypes)item.Type,
                EditValue = item.Description,
                ModDate = string.IsNullOrEmpty(item.ModDate) ? DateTime.MinValue : DateTime.Parse(item.ModDate),
            };
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
    }
}
