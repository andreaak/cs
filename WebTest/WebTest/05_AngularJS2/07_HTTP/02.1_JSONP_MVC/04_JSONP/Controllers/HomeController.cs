using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _04_JSONP.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/GetDataJsonp
        public ActionResult GetDataJsonp()
        {
            var data = new
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Age = 30
            };

            return new JsonpResult(data);
        }

        // GET: Home/GetData
        public ActionResult GetData()
        {
            var data = new
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Age = 30
            };
            
            return Json(data);
        }
    }
}