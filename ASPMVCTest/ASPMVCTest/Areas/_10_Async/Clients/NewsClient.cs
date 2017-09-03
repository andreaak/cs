using System;
using System.Net.Http;
using System.Threading.Tasks;
using _01_ASPMVCTest.Areas._10_Async.Models;

namespace _01_ASPMVCTest.Areas._10_Async.Clients
{
    public class NewsClient
    {
        private const string newsUri = "http://localhost:8889/api/news";

        private HttpClient client = null;
        private NewsModel model = null;

        private Func<NewsModel> operation = null;

        public NewsClient()
        {
            client = new HttpClient();
            operation = new Func<NewsModel>(GetNews);
        }

        public NewsModel GetNews()
        {
            Task task = client
                .GetAsync(newsUri)
                .ContinueWith(taskWithResponse =>
                {
                    HttpResponseMessage response = taskWithResponse.Result;
                    Task<NewsModel> readResponse = response.Content.ReadAsAsync<NewsModel>();

                    model = readResponse.Result;
                });

            task.Wait();

            return model;
        }

        // Асинхронные методы

        public async Task<NewsModel> GetNewsAsync()
        {
            return await Task.Factory.StartNew<NewsModel>(GetNews);
        }

        public IAsyncResult BeginGetNews(AsyncCallback callback)
        {
            return operation.BeginInvoke(callback, null);
        }

        public NewsModel EndGetNews(IAsyncResult asyncResult)
        {
            return operation.EndInvoke(asyncResult);
        }
    }
}