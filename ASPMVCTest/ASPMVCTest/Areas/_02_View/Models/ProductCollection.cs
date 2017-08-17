using System.Collections.Generic;

namespace _01_ASPMVCTest.Areas._02_View.Models
{
    public class ProductCollection
    {
        public static List<Product> All
        {
            get
            {
                List<Product> products = new List<Product>();
                for (int i = 0; i < 20; i++)
                {
                    products.Add(new Product()
                    {
                        ProductId = i + 1,
                        Name = "Item Name " + i,
                        Price = (i + 1) * 2
                    });
                }
                return products;
            }
        }
    }
}