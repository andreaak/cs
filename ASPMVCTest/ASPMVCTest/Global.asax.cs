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
        // Обработчик события старта приложения.
        protected void Application_Start()
        {
            // Регистрация областей. Поиск объектов типа производного от AreaRegistration и вызов методов RegisterArea для регистрации маршрутов каждой области.
            AreaRegistration.RegisterAllAreas();
            
            //регистрация пользовательской ValueProviderFactory
            ValueProviderFactories.Factories.Add(new CookiesValueProviderFactory());

            //регистрация пользовательского ModelBinder
            ModelBinders.Binders.Add(typeof(MyModel2), new MyModelBinder());

            // Регистрации маршрутов для работы WebAPI.
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            // Регистрация глобальных фильтров
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Регистрация маршрутов.
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Регистрация bundle для оптимизации загрузки CSS и JavaScript файлов.
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}