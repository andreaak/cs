using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._000_Base
{
    public class _000_BaseAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_000_Base";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_000_Base_default",
                "_000_Base/{controller}/{action}/{id}",
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
