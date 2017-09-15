using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._01_Controller.Controllers
{
    // Базовый класс Controller наследует тип ControllerBase, который реализует метод Execute().
    // Также тип Controller обеспечивает связь с системой представлений.
    public class _01_HomeController : Controller
    {
        //
        // GET: /_01_Controller/_01_Home/

        /*
        Для запуска данного метода следует сделать запрос по адресу 
        'http://localhost:port/_01_Controller/_01_Home/Index' 
        или 'http://localhost:port/_01_Controller/_01_Home/'
        Также данный метод может быть запущен при выполнении запроса 'http://localhost:port/_01_Controller', 
        если путь /_01_Controller/_001_HomeController/Index
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

        public ActionResult C_01_BaseInfo()
        {
            return View();
        }

        [HttpGet] // метод будет вызываться только при GET запросах 
        public ActionResult C_02_TestActions()
        {
            ViewBag.Message = "Get Method";
            return View();
        }
        
        [HttpPost] // метод будет вызываться только при POST запросах 
        public ActionResult C_02_TestActions(int id)
        {
            ViewBag.Message = string.Format("Post Method: Id = {0}", id);
            return View();
        }


        [HttpGet] // метод будет вызываться только при GET запросах 
        public ActionResult C_03_TestActions(int id = -1)
        {
            ViewBag.Message = string.Format("Get Method: Id = {0}", id);
            return View();
        }

        [HttpPost, ActionName("C_03_TestActions")] // метод будет вызываться только при POST запросах 
        public ActionResult C_03_TestActions_(int id)
        {
            ViewBag.Message = string.Format("Post Method: Id = {0}", id);
            return View();
        }
    }
}
