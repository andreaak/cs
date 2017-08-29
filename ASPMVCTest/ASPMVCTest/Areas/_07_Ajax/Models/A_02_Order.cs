using System.Collections.Generic;
using System.Threading;

namespace _01_ASPMVCTest.Areas._07_Ajax.Models
{
    public class A_02_Order
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public string Customer { get; set; }
        public int Quantity { get; set; }
    }

    class A_02_OrdersDatabase
    {
        public static IEnumerable<A_02_Order> Orders
        {
            get
            {
                Thread.Sleep(2000);

                List<A_02_Order> list = new List<A_02_Order>();
                list.Add(new A_02_Order() { Id = 1, Product = "Product 1", Customer = "Ivanov", Quantity = 1 });
                list.Add(new A_02_Order() { Id = 2, Product = "Product 2", Customer = "Petrov", Quantity = 10 });
                list.Add(new A_02_Order() { Id = 3, Product = "Product 2", Customer = "Fedorov", Quantity = 12 });
                list.Add(new A_02_Order() { Id = 4, Product = "Product 3", Customer = "Fedorov", Quantity = 6 });
                list.Add(new A_02_Order() { Id = 5, Product = "Product 1", Customer = "Petrov", Quantity = 7 });
                list.Add(new A_02_Order() { Id = 6, Product = "Product 4", Customer = "Ivanov", Quantity = 11 });
                list.Add(new A_02_Order() { Id = 7, Product = "Product 2", Customer = "Petrov", Quantity = 10 });
                list.Add(new A_02_Order() { Id = 8, Product = "Product 5", Customer = "Petrov", Quantity = 2 });
                list.Add(new A_02_Order() { Id = 9, Product = "Product 1", Customer = "Ivanov", Quantity = 11 });
                list.Add(new A_02_Order() { Id = 10, Product = "Product 5", Customer = "Fedorov", Quantity = 3 });
                list.Add(new A_02_Order() { Id = 11, Product = "Product 2", Customer = "Petrov", Quantity = 3 });
                list.Add(new A_02_Order() { Id = 12, Product = "Product 1", Customer = "Ivanov", Quantity = 3 });
                list.Add(new A_02_Order() { Id = 13, Product = "Product 4", Customer = "Petrov", Quantity = 7 });
                return list;
            }
        }

    }
}