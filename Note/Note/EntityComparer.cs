using DBServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class EntityComparer : IEqualityComparer<Entity>
{
    public bool Equals(Entity x, Entity y)
    {
        return x.ID == y.ID;
    }

    public int GetHashCode(Entity obj)
    {
        return obj.ID.GetHashCode();
    }
}
