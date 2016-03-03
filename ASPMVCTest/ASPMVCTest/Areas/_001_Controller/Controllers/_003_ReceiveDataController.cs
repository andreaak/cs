using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._001_Controller.Controllers
{
    public class _003_ReceiveDataController : Controller
    {
        //
        // GET: /_001_Controller/_003_ReceiveData/
        public ActionResult Index()
        {
            // Чтение данных, которые передаются с помощью GET запроса, как данные в маршруте.
            ViewBag.Title = "Route Information";
            ViewBag.Message = RouteData.Values["id"];
            return View();
        }


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

            return View();
        }

        // GET: /_003_ReceiveData/GetDataFromRequest
        public ActionResult GetDataFromRequest()
        {
            return View();
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

    }
}
