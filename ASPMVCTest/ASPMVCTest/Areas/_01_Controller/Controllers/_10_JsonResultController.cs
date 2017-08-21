using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._01_Controller.Controllers
{
    public class _10_JsonResultController : Controller
    {
        //
        // GET: /_01_Controller/_10_JsonResult/

        public ActionResult Index()
        {
            return View();
        }

        // атрибут устанавливает ограничение на метод Action1 
        // и позволяет выполнять его только при GET запросе
        [HttpGet] 
        public ActionResult GetAction()
        {
            var product = new
            {
                Name = "Item 1",
                Price = 10,
                Description = "Dummy Text ....",
            };

            // По умолчанию MVC Framework не будет возвращать JSON в ответ на GET запрос по причнинам безопасности.
            // Возвращать в ответ на GET запрос JSON безопасно если в ответе не содержатся чувствительные данные.
            // http://haacked.com/archive/2009/06/25/json-hijacking.aspx/

            return Json(product, JsonRequestBehavior.AllowGet);
        }

        // атрибут устанавливает ограничение на метод Action2 
        // и позволяет выполнять его только при POST запросе
        [HttpPost] 
        public ActionResult PostAction()
        {
            var product = new
            {
                Name = "Item 1",
                Price = 10,
                Description = "Dummy Text ....",
            };

            return Json(product);
        }
    }
}
