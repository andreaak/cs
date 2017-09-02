using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._08_Optimization.Controllers
{
    public class _01_HomeController : Controller
    {
        //
        // GET: /_08_Optimization/_01_Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult O_01_BaseInfo()
        {
            return View();
        }

        public ActionResult NotOptimized()
        {
            ViewBag.Message = "Загрузка НЕ оптимизированной страницы.";

            return View();
        }

        public ActionResult Optimized()
        {
            ViewBag.Message = "Загрузка оптимизированной страницы.";

            return View();
        }
    }
}
