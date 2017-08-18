using System;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._01_Controller.Controllers
{
    public class _011_HttpCodesResultController : Controller
    {
        //
        // GET: /_01_Controller/_011_HttpCodes/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ActionHttpNotFound()
        {
            // Возвращение статуса 404 NotFound
            return HttpNotFound();
        }

        public ActionResult ActionServerError()
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
