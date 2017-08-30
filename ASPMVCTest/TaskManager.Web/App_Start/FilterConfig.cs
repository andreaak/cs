﻿using System.Web;
using System.Web.Mvc;
using TaskManager.Web.Filters;

namespace TaskManager.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new InitializeSimpleMembershipAttribute());
        }
    }
}