using System.Text;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._001_Controller.Controllers
{
    public class _008_ContentResultController : Controller
    {
        //
        // GET: /_001_Controller/_008_ContentResult/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ActionText()
        {
            // Content - создает ActionResult представляющий строковой контент отправляющийся в ответ пользователю.

            // первый параметр - текстовые данные, которые необходимо отправить.
            // второй параметр - MIME тип возвращаемых данных, по умолчанию text/html.
            // третий параметр - кодировка.
            return Content("Hello World", "text/plain", Encoding.UTF8);
        }

        public ActionResult ActionEmptyResult()
        {
            // Отправка пустого ответа.
            return new EmptyResult();
        }

    }
}
