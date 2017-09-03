using System.Net.Http;

namespace _01_ASPMVCTest.Areas._09_WebApi.Handlers
{
    public class MH_02_ValidationMessageHandler : DelegatingHandler
    {
        //Запрос не дойдет до контроллера так как его перехватит пользователь обработчик HTTP сообщений
        protected async override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (true) // проверка данных полученых в запросе (параметр request)
            {
                HttpResponseMessage message = new HttpResponseMessage();
                message.Content = new StringContent("Response From ValidationMessageHandler");

                //// Создание Task без указания делегата.
                //var taskSource = new TaskCompletionSource<HttpResponseMessage>();
                //taskSource.SetResult(message);
                return message;
            }
            else
            {
                return await base.SendAsync(request, cancellationToken);
            }

        }
    }
}