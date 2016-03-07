using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._002_View.Models;

namespace _01_ASPMVCTest.Areas._002_View.Controllers
{
    public class _001_HomeController : Controller
    {
        //
        // GET: /_002_View/_001_Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BaseInfo()
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

            // Возвращаем представление из директории Views/_001_BasicInfoView/Index.cshtml
            // Параметр передающийся в метод View() является моделью, которая будет доступна только на чтение в представлении Index
            return View(products);
        }

        public ActionResult Layouts()
        {
            return View();
        }

        public ActionResult Bundling()
        {
            return View();
        }

        public ActionResult PartialViews()
        {
            return View();
        }

        public ActionResult StronglyTypedPartialViews()
        {
            List<Product> products = new List<Product>();

            products.Add(new Product()
            {
                ProductId = 1,
                Name = "Item 1",
                Price = 10
            });

            products.Add(new Product()
            {
                ProductId = 2,
                Name = "Item 2",
                Price = 5
            });

            products.Add(new Product()
            {
                ProductId = 3,
                Name = "Item 3",
                Price = 50
            });
            
            return View(products);
        }

        public ActionResult ChildAction()
        {
            return View();
        }

        [ChildActionOnly] // метод может быть запущен только как дочернее действие
        public string CurrentDate()
        {
            return DateTime.Now.ToShortDateString();
        }

        public ActionResult ShowTable(int numberOfRows = 5)
        {
            IEnumerable<Product> products = ProductCollection.All.Take(numberOfRows);
            return PartialView("_Table", products);
        }
    }
}
