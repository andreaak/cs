using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._08_Optimization
{
    public class _08_OptimizationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_08_Optimization";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_08_Optimization_default",
                "_08_Optimization/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
