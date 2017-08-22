using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas.E_30_Examples.Models;

namespace _01_ASPMVCTest.Areas.E_30_Examples.Controllers
{
    public class _03_EF_OrdersController : Controller
    {
        private E_03_EF_DatabaseContext db = new E_03_EF_DatabaseContext();

        //
        // GET: /Orders/

        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Product).Include(o => o.Customer);
            return View(orders.ToList());
        }

        //
        // GET: /Orders/Details/5

        public ActionResult Details(int id = 0)
        {
            E_03_EF_Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            return View();
        }

        //
        // POST: /Orders/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(E_03_EF_Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", order.ProductId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", order.CustomerId);
            return View(order);
        }

        //
        // GET: /Orders/Edit/5

        public ActionResult Edit(int id = 0)
        {
            E_03_EF_Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", order.ProductId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", order.CustomerId);
            return View(order);
        }

        //
        // POST: /Orders/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(E_03_EF_Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", order.ProductId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", order.CustomerId);
            return View(order);
        }

        //
        // GET: /Orders/Delete/5

        public ActionResult Delete(int id = 0)
        {
            E_03_EF_Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // POST: /Orders/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            E_03_EF_Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}