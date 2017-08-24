using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._03_Model.Models;

namespace _01_ASPMVCTest.Areas._03_Model.Controllers
{
    public class _02_ModelBindingController : Controller
    {
        //
        // GET: /_003_Model/_002_ModelBinding/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult M_01_BaseInfo()
        {
            return View();
        }

        public ActionResult M_02_SimpleTypeBinding()
        {
            return View();
        }

        public ActionResult M_02_Action(int id)
        {
            ViewBag.Message = "Метод действия Action1(int id)";
            ViewBag.Data = id;

            return View("DisplayResult");
        }

        public ActionResult M_02_ActionWithDefaultValue(int id = 1)
        {
            ViewBag.Message = "Метод действия Action2(int id = 1). При отсутствии значения для параметра id будет использоваться значение 1";
            ViewBag.Data = id;

            return View("DisplayResult");
        }

        public ActionResult M_02_ActionWithNullableParameter(int? id)
        {
            ViewBag.Message = "Метод действия Action3(int? id). При отсутствии значения для параметра id будет использоваться значение null";
            ViewBag.Data = id;

            return View("DisplayResult");
        }

        public ActionResult M_03_ComplexTypeBinding()
        {
            return View();
        }

        /*
        В данном случае связыватель модели с помощью рефлексии 
        получает список всех открытых свойств модели (класса M_02_TestModel)
        И осуществляет поиск и установку значений для каждого свойства по очереди, 
        используя стандартный приоритет поиска данных в запросе.
        */
        [HttpPost]//Если убрать то при запросе система не распознает какой Action выбирать
        public ActionResult M_03_ComplexTypeBinding(M_02_TestModel model)
        {
            ViewBag.Message += "Prop1 = " + model.Prop1 + Environment.NewLine;
            ViewBag.Message += "Prop2 = " + model.Prop2 + Environment.NewLine;
            ViewBag.Message += "Prop3 = " + model.Prop3 + Environment.NewLine;

            return View("DisplayResult");
        }

        public ActionResult M_04_BindingWithPrefix()
        {
            return View();
        }

        [HttpPost]
        public ActionResult M_04_BindingWithPrefix(M_02_TestModel model1, M_02_TestModel model2)
        {
            ViewBag.Message += "model1.Prop1 = " + model1.Prop1 + Environment.NewLine;
            ViewBag.Message += "model1.Prop2 = " + model1.Prop2 + Environment.NewLine;
            ViewBag.Message += "model2.Prop1 = " + model2.Prop1 + Environment.NewLine;
            ViewBag.Message += "model2.Prop2 = " + model2.Prop2 + Environment.NewLine;

            return View("DisplayResult");
        }

        public ActionResult M_05_ArrayBinding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult M_05_ArrayBinding(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                ViewBag.Message += (numbers[i] + Environment.NewLine);
            }

            return View("DisplayResult");
        }

        public ActionResult M_06_ManualBinding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult M_06_ManualBindingPost()
        {
            try
            {
                M_02_TestModel model = new M_02_TestModel();
                UpdateModel(model);

                ViewBag.Message = "Prop1 = " + model.Prop1 + Environment.NewLine
                                + "Prop2 = " + model.Prop2;
            }
            catch
            {
                ViewBag.Message = "ERROR!";
            }

            return View("DisplayResult");
        }

        public ActionResult M_07_FileUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult M_07_FileUpload(HttpPostedFileBase file)
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

            return View("DisplayResult");
        }

        public ActionResult M_08_CustomValueProvider()
        {
            return View();
        }

        [HttpPost]
        public ActionResult M_08_CustomValueProvider(string data)
        {
            // Данные для параметра data будут получены из cookie набора с именем data
            ViewBag.Data = "Данные прочитаные из cookie " + data;
            return View("DisplayResult");
        }
        
        public ActionResult M_09_CustomModelBinder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult M_09_CustomModelBinder(M_02_TestModelSuccessor model)
        {
            ViewBag.Message = "Prop1 = " + model.Prop1 + Environment.NewLine
                                + "Prop2 = " + model.Prop2;
            return View("DisplayResult");
        }
    }
}
