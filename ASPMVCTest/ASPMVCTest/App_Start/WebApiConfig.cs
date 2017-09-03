using System.Web.Http;
using _01_ASPMVCTest.Areas._09_WebApi.Handlers;
using _01_ASPMVCTest.Areas._09_WebApi.MediTypeFormatters;

namespace _01_ASPMVCTest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                }
            );

            config.Formatters.Add(new BinaryMediaTypeFormatter());
            config.MessageHandlers.Add(new MH_02_TestMessageHandler());
            //config.MessageHandlers.Add(new MH_02_ValidationMessageHandler());
            config.MessageHandlers.Add(new MH_02_MethodOverrideHandler());
            config.MessageHandlers.Add(new MH_02_CustomHeaderHandler());
        }
    }
}
