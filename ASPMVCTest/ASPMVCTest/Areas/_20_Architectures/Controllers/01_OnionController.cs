using _01_ASPMVCTest.Areas._20_Architectures.OnionApp.Domain.Core;
using _01_ASPMVCTest.Areas._20_Architectures.OnionApp.Domain.Interfaces;
using _01_ASPMVCTest.Areas._20_Architectures.OnionApp.Services.Interfaces;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._20_Architectures.Controllers
{
    public class _01_OnionController : Controller
    {
        //
        // GET: /_20_Architectures/01_Onion/

        IBookRepository repo;
        IOrder order;

        public _01_OnionController(IBookRepository r, IOrder o)
        {
            repo = r;
            order = o;
        }

        public ActionResult Index()
        {
            var books = repo.GetBookList();
            return View();
        }

        public ActionResult Buy(int id)
        {
            Book book = repo.GetBook(id);
            order.MakeOrder(book);
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            repo.Dispose();
            base.Dispose(disposing);
        }

    }
}
