using _01_ASPMVCTest.Areas._20_Architectures._02_UnitOfWork;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._20_Architectures.Controllers
{
    public class _02_UnitOfWorkController : Controller
    {
        //
        // GET: /_20_Architectures/02_UnitOfWork/

        private readonly UnitOfWork unitOfWork;

        public _02_UnitOfWorkController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            var books = unitOfWork.Books.GetAll();
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book b)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Books.Create(b);
                unitOfWork.Save();
                RedirectToAction("Index");
            }
            return View(b);
        }

        public ActionResult Edit(int id)
        {
            Book b = unitOfWork.Books.Get(id);
            if (b == null)
                return HttpNotFound();
            return View(b);
        }

        [HttpPost]
        public ActionResult Edit(Book b)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Books.Update(b);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(b);
        }

        public ActionResult Delete(int id)
        {
            unitOfWork.Books.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
