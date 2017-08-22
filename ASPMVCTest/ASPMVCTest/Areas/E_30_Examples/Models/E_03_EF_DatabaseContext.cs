using System.Data.Entity;

namespace _01_ASPMVCTest.Areas.E_30_Examples.Models
{
    public class E_03_EF_DatabaseContext : DbContext
    {
        public E_03_EF_DatabaseContext()
            : base("local")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<E_03_EF_DatabaseContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<E_03_EF_Customer> Customers { get; set; }

        public DbSet<E_03_EF_Order> Orders { get; set; }

        public DbSet<E_03_EF_Product> Products { get; set; }
    }
}
