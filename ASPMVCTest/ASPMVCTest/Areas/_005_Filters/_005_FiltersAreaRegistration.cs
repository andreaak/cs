using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._005_Filters
{
    public class _005_FiltersAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_005_Filters";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_005_Filters_default",
                "_005_Filters/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
