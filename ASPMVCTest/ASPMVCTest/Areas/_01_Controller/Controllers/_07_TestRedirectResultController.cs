using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._01_Controller.Controllers
{
    public class _07_TestRedirectResultController : Controller
    {
        //
        // GET: /_01_Controller/_007_TestRedirectResult/

        public ActionResult Index(string id)
        {
            ViewBag.Message = id;
            return View();
        }

    }
}
