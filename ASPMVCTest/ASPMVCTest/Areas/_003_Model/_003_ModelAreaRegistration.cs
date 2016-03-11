using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._003_Model
{
    public class _003_ModelAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_003_Model";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_003_Model_default",
                "_003_Model/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
