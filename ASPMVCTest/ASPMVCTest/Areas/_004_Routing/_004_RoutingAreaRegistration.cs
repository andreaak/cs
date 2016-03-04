using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._004_Routing
{
    public class _004_RoutingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "_004_Routing";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "_004_Routing_Optional",
                "_004_Routing/{controller}/{action}/{id}",
                new
                {
                    controller = "_001_Home",
                    action = "Index",
                    id = UrlParameter.Optional// переменная сегмента со значением UrlParameter.Optional является необязательной
                }
            ); 

            context.MapRoute(
                "_004_Routing_default",
                "_004_Routing/{controller}/{action}/{x}/{y}",
                new
                {
                    controller = "_001_Home",
                    action = "SegmentVariables1",
                    x = "DefaultIdX",
                    y = "DefaultIdY"
                },
                /*
                Маршрут настроен таким образом, что для совпадения с входящим запросам, значения переменных сегментов
                 должны подходить под шаблон, который задан через регулярное выражение.
                 Данный маршрут требует, что бы переменная {controller} имела значение, которое начинается с символа H,
                 {action} должна иметь значение либо Index либо About, переменная {id} должна быть числовым значением.
                */
                constraints: new { controller = "^_001_H.*", action = "^SegmentVariables\\d?$"/*, id = "^\\d*$"*/ } 
            );

            context.MapRoute(
                name: "_004_Routing_PublicRoute",
                url: "_004_Routing/public/private/protected/{controller}/{action}"
            );
            
            context.MapRoute(
                name: "_004_Routing_CatchAll",
                url: "_004_Routing/{controller}/{action}/{x}/{y}/{id}/{*catchall}"// * перед именем переменной сегмента указывает на то что данная переменная получит значения всех оставшихся сегментов которые есть в URL
            );
        }
    }
}
