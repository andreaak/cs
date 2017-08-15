using System.Web.Mvc;
using System.Web.Routing;

namespace _01_ASPMVCTest
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            /* 
            Регистрация маршрута с именем Default. 
            Который будет соответствовать URL состоящему из трех сегментов.
            Например, запрос к ресурсу Home/Index/10 будет обработан данным приложением.
            */
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "_000_Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}