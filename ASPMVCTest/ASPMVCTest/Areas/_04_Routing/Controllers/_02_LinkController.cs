using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._04_Routing.Controllers
{
    public class _02_LinkController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult A_01_BaseInfo()
        {
            return View();
        }

        public ActionResult A_02_GenerateLink()
        {
            return View();
        }

        public ActionResult A_02_ActionWithoutParameters()
        {
            return View();
        }

        public ActionResult A_02_ActionWithParameters(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult A_03_GenerateUrl()
        {
            return View();
        }
    }
}
