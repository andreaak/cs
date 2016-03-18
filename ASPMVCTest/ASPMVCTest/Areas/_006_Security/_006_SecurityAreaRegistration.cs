using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._006_Security
{
    public class _006_SecurityAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_006_Security";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_006_Security_default",
                "_006_Security/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
