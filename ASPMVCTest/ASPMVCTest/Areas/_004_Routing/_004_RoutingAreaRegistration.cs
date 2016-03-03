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
                name: "_004_Routing_PublicRoute",
                url: "_004_Routing/public/{controller}/{action}"
            );

            context.MapRoute(
                "_004_Routing_Optional",
                "_004_Routing/{controller}/{action}/{id}",
                new
                {
                    controller = "_001_Home",
                    action = "OptionalSegments",
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
                }
            );
            

        }
    }
}
