using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._001_Controller.Controllers
{
    // Базовый класс Controller наследует тип ControllerBase, который реализует метод Execute().
    // Также тип Controller обеспечивает связь с системой представлений.
    public class _001_HomeController : Controller
    {
        //
        // GET: /_001_Controller/_001_Home/

        /*
        Для запуска данного метода следует сделать запрос по адресу 
        'http://localhost:port/_001_Controller/_001_HomeController/Index' 
        или 'http://localhost:port/_001_Controller/_001_HomeController/'
        Также данный метод может быть запущен при выполнении запроса 'http://localhost:port/_001_Controller', 
        если путь /_001_Controller/_001_HomeController/Index
        настроен как путь по умолчанию в файле /App_Start/RouteConfig.cs
        */
        public ActionResult Index()
        {
            return View();
        }

        //Нельзя определить два действия с одним глаголом HTTP
        //Система не может выбрать используемое действие  
        //[HttpPost]
        //public ActionResult Index(string id)
        //{
        //    return View();
        //}

        public ActionResult BaseInfo()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult BaseInfo(int id)
        {
            ViewBag.Message = id;
            return View();
        }

        //Open other view
        public ActionResult OtherView()
        {
            ViewBag.Message = "Hello from _001_HomeController";
            return View("MyView");
        }
    }
}
