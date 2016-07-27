using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._002_View.Controllers
{
    public class _002_HtmlHelperController : Controller
    {
        //
        // GET: /_002_View/_002_HtmlHelper/

        public ActionResult Index()
        {
            ViewBag.Days = new string[] { "Monday", "Tuesday", "Wednesday" };
            ViewBag.Fruits = new string[] { "Mango", "Banana", "Apple" };

            return View();
        }

        public ActionResult HtmlHelperMethod()
        {
            ViewBag.Days = new string[] { "Monday", "Tuesday", "Wednesday" };
            ViewBag.Fruits = new string[] { "Mango", "Banana", "Apple" };

            return View();
        }
        
        public ActionResult ExstensionHelperMethod()
        {
            ViewBag.Days = new string[] { "Monday", "Tuesday", "Wednesday" };
            ViewBag.Fruits = new string[] { "Mango", "Banana", "Apple" };

            return View();
        }
    }
}
