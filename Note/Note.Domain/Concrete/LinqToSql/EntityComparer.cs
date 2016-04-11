using System;
using System.Collections.Generic;
using Note.Domain.Common;
using Note.Domain.Entities;

namespace Note.Domain.Concrete.LinqToSql
{
    class EntityComparer<T> : IEqualityComparer<T> where T : Description
    {
        public bool Equals(T x, T y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(T obj)
        {
            return obj.ID.GetHashCode();
        }
    }
}
