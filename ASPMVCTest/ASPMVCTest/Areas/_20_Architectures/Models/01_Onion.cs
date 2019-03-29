using System;
using System.Collections.Generic;
using System.Linq;
using _01_ASPMVCTest.Areas._20_Architectures.OnionApp.Domain.Interfaces;
using _01_ASPMVCTest.Areas._20_Architectures.OnionApp.Services.Interfaces;
using System.Data.Entity;
using _01_ASPMVCTest.Areas._20_Architectures.OnionApp.Domain.Core;

namespace _01_ASPMVCTest.Areas._20_Architectures.OnionApp.Domain
{
    namespace Core
    {
        public class Book
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }
    }

    namespace Interfaces
    {
        public interface IBookRepository : IDisposable
        {
            IEnumerable<Book> GetBookList();
            Book GetBook(int id);
            void Create(Book item);
            void Update(Book item);
            void Delete(int id);
            void Save();
        }
    }
}

namespace _01_ASPMVCTest.Areas._20_Architectures.OnionApp.Services.Interfaces
{
    public interface IOrder
    {
        void MakeOrder(Book book);
    }
}

namespace _01_ASPMVCTest.Areas._20_Architectures.OnionApp.Infrastructure
{
    namespace Data
    {
        public class OrderContext : DbContext
        {
            public DbSet<Book> Books { get; set; }
        }

        public class BookRepository : IBookRepository
        {
            private OrderContext db;
            public BookRepository()
            {
                this.db = new OrderContext();
            }

            public IEnumerable<Book> GetBookList()
            {
                return db.Books.ToList();
            }

            public Book GetBook(int id)
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
    }

    namespace Business
    {
        public class CacheOrder : IOrder
        {
            public void MakeOrder(Book book)
            {
                // код покупки книги при оплате наличностью
            }
        }

        public class CreditOrder : IOrder
        {
            public void MakeOrder(Book book)
            {
                // код покупки книги с помощью кредитной карты
            }
        }
    }
}
