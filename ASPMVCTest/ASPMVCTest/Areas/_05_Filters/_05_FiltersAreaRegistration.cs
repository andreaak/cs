using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._05_Filters
{
    public class _05_FiltersAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_05_Filters";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_05_Filters_default",
                "_05_Filters/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
