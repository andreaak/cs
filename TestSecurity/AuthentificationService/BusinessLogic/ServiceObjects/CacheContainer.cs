using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthentificationService.BusinessLogic.ServiceObjects
{
    public class CacheContainer
    {
        public HashSet<SimpleUser> Users
        {
            get;
            set;
        }

        public HashSet<SimpleRole> Roles
        {
            get;
            set;
        }

        public CacheContainer()
        {
            Users = new HashSet<SimpleUser>();
            Roles = new HashSet<SimpleRole>();
        }
    }
}