using _01_ASPMVCTest.Areas._06_Security.Models;
using Microsoft.Security.Application;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._06_Security.Controllers
{
    public class _01_HomeController : Controller
    {
        //
        // GET: /_06_Security/_01_Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult S_01_Membership_BaseInfo()
        {
            return View();
        }

        public ActionResult S_04_XSS_BaseInfo()
        {
            return View();
        }

        public ActionResult S_04_XSS_Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] // отключение валидации данных полученных от клиента.
        public ActionResult S_04_XSS_Create(BulletinBoardItem bulletinboarditem)
        {
            if (ModelState.IsValid)
            {
                // Проверка данных с помощью библиотеки AntiXSS
                // Для установки библиотеки через Package Manager Console исопльзуется команда  Install-Package AntiXSS

                // Убрать комментарий для включения проверки CSS
                //bulletinboarditem.Title = Sanitizer.GetSafeHtmlFragment(bulletinboarditem.Title);
                //bulletinboarditem.Message = Sanitizer.GetSafeHtmlFragment(bulletinboarditem.Message);

                BulletinBoardRepository.Items.Add(bulletinboarditem);

                return RedirectToAction("S_04_XSS_Index");
            }

            return View(bulletinboarditem);
        }

        public ActionResult S_04_XSS_Index()
        {
            return View(BulletinBoardRepository.Items);
        }

        public ActionResult S_05_SQL_Injection_BaseInfo()
        {
            return View();
        }

        public ActionResult S_06_CSRF_BaseInfo()
        {
            return View();
        }

        public ActionResult S_07_ControllerRecommendation()
        {
            return View();
        }
    }
}
