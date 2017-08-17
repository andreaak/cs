using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._04_Routing.Controllers
{
    public class _01_HomeController : Controller
    {
        //
        // GET: /_04_Routing/_000_Index/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult A_01_BaseInfo()
        {
            return View();
        }

        public ActionResult A_02_DefaultValues()
        {
            ViewBag.Route = ((System.Web.Routing.Route)RouteData.Route).Url;
            return View();
        }

        public ActionResult A_03_StaticSegments()
        {
            ViewBag.Route = ((System.Web.Routing.Route)RouteData.Route).Url;
            return View();
        }

        public ActionResult A_04_SegmentVariables()
        {
            ViewBag.Title = "A_04_SegmentVariables";
            ViewBag.Route = ((System.Web.Routing.Route)RouteData.Route).Url;
            return View();
        }

        public ActionResult _04_ActionWithoutParameters()
        {
            /*
            RouteData.Values - коллекция значений сегментов, 
            доступных в момент обработки текущего запроса
            ViewBag - динамический словарь, 
            который позволяет передавать данные в представление
            */
            ViewBag.Title = "_04_ActionWithoutParameters";
            ViewBag.Route = ((System.Web.Routing.Route)RouteData.Route).Url;
            if (RouteData.Values["id"] != null)
            {
                ViewBag.Message = string.Format("ID = {0} RouteData.Values.Count= {1}", RouteData.Values["id"], RouteData.Values.Count);
            }
            else
            {
                ViewBag.Message = string.Format("X = {0} Y = {1} RouteData.Values.Count= {2}", RouteData.Values["x"], RouteData.Values["y"], RouteData.Values.Count);
            }

            /*
            Метод действия _04_ActionWithoutParameters по умолчанию 
            должен возвращать представление _04_ActionWithoutParameters.cshtml 
            из директории Views/Home
            Строковое значение, переданное в качестве параметра метода View, 
            позволяет задать другое представление, которое вернется в ответ на запрос
            */
            return View("A_04_SegmentVariables");
        }

        /*
        MVC Framework будет пытаться преобразовать значение, полученное из запроса, 
        в тип указанный в параметрах.
        Значения сегментов с именами x и y автоматически будут присвоены 
        параметрам метода _04_ActionWithParameters с теми же именами.
        В случае, если значения, которые были получены из сегментов, 
        не могут быть приведены к типу int, параметрам будет присвоено значение null, 
        так как тип параметров int?,
        В случае использования типа int произойдёт ошибка этапа выполнения.
        */
        public ActionResult _04_ActionWithParameters(int? x, int? y, int? id)
        {
            ViewBag.Title = "_04_ActionWithParameters";
            ViewBag.Route = ((System.Web.Routing.Route)RouteData.Route).Url;
            if (id.HasValue)
            {
                ViewBag.Message = string.Format("ID = {0}", id);
            }
            else
            {
                ViewBag.Message = string.Format("X = {0} Y = {1}", x, y);
            }

            return View("A_04_SegmentVariables");
        }

        /*
        int id = 10 в случае если значение будет отсутствовать в сегментах, 
        будет использовано значение 10
        */
        public ActionResult A_05_OptionalSegments(int? id = 10)
        {
            ViewBag.Title = "A_05_OptionalSegments";
            ViewBag.Route = ((System.Web.Routing.Route)RouteData.Route).Url;
            ViewBag.Message = string.Format("id = {0}", id);
            return View();
        }

        public ActionResult A_06_CatchAllSegments()
        {
            ViewBag.X = RouteData.Values["X"];
            ViewBag.Y = RouteData.Values["y"];
            ViewBag.Id = RouteData.Values["Id"];
            ViewBag.All = RouteData.Values["catchall"];
            ViewBag.Route = ((System.Web.Routing.Route)RouteData.Route).Url;
            return View();
        }

        public ActionResult A_07_SegmentsRestrictions()
        {
            ViewBag.Title = "A_07_SegmentsRestrictions";
            ViewBag.Route = ((System.Web.Routing.Route)RouteData.Route).Url;
            return View();
        }

        public ActionResult _07_ActionWithParameters(int? x, int? y)
        {
            return View("A_04_SegmentVariables");
        }

        public ActionResult A_08_IgnoreRote()
        {
            return View();
        }
    }
}
