﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _01_ASPMVCTest.Areas._002_View.Models
{
    public class TaskModel
    {
        public string Name
        {
            get;
            set;
        }
        public string StartDate
        {
            get;
            set;
        }
        public bool Completed
        {
            get;
            set;
        }
    }
}