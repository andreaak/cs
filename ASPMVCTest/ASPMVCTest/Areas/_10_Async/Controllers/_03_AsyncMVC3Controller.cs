using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._10_Async.Clients;
using _01_ASPMVCTest.Areas._10_Async.Models;

namespace _01_ASPMVCTest.Areas._10_Async.Controllers
{
    public class _03_AsyncMVC3Controller : AsyncController
    {
        //
        // GET: /_10_Async/_03_AsyncMVC3/

        /*
        В данном контроллере метод IndexAsync будет выполнен потоком из пула потоков IIS
        Во время выполнения метода IndexAsync будет запущено несколько асинхронных задач.
        Когда все асинхронные задачи завершат работу в отдельном потоке 
        будет запущен метод IndexCompleted, который вернет ответ клиенту.

        В данном случае поток из пула IIS будет блокирован минимальное время и сможет обработать большее 
        количество входящих запросов.
        */

        public void IndexAsync()
        {
            ServicesModel model = new ServicesModel();
            NewsClient newsClient = new NewsClient();
            WeatherClient weatherClient = new WeatherClient();

            AsyncManager.Parameters["model"] = model;

            model.AddMessage("IndexAsync запущен");

            AsyncManager.OutstandingOperations.Increment(); // указываем, что появляется новая асинхронная задача. (счетчик +1)
            newsClient.BeginGetNews(ar =>
            {
                model.AddMessage("Выполнение задачи получения новостей");
                model.News = newsClient.EndGetNews(ar);
                AsyncManager.OutstandingOperations.Decrement(); // указываем, что асинхронная задача завершена. (счетчик -1)
            });

            AsyncManager.OutstandingOperations.Increment(); // указываем, что появляется новая асинхронная задача. (счетчик +1)
            weatherClient.BeginGetTempereture(ar =>
            {
                model.AddMessage("Выполнение задачи получения температуры");
                model.Weather = weatherClient.EndGetWeatherInfo(ar);
                AsyncManager.OutstandingOperations.Decrement(); // указываем, что асинхронная задача завершена. (счетчик -1)
            });
        }

        // Метод будет запущен когда значение счетчика OutstandingOperations будет равное 0
        public ActionResult IndexCompleted(ServicesModel model)
        {
            model.AddMessage("IndexCompleted запущен");
            ViewBag.Header = "MVC3 Async Controller";

            return View("Display", model);
        }
    }
}
