using System.Data.Entity;

namespace _01_ASPMVCTest.Areas.E_30_Examples.Models
{
    public class E_02_EF_ProductsContext : DbContext
    {
        public E_02_EF_ProductsContext()
            : base("local")
        {
        }

        public DbSet<E_02_EF_Product> Products { get; set; }
    }
}