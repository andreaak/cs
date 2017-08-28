using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._06_Security
{
    public class _06_SecurityAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_06_Security";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_06_Security_default",
                "_06_Security/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
