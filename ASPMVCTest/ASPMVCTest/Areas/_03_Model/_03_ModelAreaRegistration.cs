using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._003_Model
{
    public class _03_ModelAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_03_Model";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_03_Model_default",
                "_03_Model/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
