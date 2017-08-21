using System;
using System.Diagnostics;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._02_View.Models;

namespace _01_ASPMVCTest.Areas._02_View.Controllers
{
    public class _03_HtmlFormsElementsController : Controller
    {
        //
        // GET: /_02_View/_03_HtmlFormsElements/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult V_01_BaseInfo()
        {
            return View();
        }

        public ActionResult V_02_HtmlElements()
        {
            return View();
        }

        public ActionResult V_03_HtmlElementsUseViewBag()
        {
            ViewBag.Name = "Unknown";
            ViewBag.StartDate = DateTime.Now.ToShortDateString();
            ViewBag.Completed = false;

            return View("V_03_HtmlElementsUseXXX");
        }

        [HttpPost]
        public ActionResult V_03_HtmlElementsUseViewBag(TaskModel incomingData)
        {
            Debug.WriteLine("Name = " + incomingData.Name);
            Debug.WriteLine("StartDate = " + incomingData.StartDate);
            Debug.WriteLine("Completed = " + incomingData.Completed);

            return View("Success");
        }

        public ActionResult V_04_HtmlElementsUseViewData()
        {
            ViewData["Name"] = "Unknown";
            ViewData["StartDate"] = DateTime.Now.ToShortDateString();
            ViewData["Completed"] = false;

            return View("V_03_HtmlElementsUseXXX");
        }

        [HttpPost]
        public ActionResult V_04_HtmlElementsUseViewData(TaskModel incomingData)
        {
            Debug.WriteLine("Name = " + incomingData.Name);
            Debug.WriteLine("StartDate = " + incomingData.StartDate);
            Debug.WriteLine("Completed = " + incomingData.Completed);

            return View("Success");
        }

        // Возвращает форму для создания новой задачи
        // GET: /_02_View/_03_HtmlForms/V_02_HtmlElements
        public ActionResult V_05_HtmlElementsUseModel()
        {
            TaskModel model = new TaskModel();
            model.Name = "Unknown";
            model.StartDate = DateTime.Now.ToShortDateString();
            model.Completed = false;

            return View("V_03_HtmlElementsUseXXX");
        }

        // Получает данные переданные через форму и создает задачу
        // POST: /_02_View/_03_HtmlForms/V_02_HtmlElements
        [HttpPost]
        public ActionResult V_05_HtmlElementsUseModel(TaskModel incomingData)
        {
            Debug.WriteLine("Name = " + incomingData.Name);
            Debug.WriteLine("StartDate = " + incomingData.StartDate);
            Debug.WriteLine("Completed = " + incomingData.Completed);

            return View("Success");
        }

        // Возвращает форму для создания новой задачи
        // GET: /_02_View/_03_HtmlForms/V_06_StronglyTypedHtmlElements
        public ActionResult V_06_StronglyTypedHtmlElements()
        {
            TaskModel model = new TaskModel();
            model.Name = "Unknown";
            model.StartDate = DateTime.Now.ToShortDateString();
            model.Completed = false;

            return View(model);
        }

        // Получает данные переданные через форму и создает задачу
        // POST: /_02_View/_03_HtmlForms/V_06_StronglyTypedHtmlElements
        [HttpPost]
        public ActionResult V_06_StronglyTypedHtmlElements(TaskModel incomingData)
        {
            Debug.WriteLine("Name = " + incomingData.Name);
            Debug.WriteLine("StartDate = " + incomingData.StartDate);
            Debug.WriteLine("Completed = " + incomingData.Completed);

            return View("Success");
        }
    }
}
