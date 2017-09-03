using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._10_Async
{
    public class _10_AsyncAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_10_Async";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_10_Async_default",
                "_10_Async/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
