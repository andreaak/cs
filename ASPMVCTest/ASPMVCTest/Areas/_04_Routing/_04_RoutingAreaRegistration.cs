using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._04_Routing
{
    public class _04_RoutingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_04_Routing";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            /*
            Не маршрутизировать любые запросы направленные к файлам с расширением axd 
            и любой строкой параметров.
            */
            context.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /* 
            В данном проекте включена трассировка. 
            Для того что бы получить информацию трассировки необходимо выполнить запрос
            к виртуальному файлу trace.axd который находиться в корне приложения.
            trace.axd - является точкой входа, зарегистрированной для HTTP обработчика.
            Для включения трассировки используется элемент <trace enabled="true" /> 
            в файле web.config
            */

            context.MapRoute(
                name: "_04_Routing_Optional",
                url: "_04_Routing/{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "_01_Home",
                    action = "Index",
                    id = UrlParameter.Optional// переменная сегмента со значением UrlParameter.Optional является необязательной
                }
            );

            context.MapRoute(
                name: "_04_Routing_Variables",
                url: "_04_Routing/{controller}/{action}/{x}/{y}",
                defaults: new
                {
                    controller = "_01_Home",
                    action = "_04_ActionWithoutPatameters",
                    x = "DefaultIdX",
                    y = "DefaultIdY"
                },
                /*
                Маршрут настроен таким образом, что для совпадения с входящим запросам,
                значения переменных сегментов должны подходить под шаблон, 
                который задан через регулярное выражение.
                Данный маршрут требует, что бы переменная {controller} имела значение,
                которое начинается с символа H,
                {action} должна иметь значение либо Index либо About, 
                переменная {id} должна быть числовым значением.
                */
                constraints: new
                {
                    controller = "^_01_H.*",
                    action = "^_04_Action.*Parameters$"
                }
                /*, id = "^\\d*$"*/
                /*"^Index$|^About$"*/
            );

            context.MapRoute(
                name: "_04_Routing_PublicRoute",
                url: "_04_Routing/public/private/protected/{controller}/{action}"
            );

            context.MapRoute(
                name: "_04_Routing_CatchAll",
                url: "_04_Routing/{controller}/{action}/{x}/{y}/{id}/{*catchall}"// * перед именем переменной сегмента указывает на то что данная переменная получит значения всех оставшихся сегментов которые есть в URL
            );
        }
    }
}
