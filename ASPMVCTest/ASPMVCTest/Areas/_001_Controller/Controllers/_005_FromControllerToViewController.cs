using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._001_Controller.Controllers
{
    public class _005_FromControllerToViewController : Controller
    {
        //
        // GET: /_001_Controller/_005_FromControllerToView/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UseModel()
        {
            DateTime date = DateTime.Now;

            // Передача данных в представление.
            return View(date);
        }

        public ActionResult UseViewData()
        {
            // Запись данных во ViewData для представления.
            ViewData["Date"] = DateTime.Now;
            return View();
        }
        
        public ActionResult UseViewBag()
        {
            // Использование объекта ViewBag (это динамический тип данных).
            ViewBag.Date = DateTime.Now;
            return View();
        }

        public ActionResult UseTempData()
        {
            return View();
        }

        public ActionResult ActionSetData()
        {
            // TempData удобно использовать, если необходимо сохранить данные между двумя запросами
            // Объект TempData похож на Session, но после прочтения значения из TempData оно помечается на удаление
            // и удаляется после обработки запроса.
            TempData["info"] = "hello world from ActionSetData";
            return View();
        }

        public ActionResult ActionReadData()
        {
            // При чтении значения по ключу info, после завершения обработки запроса, запись в TempData будет удалена.
            string info = TempData["info"] as string;
            return View((object)info);
        }
        
        public ActionResult ActionPeekData()
        {
            // Получение значения из TempData не помечая его на удаление.
            string info = TempData.Peek("info") as string;
            return View((object)info);
        }
    }
}
