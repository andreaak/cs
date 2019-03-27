using _01_ASPMVCTest.Areas._20_Architectures.Models.Monolit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._20_Architectures.Models.Monolit
{
    public class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
    }

    public class MobileContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }
    }

    public class StoreDbInitializer : DropCreateDatabaseAlways<MobileContext>
    {
        protected override void Seed(MobileContext db)
        {
            db.Phones.Add(new Phone { Name = "Nokia Lumia 630", Company = "Nokia", Price = 220 });
            db.Phones.Add(new Phone { Name = "iPhone 6", Company = "Apple", Price = 320 });
            db.Phones.Add(new Phone { Name = "LG G4", Company = "lG", Price = 260 });
            db.Phones.Add(new Phone { Name = "Samsung Galaxy S 6", Company = "Samsung", Price = 300 });
            db.SaveChanges();
        }
    }

    public class OrderViewModel
    {
        public int PhoneId { get; set; } // id телефона
        public string Address { get; set; } // адрес
        public string PhoneNumber { get; set; } // номер телефона покупателя
    }
}

namespace MonolitMvcApp.Controllers
{
    public class MonolitController : Controller
    {
        MobileContext db = new MobileContext();

        public ActionResult Index()
        {
            return View(db.Phones.ToList());
        }

        public ActionResult MakeOrder(int? id)
        {
            if (id == null)
                return HttpNotFound();
            Phone phone = db.Phones.Find(id);
            if (phone == null)
                return HttpNotFound();
            OrderViewModel orderModel = new OrderViewModel { PhoneId = phone.Id };
            return View(orderModel);
        }

        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel orderModel)
        {
            if (ModelState.IsValid)
            {
                Phone phone = db.Phones.Find(orderModel.PhoneId);
                if (phone == null)
                    return HttpNotFound();
                decimal sum = phone.Price;
                // если сегодня первое число месяца, тогда скидка в 10%
                if (DateTime.Now.Day == 1)
                    sum = sum - sum * 0.1m;
                Order order = new Order
                {
                    PhoneId = phone.Id,
                    PhoneNumber = orderModel.PhoneNumber,
                    Address = orderModel.Address,
                    Date = DateTime.Now,
                    Sum = sum
                };
                db.Orders.Add(order);
                db.SaveChanges();
                return Content("<h2>Ваш заказ успешно оформлен</h2>");
            }
            return View(orderModel);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
