using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._02_View.Models;

namespace _01_ASPMVCTest.Areas._02_View.Controllers
{
    public class _003_HtmlFormsElementsController : Controller
    {
        //
        // GET: /_02_View/_003_HtmlFormsElements/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HtmlElements()
        {
            return View();
        }

        // Возвращает форму для создания новой задачи
        // GET: /_02_View/_003_HtmlForms/Create

        public ActionResult Create()
        {
            TaskModel model = new TaskModel();
            model.Name = "Unknown";
            model.StartDate = DateTime.Now.ToShortDateString();
            model.Completed = false;

            return View(model);
        }

        // Получает данные переданные через форму и создает задачу
        // POST: /_02_View/_003_HtmlForms/Create

        [HttpPost]
        public ActionResult Create(TaskModel incomingData)
        {
            Debug.WriteLine("Name = " + incomingData.Name);
            Debug.WriteLine("StartDate = " + incomingData.StartDate);
            Debug.WriteLine("Completed = " + incomingData.Completed);

            return View("Success");
        }

        public ActionResult Select()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Select(string list1, string[] list2)
        {
            ViewBag.Selected1 = list1;

            string result = string.Empty;
            foreach (var item in list2)
            {
                result += item + ", ";
            }
            ViewBag.Selected2 = result.TrimEnd(',', ' ');

            return View();
        }

        public ActionResult SampleSelect()
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
        public ActionResult SampleSelect(string Products)
        {
            return View("Selected", (object)Products);
        }

        // Возвращает форму для создания новой задачи
        // GET: /_02_View/_003_HtmlForms/Create

        public ActionResult StronglyTypedCreate()
        {
            TaskModel model = new TaskModel();
            model.Name = "Unknown";
            model.StartDate = DateTime.Now.ToShortDateString();
            model.Completed = false;

            return View(model);
        }

        // Получает данные переданные через форму и создает задачу
        // POST: /_02_View/_003_HtmlForms/Create

        [HttpPost]
        public ActionResult StronglyTypedCreate(TaskModel incomingData)
        {
            Debug.WriteLine("Name = " + incomingData.Name);
            Debug.WriteLine("StartDate = " + incomingData.StartDate);
            Debug.WriteLine("Completed = " + incomingData.Completed);

            return View("Success");
        }

    }
}
