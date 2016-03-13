using _01_ASPMVCTest.Areas._003_Model.Models;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._003_Model.Controllers
{
    public class _003_ValidationController : Controller
    {
        //
        // GET: /_003_Model/_003_Validation/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExplicitDataValidation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ExplicitDataValidation(AccountModel model)
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

        public ActionResult ModelLevelError()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ModelLevelError(AccountModel model)
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

        public ActionResult MetadataValidation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MetadataValidation(AccountModelMetadata model)
        {
            if (ModelState.IsValid)
            {
                return View("Success");
            }

            return View();
        }

        public ActionResult CustomValidationAttribute()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CustomValidationAttribute(AccountModelMetadata2 model)
        {
            if (ModelState.IsValid)
            {
                return View("Success");
            }

            return View();
        }

        public ActionResult ClientSideValidation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClientSideValidation(AccountModelMetadata model)
        {
            if (ModelState.IsValid)
            {
                return View("Success");
            }

            return View();
        }
    }
}
