using System.Collections.Generic;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._02_View.Models;

namespace _01_ASPMVCTest.Areas._02_View.Controllers
{
    public class _07_PartialViewsController : Controller
    {
        //
        // GET: /_02_View/_05_PartialViews/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult V_01_BaseInfo()
        {
            return View();
        }

        public ActionResult V_02_PartialViews()
        {
            return View();
        }

        public ActionResult V_03_StronglyTypedPartialViews()
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
    }
}
