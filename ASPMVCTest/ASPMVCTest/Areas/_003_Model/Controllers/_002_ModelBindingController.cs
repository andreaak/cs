using _01_ASPMVCTest.Areas._003_Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._003_Model.Controllers
{
    public class _002_ModelBindingController : Controller
    {
        //
        // GET: /_003_Model/_002_ModelBinding/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SimpleTypeBinding()
        {
            return View();
        }

        // Связыватель модели - объект который производит поиск значений в запросе в соответствии с именами параметров метода действия.
        // Связыватель модели по умолчанию производит поиск значений в следующем порядке:
        // 1) Request.Form
        // 2) RouteData.Values
        // 3) Request.QueryString
        // 4) Request.Files

        public ActionResult Action1(int id)
        {
            ViewBag.Message = "Метод действия Action1(int id)";
            ViewBag.Data = id;

            return View("ActionView");
        }

        public ActionResult Action2(int id = 1)
        {
            ViewBag.Message = "Метод действия Action2(int id = 1). При отсутствии значения для параметра id будет использоваться значение 1";
            ViewBag.Data = id;

            return View("ActionView");
        }

        public ActionResult Action3(int? id)
        {
            ViewBag.Message = "Метод действия Action3(int? id). При отсутствии значения для параметра id будет использоваться значение null";
            ViewBag.Data = id;

            return View("ActionView");
        }

        public ActionResult ComplexTypeBinding()
        {
            return View();
        }

        // В данном случае связыватель модели с помощью рефлексии получает список всех открытых свойств модели (класса MyModel)
        // И осуществляет поиск и установку значений для каждого свойства по очереди, используя стандартный приоритет поиска данных в запросе.
        [HttpPost]
        public ActionResult ComplexTypeBinding(MyModel model)
        {
            ViewBag.Message += "Prop1 = " + model.Prop1 + Environment.NewLine;
            ViewBag.Message += "Prop2 = " + model.Prop2 + Environment.NewLine;
            ViewBag.Message += "Prop3 = " + model.Prop3 + Environment.NewLine;

            return View("Result");
        }

        public ActionResult BindingWithPrefix()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BindingWithPrefix(MyModel model1, MyModel model2)
        {
            ViewBag.Message += "model1.Prop1 = " + model1.Prop1 + Environment.NewLine;
            ViewBag.Message += "model1.Prop2 = " + model1.Prop2 + Environment.NewLine;
            ViewBag.Message += "model2.Prop1 = " + model2.Prop1 + Environment.NewLine;
            ViewBag.Message += "model2.Prop2 = " + model2.Prop2 + Environment.NewLine;

            return View("Result");
        }

        public ActionResult ArrayBinding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ArrayBinding(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                ViewBag.Message += (numbers[i] + Environment.NewLine);
            }

            return View("Result");
        }

        public ActionResult ManualBinding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetData()
        {
            try
            {
                MyModel model = new MyModel();
                UpdateModel(model);

                ViewBag.Message = "Prop1 = " + model.Prop1 + Environment.NewLine
                                + "Prop2 = " + model.Prop2;
            }
            catch
            {
                ViewBag.Message = "ERROR!";
            }

            return View("Result");
        }

        public ActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            fileName += extension;

            List<string> extensions = new List<string>() { ".txt", ".png", ".jpg", ".pdf", ".zip" };
            if (extensions.Contains(extension))
            {
                file.SaveAs(Server.MapPath("/Uploads/" + fileName));
                ViewBag.Message = "Файл сохранен";
            }
            else
            {
                ViewBag.Message = "Ошибка. Допустимые расширения файлов - '.txt', '.png', '.jpg', '.pdf', '.zip'";
            }

            return View("Result");
        }

        public ActionResult AdvancedBindingSettings()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdvancedBindingSettings(string data)
        {
            // Данные для параметра data будут получены из cookie набора с именем data
            ViewBag.Data = "Данные прочитаные из cookie " + data;
            return View("Result");
        }
        
        public ActionResult CustomModelBinder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CustomModelBinder(MyModel2 model)
        {
            ViewBag.Message = "Prop1 = " + model.Prop1 + Environment.NewLine
                                + "Prop2 = " + model.Prop2;
            return View("Result");
        }
    }
}
