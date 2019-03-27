using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._20_Architectures.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }

    public class OrderContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
    }

    interface IRepository1<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }

    public class BookRepository : IRepository1<Book>
    {
        private OrderContext db;
        public BookRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return db.Books;
        }

        public Book Get(int id)
        {
            return db.Books.Find(id);
        }

        public void Create(Book book)
        {
            db.Books.Add(book);
        }

        public void Update(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Book book = db.Books.Find(id);
            if (book != null)
                db.Books.Remove(book);
        }
    }

    public class OrderRepository : IRepository1<Order>
    {
        private OrderContext db;
        public OrderRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return db.Orders.Include(o => o.Book);

        }

        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }

        public void Create(Order order)
        {
            db.Orders.Add(order);
        }

        public void Update(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
                db.Orders.Remove(order);
        }
    }

    public class UnitOfWork : IDisposable
    {
        private OrderContext db = new OrderContext();
        private BookRepository bookRepository;
        private OrderRepository orderRepository;

        public BookRepository Books
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BookRepository(db);
                return bookRepository;
            }
        }

        public OrderRepository Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    namespace UoWMvcApp.Controllers
    {
        public class UnitOfWorkController : Controller
        {
            UnitOfWork unitOfWork;
            public UnitOfWorkController()
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
}