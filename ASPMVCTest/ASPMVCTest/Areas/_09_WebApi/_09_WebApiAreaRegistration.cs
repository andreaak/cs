using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._09_WebApi
{
    public class _09_WebApiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_09_WebApi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_09_WebApi_default",
                "_09_WebApi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
