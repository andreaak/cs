using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _01_ASPMVCTest.Areas._002_View.Models
{
    public class Person
    {
        public int PersonId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public Role Role
        {
            get;
            set;
        }
    }

    public enum Role
    {
        Guest,
        User,
        Admin
    }
}