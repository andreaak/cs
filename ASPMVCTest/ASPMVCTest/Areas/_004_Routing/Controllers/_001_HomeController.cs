using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._004_Routing.Controllers
{
    public class _001_HomeController : Controller
    {
        //
        // GET: /_004_Routing/_000_Index/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BaseInfo()
        {
            return View();
        }

        public ActionResult StaticSegments()
        {
            return View();
        }

        public ActionResult SegmentVariables()
        {
            return View();
        }

        public ActionResult SegmentVariables1()
        {
            // RouteData.Values - коллекция значений сегментов, доступных в момент обработки текущего запроса
            // ViewBag - динамический словарь, который позволяет передавать данные в представление
            ViewBag.Title = "SegmentVariables1";
            if (RouteData.Values["id"] != null)
            {
                ViewBag.Message = string.Format("ID = {0}", RouteData.Values["id"]);
            }
            else
            {
                ViewBag.Message = string.Format("X = {0} Y = {1}", RouteData.Values["x"], RouteData.Values["y"]);
            }
            return View("SegmentVariables");
        }

        /*
        MVC Framework будет пытаться преобразовать значение, полученное из запроса, в тип указанный в параметрах.
        Значения сегментов с именами x и y автоматически будут присвоены параметрам метода SegmentVariables2 с теми же именами.
        В случае, если значения, которые были получены из сегментов, не могут быть приведены к типу int, 
        параметрам будет присвоено значение null, так как тип параметров int?,
        в случае использования типа int произойдёт ошибка этапа выполнения.
        */
        public ActionResult SegmentVariables2(int? x, int? y, int? id)
        {
            ViewBag.Title = "SegmentVariables2";
            if (id.HasValue)
            {
                ViewBag.Message = string.Format("ID = {0}", id);
            }
            else
            {
                ViewBag.Message = string.Format("X = {0} Y = {1}", x, y);
            }
            
            return View("SegmentVariables");
        }

        // int id = 10 в случае если значение будет отсутствовать в сегментах, будет использовано значение 10
        public ActionResult OptionalSegments(int? id = 10)
        {
            ViewBag.Title = "OptionalSegments";
            ViewBag.Message = string.Format("id = {0}", id);
            return View();
        }

        public ActionResult CatchAllSegments()
        {
            ViewBag.Id = RouteData.Values["Id"];
            ViewBag.All = RouteData.Values["catchall"];
            return View();
        }
        
        public ActionResult SegmentsRestrictions()
        {
            ViewBag.Title = "SegmentsRestrictions";
            return View();
        }

        public ActionResult IgnoreRote()
        {
            return View();
        }
        
        public ActionResult GenerateLink()
        {
            return View();
        }

        public ActionResult Customer()
        {
            return View();
        }

        public ActionResult CreateCustomer(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult GenerateUrl()
        {
            return View();
        }

        public ActionResult Areas()
        {
            return View();
        }
    }
}
