using System;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._01_Controller.Controllers
{
    public class _11_HttpCodesResultController : Controller
    {
        //
        // GET: /_01_Controller/_11_HttpCodes/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult C_02_HttpNotFound()
        {
            // Возвращение статуса 404 NotFound
            return HttpNotFound();
        }

        public ActionResult C_03_ServerError()
        {
            int result;
            try
            {
                int a = 10, b = 0;
                result = a / b;
            }
            catch (Exception)
            {
                // Возвращение статуса 500 Internal Server Error
                return new HttpStatusCodeResult(500, "Internal Server Error");
            }

            return Content(result.ToString());
        }
    }
}
