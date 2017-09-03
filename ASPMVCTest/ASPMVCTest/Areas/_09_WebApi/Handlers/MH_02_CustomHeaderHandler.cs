using System.Net.Http;

namespace _01_ASPMVCTest.Areas._09_WebApi.Handlers
{
    public class MH_02_CustomHeaderHandler : DelegatingHandler
    {
        protected async override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            // В каждый ответ от сервера добавляем заголовок X-Custom-Header
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add("X-Custom-Header", "Hello world");
            return response;
        }
    }
}