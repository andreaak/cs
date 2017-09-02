using System.Collections.Generic;

namespace _01_ASPMVCTest.Models
{
    public static class W_06_Products
    {
        static List<W_06_Product> products = new List<W_06_Product>() 
        {
            new W_06_Product() { Id = 1, Name = "Car", Price = 15000 },
            new W_06_Product() { Id = 2, Name = "Book", Price = 40 },
            new W_06_Product() { Id = 3, Name = "Laptop", Price = 1400 },
            new W_06_Product() { Id = 4, Name = "Pen", Price = 1 },
            new W_06_Product() { Id = 5, Name = "Phone", Price = 4000 },
            new W_06_Product() { Id = 6, Name = "House", Price = 120000 }
        };

        public static IEnumerable<W_06_Product> GetAllProducts()
        {
            return products;
        }
    }
}