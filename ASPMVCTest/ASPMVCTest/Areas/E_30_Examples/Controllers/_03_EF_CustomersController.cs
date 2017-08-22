using System.Data;
using System.Linq;
using System.Web.Mvc;
using _01_ASPMVCTest.Areas.E_30_Examples.Models;

namespace _01_ASPMVCTest.Areas.E_30_Examples.Controllers
{
    public class _03_EF_CustomersController : Controller
    {
        private E_03_EF_DatabaseContext db = new E_03_EF_DatabaseContext();

        //
        // GET: /Customers/

        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        //
        // GET: /Customers/Details/5

        public ActionResult Details(int id = 0)
        {
            E_03_EF_Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // GET: /Customers/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(E_03_EF_Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        //
        // GET: /Customers/Edit/5

        public ActionResult Edit(int id = 0)
        {
            E_03_EF_Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customers/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(E_03_EF_Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        //
        // GET: /Customers/Delete/5

        public ActionResult Delete(int id = 0)
        {
            E_03_EF_Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customers/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            E_03_EF_Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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