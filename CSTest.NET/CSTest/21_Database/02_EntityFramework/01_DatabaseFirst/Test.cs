using CSTest._21_Database._02_EntityFramework._01_DatabaseFirst.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Data;
using System.Diagnostics;

namespace CSTest._21_Database._02_EntityFramework._01_DatabaseFirst
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestEFReadAll()
        {
            ShopDbContext context = new ShopDbContext();

            foreach (var customer in context.Customers.Include(c => c.CustRepNavigation))
            {
                Debug.WriteLine("Id: {0} Company: {1} Rep:{2}", customer.CustNum, customer.Company, customer.CustRepNavigation.Name);
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
            ShopDbContext context = new ShopDbContext();

            //Add customer
            var rep = context.Salesreps.FirstOrDefault(item => item.Name == "Sam Clark");

            Customer customer = new Customer
            {
                CustNum = 2131,
                Company = "Jones Mfg. Rep",
                CreditLimit = 100,
                CustRepNavigation = rep,
            };

            context.Customers.Add(customer);
            context.SaveChanges();

            //Check
            customer = context.Customers.FirstOrDefault(item => item.CustNum == 2130);
            Debug.WriteLine("After add Id: {0} Company: {1} Rep:{2}", customer.CustNum, customer.Company, customer.CustRepNavigation.Name);

            /*
            After add Id: 2130 Company: Jones Mfg. Rep Rep:Sam Clark
            */
        }

        [Test]
        public void TestEFRemoveItem()
        {
            ShopDbContext context = new ShopDbContext();

            //Check
            var customer = context.Customers.Include(c => c.CustRepNavigation).FirstOrDefault(item => item.CustNum == 2130);
            if (customer == null)
            {
                return;
            }
            Debug.WriteLine("After add Id: {0} Company: {1} Rep:{2}", customer.CustNum, customer.Company, customer.CustRepNavigation.Name);

            //Remove customer
            context.Customers.Remove(customer);
            context.SaveChanges();

            //Check
            customer = context.Customers.FirstOrDefault(item => item.CustNum == 2130);
            if (customer != null)
            {
                Debug.WriteLine("After delete Id: {0} Company: {1} Rep:{2}", customer.CustNum, customer.Company, customer.CustRepNavigation.Name);
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
        public void TestEFProcedure1Table()
        {
            ShopDbContext context = new ShopDbContext();

            string sql = "EXEC spCustomerByName @Company";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                // Create parameter(s)    
                new SqlParameter { ParameterName = "@Company", Value = "First Corp."}
            };

            var list = context.Customers.FromSqlRaw(sql, parms.ToArray()).ToList();
            foreach (var customer in list)
            {
                Debug.WriteLine("Id: {0} Company: {1}", customer.CustNum, customer.Company);
            }

            var list2 = context.Customers.FromSqlInterpolated($"EXEC spCustomerByName {"First Corp."}").ToList(); 
            foreach (var customer in list2)
            {
                Debug.WriteLine("Id: {0} Company: {1}", customer.CustNum, customer.Company);
            }  
            /*
            Id: 2102 Company: First Corp. 
            */
        }

        [Test]
        public void TestEFProcedure2Output()
        {
            ShopDbContext context = new ShopDbContext();

            var company = new SqlParameter("@Company", SqlDbType.VarChar)
            {
                Direction = ParameterDirection.Output,
                Size = 20
            };
            var limit = new SqlParameter("@Limit", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output
            };

            List<SqlParameter> parms = new List<SqlParameter>
            {
                // Create parameter(s)
                new SqlParameter("@Id", 2102),
                company,
                limit
            };

            string sql = "EXEC spCustomerById @Id, @Company OUTPUT, @Limit OUTPUT";

            int result = context.Database.ExecuteSqlRaw(sql, parms.ToArray());
            Debug.WriteLine("Result: {0} Company: {1} Limit:{2}", result, company.Value, limit.Value);
            /*
            Result: -1 Company: First Corp. Limit:65000
            */
        }

        [Test]
        public void TestEFProcedure3()
        {
            ShopDbContext context = new ShopDbContext();
            string sql = "EXEC spCustomerByName2 @Company";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                // Create parameter(s)    
                new SqlParameter { ParameterName = "@Company", Value = "First Corp."}
            };

            var list = context.Customers.FromSqlRaw(sql, parms.ToArray()).ToList();
            foreach (var customer in list)
            {
                Debug.WriteLine("Id: {0} Company: {1}", customer.CustNum, customer.Company);
            }
            /*
            Id: 2102 Company: First Corp. 
            */
        }

        //[Test]
        //public void TestEFFunction4Scalar()
        //{
        //    ShopDbContext context = new ShopDbContext();

        //    var company = context.Database
        //        .SqlQuery<string>($"SELECT dbo.sfCustomerCompanyById({2102}) Value")
        //        .Single();

        //    Debug.WriteLine("Id: {0} Company: {1}", 2102, company);

        //    var company2 = context.Customers
        //        .Where(c => c.CustNum == 2102)
        //        .Select(c => context.GetCustomerCompanyById(c.CustNum))
        //        .FirstOrDefault();
        //    Debug.WriteLine("Id: {0} Company: {1}", 2102, company2);

        //    /*
        //    Id: 2102 Company: First Corp.
        //    Id: 2102 Company: First Corp. 
        //    */
        //}

        [Test]
        public void TestEFFunction5Table()
        {
            ShopDbContext context = new ShopDbContext();

            var customers = context.Customers
            .FromSqlInterpolated($"SELECT * FROM dbo.sfCustomerInfoById({2102})")
            .ToList();

            foreach (var customer in customers)
            {
                Debug.WriteLine("Id: {0} Company: {1} Limit: {2}", customer.CustNum, customer.Company, customer.CreditLimit);
            }

            var customers1 = context
                .GetCustomerInfoById(2102)
                .ToArray();

            foreach (var customer in customers1)
            {
                Debug.WriteLine("Id: {0} Company: {1} Limit: {2}", customer.CustNum, customer.Company, customer.CreditLimit);
            }
            /*
            Id: 2102 Company: First Corp. Limit: 65000,0000
            Id: 2102 Company: First Corp. Limit: 65000,0000
            */
        }
    }
}
