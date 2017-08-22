using System.Collections.Generic;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._02_View.Models;

namespace _01_ASPMVCTest.Areas._02_View.Controllers
{
    public class _05_HtmlHelperSelectMethodsController : Controller
    {
        //
        // GET: /_02_View/_04_SelectElements/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult V_01_BaseInfo()
        {
            return View();
        }

        public ActionResult V_02_SelectElements()
        {
            return View();
        }

        [HttpPost]
        public ActionResult V_02_SelectElements(string list1, string[] list2)
        {
            ViewBag.Selected1 = list1;

            string result = string.Empty;
            if (list2 != null)
            {
                foreach (var item in list2)
                {
                    result += item + ", ";
                }
                ViewBag.Selected2 = result.TrimEnd(',', ' ');
            }
            return View();
        }

        public ActionResult V_03_DropDownList()
        {
            List<Product> products = new List<Product>()
            {
                new Product() { ProductId = 1, Name ="Product #1"},
                new Product() { ProductId = 2, Name ="Product #2"},
                new Product() { ProductId = 3, Name ="Product #3"},
                new Product() { ProductId = 4, Name ="Product #4"}
            };

            ViewBag.Products = new SelectList(products, "ProductId", "Name", 2);

            return View();
        }

        [HttpPost]
        public ActionResult V_03_DropDownList(string Products)
        {
            return View("Selected", (object)Products);
        }

        public ActionResult V_04_ListBox()
        {
            List<Product> products = new List<Product>()
            {
                new Product() { ProductId = 1, Name ="Product #1"},
                new Product() { ProductId = 2, Name ="Product #2"},
                new Product() { ProductId = 3, Name ="Product #3"},
                new Product() { ProductId = 4, Name ="Product #4"}
            };

            ViewBag.Products = new SelectList(products, "ProductId", "Name", 2);

            return View();
        }

        [HttpPost]
        public ActionResult V_04_ListBox(string[] Products)
        {
            return View("Selected", (object)Products);
        }
    }
}
