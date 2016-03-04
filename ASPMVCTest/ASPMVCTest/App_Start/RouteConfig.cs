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
            // Не маршрутизировать любые запросы направленные к файлам с расширением axd и любой строкой параметров.
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // В данном проекте включена трассировка. Для того что бы получить информацию трассировки необходимо выполнить запрос
            // к виртуальному файлу trace.axd который находиться в корне приложения.
            // trace.axd - является точкой входа, зарегистрированной для HTTP обработчика.
            // Для включения трассировки используется элемент <trace enabled="true" /> в файле web.config

            // Регистрация маршрута с именем Default. Который будет соответствовать URL состоящему из трех сегментов.
            // Например, запрос к ресурсу Home/Index/10 будет обработан данным приложением.
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