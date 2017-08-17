using System.Collections.Generic;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._02_View.Models;

namespace _01_ASPMVCTest.Areas._02_View.Controllers
{
    public class _01_HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult V_01_BaseInfo()
        {
            return View();
        }

        public ActionResult V_02_StronglyTypedView()
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
            Возвращаем представление из директории Views/_01_Home/V_02_StronglyTypedView.cshtml
            Параметр передающийся в метод View() является моделью, 
            которая будет доступна только на чтение в представлении Index
            */
            return View(products);
        }
    }
}
