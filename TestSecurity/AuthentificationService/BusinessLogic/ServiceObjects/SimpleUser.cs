using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthentificationService.BusinessLogic.ServiceObjects
{
    public class SimpleUser : IEquatable<SimpleUser>
    {
        
        public string Login
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is SimpleUser))
            {
                return false;
            }
            return this.Login == ((SimpleUser)obj).Login;
        }

        public override int GetHashCode()
        {
            return Login.GetHashCode();
        }

        #region IEquatable<SimpleRole> Members

        public bool Equals(SimpleUser other)
        {
            return this.Login == other.Login;
        }

        #endregion
    }
}