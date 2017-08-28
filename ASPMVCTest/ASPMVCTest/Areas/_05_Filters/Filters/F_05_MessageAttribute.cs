using System;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._05_Filters.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class F_05_MessageAttribute : FilterAttribute, IActionFilter
    {
        public F_05_MessageAttribute()
        {
            
        }

        public F_05_MessageAttribute(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Write(string.Format("[Before action: {0}]", Message));
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write(string.Format("[After action: {0}]", Message));
        }
    }
}