using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._02_View.Controllers
{
    public class _02_HtmlHelperController : Controller
    {
        //
        // GET: /_02_View/_002_HtmlHelper/

        public ActionResult Index()
        {
            ViewBag.Days = new string[] { "Monday", "Tuesday", "Wednesday" };
            ViewBag.Fruits = new string[] { "Mango", "Banana", "Apple" };

            return View();
        }

        public ActionResult V_01_BaseInfo()
        {
            return View();
        }

        public ActionResult V_02_EmbeddedHelper()
        {
            ViewBag.Days = new string[] { "Monday", "Tuesday", "Wednesday" };
            ViewBag.Fruits = new string[] { "Mango", "Banana", "Apple" };

            return View();
        }
        
        public ActionResult V_03_ExstensionHelper()
        {
            ViewBag.Days = new string[] { "Monday", "Tuesday", "Wednesday" };
            ViewBag.Fruits = new string[] { "Mango", "Banana", "Apple" };

            return View();
        }
    }
}
