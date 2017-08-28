using System;
using System.Threading;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._05_Filters.Filters;

namespace _01_ASPMVCTest.Areas._05_Filters.Controllers
{
    public class _01_HomeController : Controller
    {
        //
        // GET: /_005_Filters/_001_Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult F_01_BaseInfo()
        {
            return View();
        }

        public ActionResult F_02_ExceptionFilter()
        {
            return View();
        }

        // Для работы фильтра HandleError в файле web.config нужно добавить <customErrors mode="On"></customErrors>
        // В случае возникновения исключения типа DivideByZeroException будет возвращено представление DivideError
        [HandleError(ExceptionType = typeof(DivideByZeroException), View = "F_02_DivideError")]
        public ActionResult F_02_ExceptionFilterTest()
        {
            int a = 10, b = 0;
            int result = a / b;

            return Content(result.ToString());
        }

        // Атрибут не дает запустить метод действия и возвращает ошибку 404 
        // если в cookie наборах запроса нет записи с именем test
        [F_03_ActionFilter]
        public ActionResult F_03_ActionFilter()
        {
            return View();
        }

        public ActionResult F_04_ResultFilter()
        {
            return View();
        }

        [Profile]
        public ActionResult F_04_SlowAction()
        {
            Thread.Sleep(2000);
            return View();
        }

        [Profile]
        public ActionResult F_04_SlowView()
        {
            return View();
        }

        [F_05_Message(Message = "A", Order = 2)]
        [F_05_Message(Message = "B", Order = 1)]
        public ActionResult F_05_FiltersOrder()
        {
            return View();
        }

        public ActionResult F_06_GlobalFilters()
        {
            return View();
        }

        public ActionResult F_06_GlobalFiltersTest()
        {
            throw new Exception();
        }

        // OutputCache – указывает, что ответ нужно кэшировать на время указанное в параметре Duration
        [OutputCache(Duration = 10)]
        public ActionResult F_07_OutputCache()
        {
            ViewBag.Message = "Операция выполнена в " + DateTime.Now.ToLongTimeString();
            return View();
        }
    }
}
