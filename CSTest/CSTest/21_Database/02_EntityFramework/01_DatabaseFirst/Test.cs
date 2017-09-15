using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace CSTest._21_Database._02_EntityFramework._01_DatabaseFirst
{

    [TestFixture]
    public class Test
    {
        [Test]
        public void TestEFReadAll()
        {
            ShopDBEntities context = new ShopDBEntities();

            foreach (var customer in context.CUSTOMERS)
            {
                Debug.WriteLine("Id: {0} Company: {1} Rep:{2}", customer.CUST_NUM, customer.COMPANY, customer.SALESREPS.NAME);
            }

            /*
            Id: 2101 Company: Jones Mfg. Rep:Sam Clark
            Id: 2102 Company: First Corp. Rep:Dan Roberts
            Id: 2103 Company: Acme Mfg. Rep:Bill Adams
            Id: 2105 Company: AAA Investments Rep:Dan Roberts
            Id: 2106 Company: Fred Lewis Corp. Rep:Sue Smith
            Id: 2107 Company: Ace International Rep:Tom Snyder
            Id: 2108 Company: Holm & Landis Rep:Mary Jones
            Id: 2109 Company: Chen Associates Rep:Paul Cruz
            Id: 2111 Company: JCP Inc. Rep:Paul Cruz
            Id: 2112 Company: Zetacorp Rep:Larry Fitch
            Id: 2113 Company: Ian & Schmidt Rep:Bob Smith
            Id: 2114 Company: Orion Corp. Rep:Sue Smith
            Id: 2115 Company: Smithson Corp. Rep:Dan Roberts
            Id: 2117 Company: J.P. Sinclair Rep:Sam Clark
            Id: 2118 Company: Midwest Systems Rep:Larry Fitch
            Id: 2119 Company: Solomon Inc. Rep:Mary Jones
            Id: 2120 Company: Rico Enterprises Rep:Sue Smith
            Id: 2121 Company: QMA Assoc. Rep:Paul Cruz
            Id: 2122 Company: Three Way Lines Rep:Bill Adams
            Id: 2123 Company: Carter & Sons Rep:Sue Smith
            Id: 2124 Company: Peter Brothers Rep:Nancy Angelli
            */
        }

        [Test]
        public void TestEFAddItem()
        {
            ShopDBEntities context = new ShopDBEntities();

            //Add customer
            SALESREPS rep = context.SALESREPS.FirstOrDefault(item => item.NAME == "Sam Clark");

            CUSTOMERS customer = new CUSTOMERS
            {
                CUST_NUM = 2130,
                COMPANY = "Jones Mfg. Rep",
                CREDIT_LIMIT = 100,
                SALESREPS = rep,
            };

            context.CUSTOMERS.Add(customer);
            context.SaveChanges();

            //Check
            customer = context.CUSTOMERS.FirstOrDefault(item => item.CUST_NUM == 2130);
            Debug.WriteLine("After add Id: {0} Company: {1} Rep:{2}", customer.CUST_NUM, customer.COMPANY, customer.SALESREPS.NAME);

            /*
            After add Id: 2130 Company: Jones Mfg. Rep Rep:Sam Clark
            */
        }

        [Test]
        public void TestEFRemoveItem()
        {
            ShopDBEntities context = new ShopDBEntities();

            //Check
            CUSTOMERS customer = context.CUSTOMERS.FirstOrDefault(item => item.CUST_NUM == 2130);
            if (customer == null)
            {
                return;
            }
            Debug.WriteLine("After add Id: {0} Company: {1} Rep:{2}", customer.CUST_NUM, customer.COMPANY, customer.SALESREPS.NAME);

            //Remove customer
            context.CUSTOMERS.Remove(customer);
            context.SaveChanges();

            //Check
            customer = context.CUSTOMERS.FirstOrDefault(item => item.CUST_NUM == 2130);
            if (customer != null)
            {
                Debug.WriteLine("After delete Id: {0} Company: {1} Rep:{2}", customer.CUST_NUM, customer.COMPANY, customer.SALESREPS.NAME);
            }
            else
            {
                Debug.WriteLine("After delete customer is null");
            }

            /*
            After delete customer is null
            */
        }

        [Test]
        public void TestEntityFrameworkProcedure1()
        {
            ShopDBEntities context = new ShopDBEntities();
            var result = context.spCustomerByName("First Corp.");
            foreach (var customer in result)
            {
                Debug.WriteLine("Id: {0} Company: {1}", customer.CUST_NUM, customer.COMPANY);
            }
            /*
            Id: 2102 Company: First Corp. 
            */
        }

        [Test]
        public void TestEntityFrameworkProcedure2()
        {
            ShopDBEntities context = new ShopDBEntities();

            ObjectParameter company = new ObjectParameter("Company", typeof(string));
            ObjectParameter limit = new ObjectParameter("Limit", typeof(decimal));

            int result = context.spCustomerById(2102, company, limit);
            Debug.WriteLine("Result: {0} Company: {1} Limit:{2}", result, company.Value, limit.Value);
            /*
            Result: -1 Company: First Corp. Limit:65000
            */
        }

        [Test]
        public void TestEntityFrameworkProcedure3()
        {
            ShopDBEntities context = new ShopDBEntities();
            var result = context.spCustomerByName2("First Corp.");
            foreach (var customer in result)
            {
                Debug.WriteLine("Id: {0} Company: {1}", customer.CUST_NUM, customer.COMPANY);
            }
            /*
            Id: 2102 Company: First Corp. 
            */
        }

        //[Test]
        //public void TestEFFunction4()
        //{
        //    ShopDBEntities context = new ShopDBEntities();
        //    //string result = context.sfCustomerCompanyById(2102);
        //    //Debug.WriteLine("Id: {0} Company: {1}", 2102, result);
        //    /*
        //    Id: 2102 Company: First Corp. 
        //    */
        //}

        [Test]
        public void TestEntityFrameworkFunction1()
        {
            ShopDBEntities context = new ShopDBEntities();
            var result = context.sfCustomerInfoById(2102);
            foreach (var customer in result)
            {
                Debug.WriteLine("Id: {0} Company: {1} Limit: {2}", customer.CUST_NUM, customer.COMPANY, customer.CREDIT_LIMIT);
            }
            /*
            Id: 2102 Company: First Corp. Limit: 65000.0000 
            */
        }
    }
}
