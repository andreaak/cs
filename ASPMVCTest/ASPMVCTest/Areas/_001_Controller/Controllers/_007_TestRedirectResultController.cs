using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._001_Controller.Controllers
{
    public class _007_TestRedirectResultController : Controller
    {
        //
        // GET: /_001_Controller/_007_TestRedirectResult/

        public ActionResult Index(string id)
        {
            ViewBag.Message = id;
            return View();
        }

    }
}
