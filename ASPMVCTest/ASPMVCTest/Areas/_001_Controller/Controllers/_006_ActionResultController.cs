using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._001_Controller.Controllers
{
    public class _006_ActionResultController : Controller
    {
        //
        // GET: /_001_Controller/_006_ActionResult/
        /*
        ActionResult – базовый класс, который представляет результат работы метода действия и используется для создания ответа пользователю.
        Некоторые производные классы ActionResult:
            • ContentResult – класс используется для отправки в ответ текстового содержимого. 
            • EmptyResult – отправка пустого ответа. Сообщение от сервера будет содержать заголовки но тело будет пустым. 
            • FileResult – отправка в ответ содержимого файла размещенного на сервере. 
                · FilePathResult
                · FileStreamResult
                · FileContentResult
            • JsonResult – сериализация в JSON и отправка клиенту указанного объекта. 
            • ViewResult – отправка в ответ HTML кода, который получился после визуализации представления 
            • PartialViewResult
            • PartialViewResult
            • RedirectResult
            • RedirectToRouteResult
            • JavascriptResult
            • JsonResult
            • RedirectResult
            • HttpStatusCodeResult
                · HttpUnauthorizedResult
                · HttpNotFoundResult
        */
        public ActionResult Index()
        {
            return View();
        }
    }
}
