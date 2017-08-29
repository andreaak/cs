using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._07_Ajax
{
    public class _07_AjaxAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_07_Ajax";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_07_Ajax_default",
                "_07_Ajax/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
