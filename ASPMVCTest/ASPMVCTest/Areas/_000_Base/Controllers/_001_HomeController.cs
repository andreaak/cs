using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._000_Base.Controllers
{
    public class _001_HomeController : Controller
    {
        //
        // GET: /_000_Base/_001_Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
