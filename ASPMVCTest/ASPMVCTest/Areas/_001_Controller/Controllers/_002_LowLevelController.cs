using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._001_Controller.Controllers
{
    // В MVC классы контроллеров должны наследоваться от интерфейса IController
    // Для получения доступа к данному контроллеру нужно выполнить запрос /_002_LowLevel/имя_действия
    public class _002_LowLevelController : IController
    {
        // Метод Execute вызывается, когда запрос направляется этому контроллеру.
        public void Execute(System.Web.Routing.RequestContext requestContext)
        {
            string controller = (string)requestContext.RouteData.Values["controller"];
            string action = (string)requestContext.RouteData.Values["action"];

            requestContext.HttpContext.Response.Write(
                string.Format("Controller: {0}, Action: {1}", controller, action));
        }
        //Не вызывается
        //public ActionResult Index2()
        //{
        //    return View();
        //}
    }
}
