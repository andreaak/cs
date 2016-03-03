using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._001_Controller
{
    public class _001_ControllerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_001_Controller";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_001_Controller_default",
                "_001_Controller/{controller}/{action}/{id}",
                new
                {
                    controller = "_001_Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
