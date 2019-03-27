using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._20_Architectures
{
    public class _20_ArchitecturesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_20_Architectures";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_20_Architectures_default",
                "_20_Architectures/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
