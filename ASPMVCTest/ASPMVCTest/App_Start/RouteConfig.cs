using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _01_ASPMVCTest
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


           /* routes.MapRoute(
                name: "001_Controllers", // Route name
                url: "001_Controllers/{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "_001_Controllers/_001_TestController",
                    action = "Index"
                },
                namespaces: new[] { "_01_ASPMVCTest.Controllers._001_Controllers" });*/
            
            //routes.MapRoute(
            //    name: "PublicRoute",
            //    url: "public/{controller}/{action}",
            //    defaults: new
            //    {
            //        controller = "_000_Index",
            //        action = "Index"
            //    }
            //);

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