using System;
using System.Collections.Generic;
using DataManager.Repository;

namespace DataManager.Domain
{
    class EntityComparer : IEqualityComparer<Tuple<Entity, DataStatus>>
    {
        public bool Equals(Tuple<Entity, DataStatus> x, Tuple<Entity, DataStatus> y)
        {
            return x.Item1.ID == y.Item1.ID;
        }

        public int GetHashCode(Tuple<Entity, DataStatus> obj)
        {
            return obj.Item1.ID.GetHashCode();
        }
    }
}
