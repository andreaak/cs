using System;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas._02_View.Models;

namespace _01_ASPMVCTest.Areas._02_View.Controllers
{
    public class _004_TemplatedHelperMethodController : Controller
    {
        //
        // GET: /_02_View/_004_TemplatedHelperMethod/

        public ActionResult Index()
        {
            return View();
        }

        /*
        Шаблонизированный вспомогательный метод - обеспечивает большую гибкость при создании представления, 
        позволяя автоматизировать выбор HTML элемента для редактирования определенного свойства модели. 
	        o @Html.EditorFor(model => model.Property) 
	        o @Html.DisplayFor(model => model.Property) 
	        o @Html.LabelFor(model => model.Property) 
	        o @Html.EditorForModel() 
	        o @Html.DisplayForModel() 
        */

        public ActionResult TemplatedHelperMethod()
        {
            Product p = new Product()
            {
                ProductId = 1,
                Name = "Test Product",
                IsTangible = false
            };
            return View(p);
        }

        public ActionResult EditorForModel()
        {
            Product p = new Product()
            {
                ProductId = 1,
                Name = "Test Product",
                IsTangible = false
            };
            return View(p);
        }

        /*
        С помощью атрибутов, добавленных свойствам модели, можно управлять поведением шаблонизированных методов.  
	        o HiddenInput – атрибут указывает, что свойство модели должно отображаться как скрытое поле (<input type=“hidden”>) 
	        o ScaffoldColumn – свойство декорированное данным атрибутом не отображается при использовании шаблонизированных методов  
	        o Display – задает значение элемента label при использовании метода LabelFor 
	        o DataType – задает формат отображения значения свойства 
	        o UIHint – определяет, какой шаблон разметки необходимо использовать для определенного свойства. 

       Системные значения для атрибута UIHint 
	        o Boolean – флажок или флажок с атрибутом disabled 
	        o Collection – подходящий контрол для каждого элемента коллекции 
	        o Decimal – поле ввода со значением в формате с отображением двух десятичных позиций 
	        o EmailAddress – поле ввода или ссылка с href = mailto 
	        o HiddenInput – скрытое поле  
	        o Html – поле ввода или ссылка <a> 
	        o MultilineText – многострочное поле ввода 
	        o Object – подходящий элемент для каждого свойства объекта 
	        o Password – поле ввода для пароля 
	        o String -  поле ввода 
	        o Url – поле ввода или ссылка <a> 
        */

        public ActionResult ModelMetadata()
        {
            Customer customer = new Customer()
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

        /*
        С помощью атрибута UIHint можно указать специальный шаблон, разработанный отдельно. 
        Специальный шаблон – это частичное представление, которое размещено в директории Shared/EditorTemplates 
        (если шаблон предназначен для EidtorFor метода) или Shared/DisplayTemplaes (если шаблон предназначен для DisplayFor метода) 
        и используется при работе с шаблонизированными вспомогательными методами. 

        Процесс поиска шаблона: 
        1. Шаблон, переданный шаблонизированному методу, например, Html.EditorFor(m => m.Prop, “Template”) 
        2. Шаблон указанный атрибутом UIHint. 
        3. Шаблон соответствующий имени класса .NET обрабатываемого типа данных. 
        4. Если обрабатываемый тип данных является простым, используется встроенный шаблон для типа String. 
        5. Любой шаблон, который соответствует базовым классам типа данных. 
        6. Если тип данных реализует IEnumerable, будет использоваться встроенный шаблон Collection. 
        7. Если все проверки завершились неудачно, будет использоваться шаблон Object -  
        данный шаблон просматривает каждое свойство объекта модели и выбирает шаблон, наиболее подходящий для типа свойства. 
        */


        public ActionResult SpecialTemplate()
        {
            Person person = new Person();
            person.PersonId = 1;
            person.Name = "Jhon Doe";
            person.Role = Role.User;

            return View(person);
        }
    }
}
