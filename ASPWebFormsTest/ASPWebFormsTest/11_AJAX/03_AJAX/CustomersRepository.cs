using System.Collections.Generic;

namespace ASPWebFormsTest._11_AJAX._03_AJAX
{
    class CustomersRepository
    {
        public List<Customer> Customers { get; } = new List<Customer>();

        public CustomersRepository()
        {
            Customers.Add(new Customer() { FirstName = "John", LastName = "Doe", Age = 27 });
            Customers.Add(new Customer() { FirstName = "Tom", LastName = "Ronald", Age = 24 });
            Customers.Add(new Customer() { FirstName = "Jane", LastName = "Roe", Age = 32 });
        }
    }
}
