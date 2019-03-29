using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace _01_ASPMVCTest.Areas._20_Architectures._04_Monolit
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
