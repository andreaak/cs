using System;
using System.Net.Http;
using System.Threading.Tasks;
using _01_ASPMVCTest.Areas._10_Async.Models;

namespace _01_ASPMVCTest.Areas._10_Async.Clients
{
    public class WeatherClient
    {
        private const string weatherUri = "http://localhost:8889/api/weather";

        HttpClient client = null;
        WeatherModel model = new WeatherModel();

        private Func<WeatherModel> operation = null;

        public WeatherClient()
        {
            client = new HttpClient();
            operation = new Func<WeatherModel>(GetWeatherInfo);
        }

        public WeatherModel GetWeatherInfo()
        {
            Task task = client
                .GetAsync(weatherUri)
                .ContinueWith(response =>
                {
                    Task<WeatherModel> readResponse = response.Result.Content.ReadAsAsync<WeatherModel>();
                    model = readResponse.Result;
                });

            task.Wait();

            return model;
        }

        // Асинхронные методы

        public async Task<WeatherModel> GetWeatherInfoAsync()
        {
            return await Task.Factory.StartNew<WeatherModel>(GetWeatherInfo);
        }

        public IAsyncResult BeginGetTempereture(AsyncCallback callback)
        {
            return operation.BeginInvoke(callback, null);
        }

        public WeatherModel EndGetWeatherInfo(IAsyncResult asyncResult)
        {
            return operation.EndInvoke(asyncResult);
        }
    }
}