using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._20_Architectures.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /_20_Architectures/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
