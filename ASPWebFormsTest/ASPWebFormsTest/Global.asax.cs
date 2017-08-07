using System;
using System.Web;
using System.Web.Routing;

namespace ASPWebFormsTest
{
    public class Global : HttpApplication
    {
        // Обработчик события старта приложения. Срабатывает один раз при первом запросе к веб приложению.
        void Application_Start(object sender, EventArgs e)
        {
            // RouteTable.Routes - коллекция всех маршрутов текущего приложения.
            RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        // Метод для регистрации маршрутов.
        void RegisterRoutes(RouteCollection routes)
        {
            /*
            Маршрут для запросов brosers/ie, browsers/сhrome и т.д.
            Переходя по адресу browser/имя будет создаваться экземпляр страницы Browsers.aspx 
            для которой будет доступен параметр с именем name
            */
            routes.MapPageRoute("BrowsersCollection", "browsers/{name}", "~/13_Routing/02_RoutingWithParameterDestination.aspx");

            // Маршрут будет срабатывать при поступлении запроса подобного следующему calc/sum/10/20
            routes.MapPageRoute("Calculator", "calc/{operation}/{x}/{y}", "~/13_Routing/03_RoutingWithParametersDestination.aspx");

            /*
            Если на странице 03_RoutingWithParameters.aspx используется <%$RouteUrl: %> 
            адреса ссылок генерируются автоматически на основе таблицы маршрутизации.
            Поэтому при смене настроек маршрута, например, при изменении последовательности сегментов в маршруте,
            переписывать настройки ссылок в приложении не продеться.
            */
            //routes.MapPageRoute("Calculator", "calc/{x}/{y}/{operation}", "~/13_Routing/03_RoutingWithParametersDestination.aspx"); // СНЯТЬ КОММЕНТАРИЙ И ЗАКОММЕНТИРОВАТЬ ПРЕДЫДУЩИЙ МАРШРУТ

            routes.MapPageRoute("Route", "{lang}/{page}.aspx", "~/13_Routing/{page}.aspx");
        }
    }
}
