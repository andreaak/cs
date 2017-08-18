using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._01_Controller
{
    public class _01_ControllerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_01_Controller";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_01_Controller_default",
                "_01_Controller/{controller}/{action}/{id}",
                new
                {
                    controller = "_01_Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
