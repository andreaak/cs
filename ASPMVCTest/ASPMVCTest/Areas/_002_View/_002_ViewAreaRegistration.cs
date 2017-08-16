using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._002_View
{
    public class _002_ViewAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_002_View";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_002_View_default",
                "_002_View/{controller}/{action}/{id}",
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
