using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._05_Filters.Filters
{
    // Атрибут, который реализует интерфейс IActionFilter, используется для декорирования действий в контроллере.
    public class F_03_ActionFilter : FilterAttribute, IActionFilter
    {
        // Метод запускается до выполнения кода действия.
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Проверка наличия значения в cookies
            if (filterContext.HttpContext.Request.Cookies["test"] != null)
            {
                // Метод действия не выполняется, если в свойство Result записать значение отличное от null.
                filterContext.Result = new HttpNotFoundResult();
            }
        }

        // Метод запускается после выполнения кода действия.
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}
