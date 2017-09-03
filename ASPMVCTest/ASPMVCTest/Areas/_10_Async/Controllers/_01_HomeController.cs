using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._10_Async.Clients;
using _01_ASPMVCTest.Areas._10_Async.Models;

namespace _01_ASPMVCTest.Areas._10_Async.Controllers
{
    public class _01_HomeController : Controller
    {
        //
        // GET: /_10_Async/_01_Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult A_01_BaseInfo()
        {
            return View();
        }

        public ActionResult A_02_SyncController()
        {
            ServicesModel model = new ServicesModel();
            NewsClient newsClient = new NewsClient();
            WeatherClient weatherClient = new WeatherClient();

            model.AddMessage("Index запущен");

            model.AddMessage("Выполнение задачи получения новостей");
            model.News = newsClient.GetNews();

            model.AddMessage("Выполнение задачи получения температуры");
            model.Weather = weatherClient.GetWeatherInfo();

            model.AddMessage("Index завершен");

            return View("Display", model);
        }

        public async Task<ActionResult> A_03_AsyncController()
        {
            ServicesModel model = new ServicesModel();
            NewsClient newsClient = new NewsClient();
            WeatherClient weatherClient = new WeatherClient();

            model.AddMessage("Index запущен");

            model.AddMessage("Выполнение задачи получения новостей");

            // После вызова метода newsClient.GetNewsAsync() поток IIS вернется в пул потоков.
            // Поток, который был создан в методе GetNewsAsync() получив ответ от службы продолжит выполнение 
            // данного метода действия.
            model.News = await newsClient.GetNewsAsync(); // задержка 2 секунды

            model.AddMessage("Выполнение задачи получения температуры");

            // На этом этапе будет создан еще один поток, предыдущий поток прекратит свою работу.
            model.Weather = await weatherClient.GetWeatherInfoAsync(); // задержка 2 секунды

            model.AddMessage("Index завершен");

            // ответ клиенту будет возвращать поток который был создан при вызове GetWeatherInfoAsync() 
            return View("Display", model);
        }

        public async Task<ActionResult> A_04_AsyncParallelController()
        {
            ServicesModel model = new ServicesModel();

            model.AddMessage("Запуск метода действия");

            #region Вызов асинхронных задач. Не корректное поведение контроллера

            //GetLastNewsAsync(model);
            //GetWeatherAsync(model);

            #endregion

            #region 1. Последовательное выполнение с await

            //await GetLastNewsAsync(model);
            //await GetWeatherAsync(model);
            #endregion

            #region 2. Параллельное выполнение c await

            Task newsTask = GetLastNewsAsync(model);
            Task weatherTask = GetWeatherAsync(model);
            await Task.WhenAll(newsTask, weatherTask);

            #endregion

            model.AddMessage("Завершение метода действия");

            ViewBag.Header = "Асинхронный метод действия с параллельными задачами";
            return View("Display", model);
        }

        async Task GetLastNewsAsync(ServicesModel model)
        {
            model.AddMessage("Запуск получения новостей");
            NewsClient newsClient = new NewsClient();
            model.News = await newsClient.GetNewsAsync();
            model.AddMessage("Завершение получения новостей");
        }

        async Task GetWeatherAsync(ServicesModel model)
        {
            model.AddMessage("Запуск получения погоды");
            WeatherClient weatherClient = new WeatherClient();
            model.Weather = await weatherClient.GetWeatherInfoAsync();
            model.AddMessage("Завершение получения погоды");
        }
    }
}
