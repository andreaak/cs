using System.Collections.Generic;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._000_Base.Models;

namespace _01_ASPMVCTest.Areas._000_Base.Controllers
{
    public class _001_HomeController : Controller
    {
        /*
        Для запуска данного метода следует сделать запрос по адресу 
        'http://localhost:port/Home/Index' или 'http://localhost:port/Home/'
        Также данный метод будет запущен при выполнении запроса 'http://localhost:port/', 
        так как путь /Home/Index настроен как путь по умолчанию 
        в файле /App_Start/RouteConfig.cs
        */
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _01_BaseInfo()
        {
            return View();
        }

        public ActionResult _02_Products()
        {
            List<Product> products = new List<Product>();

            products.Add(new Product()
            {
                ProductId = 1,
                Name = "Шариковая ручка",
                Description = "Синяя шариковая ручка с колпачком и прозрачным корпусом.",
                Price = 3m,
                Category = "Канцтовары"
            });

            products.Add(new Product()
            {
                ProductId = 2,
                Name = "Бумага A4",
                Description = "Стандартная бумага для цветной и чёрно-белой печати.",
                Price = 15m,
                Category = "Канцтовары"
            });

            products.Add(new Product()
            {
                ProductId = 2,
                Name = "Мобильный телефон",
                Description = "Мобильный телефон с фотокамерой.",
                Price = 250m,
                Category = "Техника"
            });

            /*
            Возвращаем представление из директории Views/_001_Home/_02_Products.cshtml
            Параметр передающийся в метод View() является моделью, 
            которая будет доступна только на чтение в представлении Index
            */
            return View(products);
        }
    }
}
