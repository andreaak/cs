using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._01_Controller.Controllers
{
    public class _04_ReceiveSimpleTypesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult C_03_SendData()
        {
            return View();
        }

        public ActionResult C_04_PostInformation(string message)
        {
            //ViewBag.Message = Request.Form["message"];
            ViewBag.Message = message;
            return View("DisplayData");
        }

        public ActionResult C_05_RouteInformation(string id)
        {
            //ViewBag.Message = RouteData.Values["id"];
            ViewBag.Message = id;
            return View("DisplayData");
        }

        public ActionResult C_06_QueryStringInformation(string message)
        {
            //ViewBag.Message = Request.QueryString["message"];
            ViewBag.Message = message;
            return View("DisplayData");
        }


        /*
        Платформа предоставит значения для параметров, 
        проверив соответствующие объекты контекста, включая:
            Request.Form, 
            RouteData.Values, 
            Request.QueryString
        */

        // /_04_ReceiveSimpleTypes/SimpleType/5
        // /_04_ReceiveSimpleTypes/SimpleType?id=5
        public ActionResult SimpleType(int id)
        {
            ViewBag.Message = "id = " + id;
            return View("DisplayData");
        }

        //can use only variable name like in routes with Html.ActionLink
        // /_04_ReceiveSimpleTypes/SimpleType?ff=5
        public ActionResult SimpleType2(int ff)
        {
            ViewBag.Message = "ff = " + ff;
            return View("DisplayData");
        }

        // GET: /_04_ReceiveSimpleTypes/C_05_SimpleForm
        public ActionResult C_07_SimpleForm()
        {
            string x = Request.Params["x"];
            string y = Request.Params["y"];
            if (!string.IsNullOrEmpty(x) && !string.IsNullOrEmpty(y))
            {
                ViewBag.GetResult = x + y;
            }
            return View();
        }

        // POST: /_04_ReceiveSimpleTypes/C_05_SimpleForm
        [HttpPost] // метод будет вызываться только при POST запросах
        public ActionResult C_07_SimpleForm(int x, int y)
        {
            ViewBag.PostResult = x + y;

            return View();
        }
    }
}
