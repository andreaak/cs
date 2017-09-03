using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace _01_ASPMVCTest.Areas._09_WebApi.Handlers
{
    public class MH_02_MethodOverrideHandler : DelegatingHandler
    {
        readonly string[] _methods = { "DELETE", "HEAD", "PUT" };
        const string _header = "X-HTTP-Method-Override";

        /* 
        X-HTTP-Method-Override это не стандартный HTTP заголовок. 
        Он разработан для клиентов, которые не могут отправить определенные типы HTTP запросов, 
        например PUT или DELETE.
        Вместо этого клиент отправляет POST запрос с заголовком X-HTTP-Method-Override с требуемым заголовком.
        */
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Проверка наличия заголовка с именем X-HTTP-Method-Override.
            if (request.Method == HttpMethod.Post && request.Headers.Contains(_header))
            {
                // Проверка наличия значения заголовка в списке _header.
                var method = request.Headers.GetValues(_header).FirstOrDefault();
                if (_methods.Contains(method, StringComparer.InvariantCultureIgnoreCase))
                {
                    // Смена метода запроса.
                    request.Method = new HttpMethod(method);
                }
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}