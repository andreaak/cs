using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas.E_30_Examples
{
    public class E_30_ExamplesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "E_30_Examples";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "E_30_Examples_default",
                "E_30_Examples/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
