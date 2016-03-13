using _01_ASPMVCTest.Areas._003_Model.Models;
using _01_ASPMVCTest.ModelBinder;
using _01_ASPMVCTest.Providers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace _01_ASPMVCTest
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            ValueProviderFactories.Factories.Add(new CookiesValueProviderFactory());

            ModelBinders.Binders.Add(typeof(MyModel2), new MyModelBinder());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}