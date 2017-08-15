using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._004_Routing.Controllers
{
    public class _02_LinkController : Controller
    {
        public ActionResult _01_BaseInfo()
        {
            return View();
        }

        public ActionResult _02_GenerateLink()
        {
            return View();
        }

        public ActionResult _02_ActionWithoutParameters()
        {
            return View();
        }

        public ActionResult _02_ActionWithParameters(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult _03_GenerateUrl()
        {
            return View();
        }

        public ActionResult _04_Areas()
        {
            return View();
        }

    }
}
