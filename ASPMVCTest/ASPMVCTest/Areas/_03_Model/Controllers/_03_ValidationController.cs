using System.Text.RegularExpressions;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._03_Model.Models;

namespace _01_ASPMVCTest.Areas._03_Model.Controllers
{
    public class _03_ValidationController : Controller
    {
        //
        // GET: /_03_Model/_03_Validation/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult M_01_BaseInfo()
        {
            return View();
        }

        public ActionResult M_02_ExplicitDataValidation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult M_02_ExplicitDataValidation(M_03_TestModel model)
        {
            if (string.IsNullOrEmpty(model.Login))
            {
                ModelState.AddModelError("Login", "Введите логин");
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("Password", "Введите пароль");
            }
            if (string.Compare(model.Password, model.PasswordConfirm) != 0)
            {
                ModelState.AddModelError("PasswordConfirm", "Пароли не совпадают");
            }
            if (model.Email != null && !new Regex(@"\b[a-z0-9._]+@[a-z0-9.-]+\.[a-z]{2,4}\b").IsMatch(model.Email))
            {
                ModelState.AddModelError("Email", "Email не правильный");
            }

            if (ModelState.IsValid)
            {
                // произвести сохранение модели
                return View("Success");
            }
            else
            {
                return View();
            }
        }

        public ActionResult M_03_ModelLevelError()
        {
            return View();
        }

        [HttpPost]
        public ActionResult M_03_ModelLevelError(M_03_TestModel model)
        {
            if (string.IsNullOrEmpty(model.Login))
            {
                ModelState.AddModelError("Login", "Введите логин");
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("Password", "Введите пароль");
            }
            if (string.Compare(model.Password, model.PasswordConfirm) != 0)
            {
                ModelState.AddModelError("PasswordConfirm", "Пароли не совпадают");
            }
            if (model.Email != null && !new Regex(@"\b[a-z0-9._]+@[a-z0-9.-]+\.[a-z]{2,4}\b").IsMatch(model.Email))
            {
                ModelState.AddModelError("Email", "Email не правильный");
            }

            if (model.Login == model.Password)
            {
                // Добавление ошибки уровня модели.
                ModelState.AddModelError("", "Пароль не может совпадать с логином");
            }

            if (ModelState.IsValid)
            {
                // произвести сохранение модели
                return View("Success");
            }
            else
            {
                return View();
            }
        }

        public ActionResult M_04_ValidationWithMetadata()
        {
            return View();
        }

        [HttpPost]
        public ActionResult M_04_ValidationWithMetadata(M_03_ModelWithValidationMetadata model)
        {
            if (ModelState.IsValid)
            {
                return View("Success");
            }

            return View();
        }

        public ActionResult M_05_CustomValidationAttribute()
        {
            return View();
        }

        [HttpPost]
        public ActionResult M_05_CustomValidationAttribute(M_03_ModelWithCustomValidationAttribute model)
        {
            if (ModelState.IsValid)
            {
                return View("Success");
            }

            return View();
        }

        public ActionResult M_06_ClientSideValidation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult M_06_ClientSideValidation(M_03_ModelWithValidationMetadata model)
        {
            if (ModelState.IsValid)
            {
                return View("Success");
            }

            return View();
        }
    }
}
