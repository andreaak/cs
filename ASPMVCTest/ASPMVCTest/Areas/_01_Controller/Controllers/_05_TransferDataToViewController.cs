using System;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._01_Controller.Controllers
{
    public class _05_TransferDataToViewController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult C_01_BaseInfo()
        {
            return View();
        }

        public ActionResult C_02_UseModel()
        {
            DateTime date = DateTime.Now;

            // Передача данных в представление.
            return View(date);
        }

        public ActionResult C_03_UseViewData()
        {
            // Запись данных во ViewData для представления.
            ViewData["Date"] = DateTime.Now;
            return View();
        }
        
        public ActionResult C_04_UseViewBag()
        {
            // Использование объекта ViewBag (это динамический тип данных).
            ViewBag.Date = DateTime.Now;
            return View();
        }

        public ActionResult C_05_UseTempData()
        {
            return View();
        }

        public ActionResult C_05_SetTempData()
        {
            /*
            TempData удобно использовать, если необходимо сохранить данные 
            между двумя запросами
            Объект TempData похож на Session, 
            но после прочтения значения из TempData оно помечается на удаление
            и удаляется после обработки запроса.
            */
            TempData["info"] = "hello world from ActionSetData";
            return View();
        }

        public ActionResult C_05_ReadTempData()
        {
            // При чтении значения по ключу info, после завершения обработки запроса, запись в TempData будет удалена.
            string info = TempData["info"] as string;
            return View((object)info);
        }
        
        public ActionResult C_05_PeekTempData()
        {
            // Получение значения из TempData не помечая его на удаление.
            string info = TempData.Peek("info") as string;
            return View((object)info);
        }
    }
}
