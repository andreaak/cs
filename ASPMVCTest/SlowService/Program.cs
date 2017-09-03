using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using System.Web.Http.SelfHost;

namespace _04_SlowService
{
    class Program
    {
        // Для корректной работы кода запустите студию от имени Администратора
        // Данное приложение необходимо запустить перед запуском остальных примеров этого урока.

        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8889");

            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(baseAddress);
            config.Routes.Add("default", new HttpRoute("api/{controller}"));

            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync();

            Console.WriteLine("Slow service is up and running...");

            Console.ReadKey();
            server.CloseAsync();
        }
    }
}
