using System.Linq;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._07_Ajax.Models;

namespace _01_ASPMVCTest.Areas._07_Ajax.Controllers
{
    public class _01_HomeController : Controller
    {
        //
        // GET: /_07_Ajax/_01_Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult A_01_BaseInfo()
        {
            return View();
        }

        public ActionResult A_02_SimpleAjax()
        {
            return View();
        }

        [HttpPost]
        public ActionResult A_02_SimpleAjax(string id)
        {
            // id - имя клиента, заказы которого необходимо выводить на странице.
            return View((object)id);
        }

        public ActionResult A_02_SimpleAjaxData(string id)
        {
            var data = A_02_OrdersDatabase.Orders;
            if (!string.IsNullOrEmpty(id) && id != "All")
            {
                // выполняем выборку по свойству Customer если значение id не пустое и не равное "All"
                data = data.Where(e => e.Customer == id);
            }
            return PartialView(data);
        }

        public ActionResult A_03_LoadingIndicator()
        {
            return View();
        }

        [HttpPost]
        public ActionResult A_03_LoadingIndicator(string id)
        {
            // id - имя клиента, заказы которого необходимо выводить на странице.
            return View((object)id);
        }

        public ActionResult A_04_AjaxLinks()
        {
            return View();
        }

        [HttpPost]
        public ActionResult A_04_AjaxLinks(string id)
        {
            // id - имя клиента, заказы которого необходимо выводить на странице.
            return View((object)id);
        }

        public ActionResult A_05_AjaxCallbacks()
        {
            return View();
        }

        [HttpPost]
        public ActionResult A_05_AjaxCallbacks(string id)
        {
            // id - имя клиента, заказы которого необходимо выводить на странице.
            return View((object)id);
        }

        public ActionResult A_06_JsonResult()
        {
            return View();
        }

        [HttpPost]
        public ActionResult A_06_JsonResult(string id)
        {
            // id - имя клиента, заказы которого необходимо выводить на странице.
            return View((object)id);
        }

        public ActionResult A_06_JsonOrdersData(string id)
        {
            var data = A_02_OrdersDatabase.Orders;
            if (!string.IsNullOrEmpty(id) && id != "All")
            {
                data = data.Where(e => e.Customer == id);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult A_07_JQuery()
        {
            return View();
        }

        public ActionResult A_07_GetArticle(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return Content("Article " + id);
            }
            return new EmptyResult();
        }

        public ActionResult A_08_JavaScript()
        {
            return View();
        }
    }
}
