using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthentificationService.BusinessLogic.ServiceObjects
{
    public class RoleResponse
    {
        public SimpleRole Role
        {
            get;
            set;
        }

        public bool IsCacheable
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }
    }
}