using System.Data.Entity;

namespace _01_ASPMVCTest.Areas.E_30_Examples.Models
{
    public class E_02_EF_ProductsDbInitializer : DropCreateDatabaseAlways<E_02_EF_ProductsContext>
    {
        protected override void Seed(E_02_EF_ProductsContext context)
        {
            context.Products.Add(new E_02_EF_Product() { Id = 1, Name = "Product 1", Price = 10 });
            context.Products.Add(new E_02_EF_Product() { Id = 2, Name = "Product 2", Price = 20 });
            context.Products.Add(new E_02_EF_Product() { Id = 3, Name = "Product 3", Price = 30 });
            context.Products.Add(new E_02_EF_Product() { Id = 4, Name = "Product 4", Price = 40 });

            base.Seed(context);
        }
    }
}