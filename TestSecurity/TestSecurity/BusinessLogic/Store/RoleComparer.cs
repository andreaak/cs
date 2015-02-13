using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSecurity.AuthentificationService;

namespace TestSecurity.BusinessLogic.Store
{
    class RoleComparer : IEqualityComparer<SimpleRole>
    {
        public bool Equals(SimpleRole x, SimpleRole y)
        {
            
            return x.RoleName == y.RoleName; 
        }

        public int GetHashCode(SimpleRole obj)
        {
            return obj.RoleName.GetHashCode();
        }
    }
}
