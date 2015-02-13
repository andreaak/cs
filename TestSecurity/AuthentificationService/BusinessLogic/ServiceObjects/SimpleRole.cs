using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthentificationService.BusinessLogic.ServiceObjects
{
    public class SimpleRole : IEquatable<SimpleRole>
    {
        public string RoleName
        {
            get;
            set;
        }
        public HashSet<string> AssignedUsers
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is SimpleRole))
            {
                return false;
            }
            return this.RoleName == ((SimpleRole)obj).RoleName;
        }

        public override int GetHashCode()
        {
            return RoleName.GetHashCode();
        }

        #region IEquatable<SimpleRole> Members

        public bool Equals(SimpleRole other)
        {
            return this.RoleName == other.RoleName;
        }

        #endregion
    }
}