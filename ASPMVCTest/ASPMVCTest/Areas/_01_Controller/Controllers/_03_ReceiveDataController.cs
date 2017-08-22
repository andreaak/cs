using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._01_Controller.Controllers
{
    public class _03_ReceiveDataController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult C_01_BaseInfo()
        {
            string userName = User.Identity.Name;
            string machineName = Server.MachineName;
            string clientIp = Request.UserHostAddress;

            string formData = Request.Form["data"];
            string queryStringData = Request.QueryString["data"];
            HttpCookie cookie = Request.Cookies["cookieName"];

            return View();
        }

        public ActionResult C_03_SendDataToController()
        {
            return View();
        }

        public ActionResult C_04_PostInformation()
        {
            // Чтение данных, которые передаются с помощью POST запроса.
            ViewBag.Title = "Post Information";
            ViewBag.Message = Request.Form["message"];
            return View("DisplayData");
        }

        public ActionResult C_05_RouteInformation()
        {
            /*
            Чтение данных, которые передаются с помощью GET запроса, 
            как данные в маршруте.
            */
            ViewBag.Title = "Route Information";
            ViewBag.Message = RouteData.Values["id"];
            return View("DisplayData");
        }

        public ActionResult C_06_QueryStringInformation()
        {
            // Чтение данных, которые передаются в адресной строке.
            ViewBag.Title = "Query Information";
            ViewBag.Message = Request.QueryString["message"];
            return View("DisplayData");
        }
    }
}
