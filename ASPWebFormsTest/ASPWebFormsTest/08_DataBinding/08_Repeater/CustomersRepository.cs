using System.Collections.Generic;

namespace ASPWebFormsTest._08_DataBinding._08_Repeater
{
    public static class CustomersRepository
    {
        private static List<Customer> _customers = null;
        public static List<Customer> Customers
        {
            get
            {
                if (_customers == null)
                {
                    _customers = new List<Customer>();

                    Customer temp = new Customer();
                    temp.FirstName = "Ivan";
                    temp.LastName = "Ivanov";
                    temp.PhoneNumber = "111-22-33";
                    temp.Email = "i.ivanov@example.com";
                    _customers.Add(temp);

                    temp = new Customer();
                    temp.FirstName = "Petr";
                    temp.LastName = "Petrov";
                    temp.PhoneNumber = "123-45-67";
                    temp.Email = "petrov@example.com";
                    _customers.Add(temp);

                    temp = new Customer();
                    temp.FirstName = "Fedor";
                    temp.LastName = "Fedorov";
                    temp.PhoneNumber = "999-88-77";
                    temp.Email = "fedor@example.com";
                    _customers.Add(temp);

                    temp = new Customer();
                    temp.FirstName = "Sergei";
                    temp.LastName = "Sergeev";
                    temp.PhoneNumber = "555-88-55";
                    temp.Email = "sergei_sergeev@example.com";
                    _customers.Add(temp);
                }
                return _customers;
            }
        }
    }

    public struct Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}