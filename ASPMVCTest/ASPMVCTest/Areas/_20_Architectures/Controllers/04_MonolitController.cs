using _01_ASPMVCTest.Areas._20_Architectures._04_Monolit;
using System;
using System.Linq;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._20_Architectures.Controllers
{
    public class _04_MonolitController : Controller
    {
        //
        // GET: /_20_Architectures/04_Monolit/

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
