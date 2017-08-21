using System.Web.Mvc;
using _01_ASPMVCTest.Areas._02_View.Models;

namespace _01_ASPMVCTest.Areas._02_View.Controllers
{
    public class _10_WebGridController : Controller
    {
        //
        // GET: /_02_View/_003_WebGrid/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BaseFunctionality()
        {
            return View(BrowsersDatabase.Browsers);
        }

        public ActionResult WebGridStyles()
        {
            return View(BrowsersDatabase.Browsers);
        }

        public ActionResult WebGridColumns()
        {
            return View(BrowsersDatabase.Browsers);
        }
    }
}
