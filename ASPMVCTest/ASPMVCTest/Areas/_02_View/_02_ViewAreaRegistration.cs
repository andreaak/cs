using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._02_View
{
    public class _02_ViewAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_02_View";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_02_View_default",
                "_02_View/{controller}/{action}/{id}",
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
