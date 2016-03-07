using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._001_Controller.Controllers
{
    public class _007_RedirectResultController : Controller
    {
        //
        // GET: /_001_Controller/_007_RedirectResult/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GoToRedirect()
        {
            // Переход со статус кодом 302 - временное перенаправление.
            // Использование метода из базового класса Controller
            return Redirect("http://itvdn.com");
        }

        public ActionResult GoToRedirectResult()
        {
            // Переход со статус кодом 302 - временное перенаправление.
            // Использование экземпляра класса RedirectResult
            return new RedirectResult("http://itvdn.com");
        }

        public ActionResult GoToRedirectPermanent()
        {
            // Переход со статус кодом 301 - постоянное перенаправление.
            return RedirectPermanent("http://itvdn.com");
            // Второй вариант постоянного перенаправления.
            //return new RedirectResult("http://itvdn.com", true);
        }

        public ActionResult GoToRedirectToRoute()
        {
            return RedirectToRoute(new
            {
                controller = "_007_TestRedirectResult",
                action = "Index",
                id = "Hello from RedirectToRoute"
            });
        }

        public ActionResult GoToRedirectToAction()
        {
            return RedirectToAction("Index", "_007_TestRedirectResult", new
            {
                id = "Hello from RedirectToAction"
            });
        }
    }
}
