using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._06_Security.Controllers
{
    public class _03_HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize] // Проверяет наличие аутентификационных cookie значений в запросе к методу
        // Запрещены анонимные обращения к данной странице
        public ActionResult Private()
        {
            ViewBag.Message = "Private Personal Page.";

            return View();
        }

        [Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult AdminPanel()
        {
            ViewBag.Message = "Admin Panel.";

            return View();
        }

        [Authorize(Roles = "Admin, Moderator")] // К данному методу действия могут получать доступ только пользователи с ролью Admin и Moderator
        public ActionResult ModeratorPanel()
        {
            ViewBag.Message = "Moderator Panel.";

            return View();
        }

    }
}
