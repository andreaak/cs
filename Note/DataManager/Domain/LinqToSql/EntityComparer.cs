using System;
using System.Collections.Generic;
using DataManager.Repository;

namespace DataManager.Domain.LinqToSql
{
    class EntityComparer : IEqualityComparer<Tuple<Description, DataStatus>>
    {
        public bool Equals(Tuple<Description, DataStatus> x, Tuple<Description, DataStatus> y)
        {
            return x.Item1.ID == y.Item1.ID;
        }

        public int GetHashCode(Tuple<Description, DataStatus> obj)
        {
            return obj.Item1.ID.GetHashCode();
        }
    }
}
