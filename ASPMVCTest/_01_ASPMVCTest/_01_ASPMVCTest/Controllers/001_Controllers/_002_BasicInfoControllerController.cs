using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Controllers._001_Controllers
{
    // Базовый класс Controller наследует тип ControllerBase, который реализует метод Execute().
    // Также тип Controller обеспечивает связь с системой представлений.
    public class _002_BasicInfoControllerController : Controller
    {
        //
        // GET: /_002_BasicInfo/


        // Для запуска данного метода следует сделать запрос по адресу 
        // 'http://localhost:port/_002_BasicInfoController/Index' или 'http://localhost:port/_002_BasicInfoController/'
        // Также данный метод может быть запущен при выполнении запроса 'http://localhost:port/', если путь /_002_BasicInfoController/Index
        // настроен как путь по умолчанию в файле /App_Start/RouteConfig.cs
        public ActionResult Index()
        {
            return View("001_Controller/_002_BasicInfoController/Index");
        }

        //Open other view
        public ActionResult OtherView()
        {
            ViewBag.Message = "Hello from _002_BasicInfoController";
            return View("001_Controller/_002_BasicInfoController/MyView");
        }
    }
}
