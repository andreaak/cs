using System;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._02_View.Models;

namespace _01_ASPMVCTest.Areas._02_View.Controllers
{
    public class _06_HtmlHelperTemplatedMethodsController : Controller
    {
        //
        // GET: /_02_View/06_HtmlHelperTemplatedMethods/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult V_01_BaseInfo()
        {
            return View();
        }

        public ActionResult V_02_TemplatedHelperMethod()
        {
            Product p = new Product()
            {
                ProductId = 1,
                Name = "Test Product",
                IsTangible = false
            };
            return View(p);
        }

        public ActionResult V_03_Editor_Display_ForModel()
        {
            Product p = new Product()
            {
                ProductId = 1,
                Name = "Test Product",
                IsTangible = false
            };
            return View(p);
        }

        public ActionResult V_04_ModelMetadata()
        {
            V_04_ModelWithMetaData customer = new V_04_ModelWithMetaData()
            {
                Id = 1,
                FirstName = "Ivan",
                LastName = "Ivanov",
                IsApproved = true,
                Description = "text... text... text...",
                RegistrationDate = DateTime.Now,
                Foo = 123
            };

            return View(customer);
        }

        public ActionResult V_05_SpecialTemplate()
        {
            V_05_Person person = new V_05_Person();
            person.PersonId = 1;
            person.Name = "Jhon Doe";
            person.Role = Role.User;

            return View(person);
        }
    }
}
