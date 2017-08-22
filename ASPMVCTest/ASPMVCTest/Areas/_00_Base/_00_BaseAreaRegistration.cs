using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._00_Base
{
    public class _00_BaseAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_00_Base";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_00_Base_default",
                "_00_Base/{controller}/{action}/{id}",
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
