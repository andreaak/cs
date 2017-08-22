using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._02_View.Models;

namespace _01_ASPMVCTest.Areas._02_View.Controllers
{
    public class _09_ChildActionsController : Controller
    {
        //
        // GET: /_02_View/_08_ChildAction/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult V_01_BaseInfo()
        {
            return View();
        }

        public ActionResult V_02_ChildAction()
        {
            return View();
        }

        [ChildActionOnly] // метод может быть запущен только как дочернее действие
        public string CurrentDate()
        {
            return DateTime.Now.ToShortDateString();
        }

        public ActionResult ShowTable(int numberOfRows = 5)
        {
            IEnumerable<Product> products = ProductCollection.All.Take(numberOfRows);
            return PartialView("_V_02_Table", products);
        }
    }
}
