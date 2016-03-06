using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._001_Controller.Controllers
{
    public class _005_FromControllerToViewController : Controller
    {
        //
        // GET: /_001_Controller/_005_FromControllerToView/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UseModel()
        {
            return View();
        }

        public ActionResult UseViewData()
        {
            return View();
        }
        
        public ActionResult UseViewBag()
        {
            return View();
        }

        public ActionResult UseTempData()
        {
            return View();
        }
    }
}
