using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._02_View.Controllers
{
    public class _06_LayoutsController : Controller
    {
        //
        // GET: /_02_View/_06_Layouts/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult V_01_BaseInfo()
        {
            return View();
        }

        public ActionResult V_02_Layouts()
        {
            return View();
        }

        public ActionResult V_03_LayoutWithoutSomeSections()
        {
            return View();
        }

        public ActionResult V_04_Bundling()
        {
            return View();
        }
    }
}
