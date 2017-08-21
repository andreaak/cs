using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._01_Controller.Controllers
{
    public class _07_RedirectResultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult C_01_BaseInfo()
        {
            return View();
        }

        public ActionResult C_02_Redirect()
        {
            // Переход со статус кодом 302 - временное перенаправление.
            // Использование метода из базового класса Controller
            return Redirect("http://itvdn.com");
        }

        public ActionResult C_03_RedirectResult()
        {
            // Переход со статус кодом 302 - временное перенаправление.
            // Использование экземпляра класса RedirectResult
            return new RedirectResult("http://itvdn.com");
        }

        public ActionResult C_04_RedirectPermanent()
        {
            // Переход со статус кодом 301 - постоянное перенаправление.
            return RedirectPermanent("http://itvdn.com");
            // Второй вариант постоянного перенаправления.
            //return new RedirectResult("http://itvdn.com", true);
        }

        public ActionResult C_05_RedirectToRoute()
        {
            return RedirectToRoute(new
            {
                controller = "_07_TestRedirectResult",
                action = "Index",
                id = "Hello from _05_RedirectToRoute"
            });
        }

        public ActionResult C_06_RedirectToAction()
        {
            return RedirectToAction("Index", "_07_TestRedirectResult", new
            {
                id = "Hello from _06_RedirectToAction"
            });
        }
    }
}
