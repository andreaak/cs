using System.Text;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._01_Controller.Controllers
{
    public class _08_ContentResultController : Controller
    {
        //
        // GET: /_01_Controller/_08_ContentResult/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult C_01_BaseInfo()
        {
            return View();
        }

        public ActionResult C_02_Content()
        {
            return Content("Hello World", "text/plain", Encoding.UTF8);
        }

        public ActionResult C_03_EmptyResult()
        {
            // Отправка пустого ответа.
            return new EmptyResult();
        }

    }
}
