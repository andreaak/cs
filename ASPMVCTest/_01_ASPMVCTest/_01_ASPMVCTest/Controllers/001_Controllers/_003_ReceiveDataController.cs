using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Controllers._001_Controllers
{
    public class _003_ReceiveDataController : Controller
    {
        //
        // GET: /_003_ReceiveData/GetDataFromContextObjects
        public ActionResult GetDataFromContextObjects()
        {
            // Свойства контроллера для доступа к информации о запросе.
            // Request - данные о текущем HTTP запросе.
            // Response - данные о текущем HTTP ответе.
            // RouteData - данные маршрутизации для текущего запроса.
            // HttpContext - получение специфической информации о текущем HTTP запросе.
            // Server - объект с методами для обработки HTTP запроса.

            string userName = User.Identity.Name;
            string machineName = Server.MachineName;
            string clientIp = Request.UserHostAddress;

            string formData = Request.Form["data"];
            string queryStringData = Request.QueryString["data"];
            HttpCookie cookie = Request.Cookies["cookieName"];

            return View("001_Controller/_003_ReceiveData/GetDataFromContextObjects");
        }

        // GET: /_003_ReceiveData/GetDataFromRequest
        public ActionResult GetDataFromRequest()
        {
            return View("001_Controller/_003_ReceiveData/GetDataFromRequest");
        }

        // POST: /_003_ReceiveData/PostInformation/
        public ActionResult PostInformation()
        {
            // Чтение данных, которые передаются с помощью POST запроса.
            ViewBag.Title = "Post Information";
            ViewBag.Message = Request.Form["message"];
            return View("Index");
        }

        // GET: /_003_ReceiveData/RouteInformation/
        public ActionResult RouteInformation()
        {
            // Чтение данных, которые передаются с помощью GET запроса, как данные в маршруте.
            ViewBag.Title = "Route Information";
            ViewBag.Message = RouteData.Values["id"];
            return View("Index");
        }

        // GET: /_003_ReceiveData/QueryInformation/
        public ActionResult QueryInformation()
        {
            // Чтение данных, которые передаются в адресной строке.
            ViewBag.Title = "Query Information";
            ViewBag.Message = Request.QueryString["message"];
            return View("Index");
        }

        // Платформа предоставит значения для параметров, проверив соответствующие объекты контекста,
        // включая Request.Form, RouteData.Values, Request.QueryString

        // GET: /_003_ReceiveData/GetDataFromRequest
        public ActionResult GetDataFromRequestSimpleType()
        {
            return View("001_Controller/_003_ReceiveData/GetDataFromRequestSimpleType");
        }

        // POST: /_003_ReceiveData/PostInformationSimpleType
        public ActionResult PostInformationSimpleType(string message)
        {
            //ViewBag.Message = Request.Form["message"];
            ViewBag.Title = "PostInformationSimpleType";
            ViewBag.Message = message;
            return View("Index");
        }

        // GET: /_003_ReceiveData/RouteInformationSimpleType
        public ActionResult RouteInformationSimpleType(string id)
        {
            //ViewBag.Message = RouteData.Values["id"];
            ViewBag.Title = "RouteInformationSimpleType";
            ViewBag.Message = id;
            return View("Index");
        }

        // GET: /_003_ReceiveData/QueryInformationSimpleType
        public ActionResult QueryInformationSimpleType(string message)
        {
            //ViewBag.Message = Request.QueryString["message"];
            ViewBag.Title = "QueryInformationSimpleType";
            ViewBag.Message = message;
            return View("Index");
        }

        // GET: /_003_ReceiveData/SimpleType/5
        // GET: /_003_ReceiveData/SimpleType?id=5
        public ActionResult SimpleType(int id)
        {
            ViewBag.Message = "id = " + id;
            return View("Index");
        }
        //can use only variable name like in routes with Html.ActionLink
        // GET: /_003_ReceiveData/SimpleType?ff=5
        public ActionResult SimpleType2(int ff)
        {
            ViewBag.Message = "ff = " + ff;
            return View("Index");
        }

        // GET: /_003_ReceiveData/SimpleForm
        public ActionResult SimpleForm()
        {
            string x = Request.Params["x"];
            string y = Request.Params["y"];
            if (!string.IsNullOrEmpty(x) && !string.IsNullOrEmpty(y))
            {
                ViewBag.Result = x + y;
            }
            return View("001_Controller/_003_ReceiveData/SimpleForm");
        }

        // POST: /_003_ReceiveData/SimpleForm
        [HttpPost] // метод будет вызываться только при POST запросах
        public ActionResult SimpleForm(int x, int y)
        {
            ViewBag.Result = x + y;

            return View("001_Controller/_003_ReceiveData/SimpleForm");
        }
    }
}
