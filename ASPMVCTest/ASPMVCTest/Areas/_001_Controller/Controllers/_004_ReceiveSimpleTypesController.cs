using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._001_Controller.Controllers
{
    public class _004_ReceiveSimpleTypesController : Controller
    {
        //
        // GET: /_001_Controller/_004_ReceiveSimpleTypes/

        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Data/PostInformation

        public ActionResult PostInformation(string message)
        {
            //ViewBag.Message = Request.Form["message"];
            ViewBag.Message = message;
            return View("Index");
        }

        //
        // GET: /Data/RouteInformation

        public ActionResult RouteInformation(string id)
        {
            //ViewBag.Message = RouteData.Values["id"];
            ViewBag.Message = id;
            return View("Index");
        }

        //
        // GET: /Data/QueryInformation

        public ActionResult QueryInformation(string message)
        {
            //ViewBag.Message = Request.QueryString["message"];
            ViewBag.Message = message;
            return View("Index");
        }


        /*
        Платформа предоставит значения для параметров, проверив соответствующие объекты контекста,
        включая:
            Request.Form, 
            RouteData.Values, 
            Request.QueryString
        */

        // GET: /_004_ReceiveSimpleTypes/SimpleType/5
        // GET: /_004_ReceiveSimpleTypes/SimpleType?id=5
        public ActionResult SimpleType(int id)
        {
            ViewBag.Message = "id = " + id;
            return View("Index");
        }

        //can use only variable name like in routes with Html.ActionLink
        // GET: /_004_ReceiveSimpleTypes/SimpleType?ff=5
        public ActionResult SimpleType2(int ff)
        {
            ViewBag.Message = "ff = " + ff;
            return View("Index");
        }

        // GET: /_004_ReceiveSimpleTypes/SimpleForm
        public ActionResult SimpleForm()
        {
            string x = Request.Params["x"];
            string y = Request.Params["y"];
            if (!string.IsNullOrEmpty(x) && !string.IsNullOrEmpty(y))
            {
                ViewBag.Result = x + y;
            }
            return View("SimpleForm");
        }

        // POST: /_004_ReceiveSimpleTypes/SimpleForm
        [HttpPost] // метод будет вызываться только при POST запросах
        public ActionResult SimpleForm(int x, int y)
        {
            ViewBag.Result = x + y;

            return View("SimpleForm");
        }

    }
}
