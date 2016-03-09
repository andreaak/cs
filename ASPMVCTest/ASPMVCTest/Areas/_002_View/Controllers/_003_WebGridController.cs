using System.Web.Mvc;
using _01_ASPMVCTest.Areas._002_View.Models;

namespace _01_ASPMVCTest.Areas._002_View.Controllers
{
    public class _003_WebGridController : Controller
    {
        //
        // GET: /_002_View/_003_WebGrid/

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
