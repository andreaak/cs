using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._06_Security.Controllers
{
    public class _01_HomeController : Controller
    {
        //
        // GET: /_06_Security/_01_Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult S_01_BaseInfo()
        {
            return View();
        }


        public ActionResult S_04_XSS_BaseInfo()
        {
            return View();
        }

    }
}
