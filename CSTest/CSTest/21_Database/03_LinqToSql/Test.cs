using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CSTest._21_Database._03_LinqToSql
{

    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestLTS1()
        {
            ShopDBDataContext context = new ShopDBDataContext();

            foreach (CUSTOMER customer in context.CUSTOMERs)
            {
                Debug.WriteLine("Id: {0} Company: {1} Rep: {2}", customer.CUST_NUM, customer.COMPANY, customer.SALESREP.NAME);
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

        [TestMethod]
        public void TestLTSProcedure1()
        {
            ShopDBDataContext context = new ShopDBDataContext();
            var result = context.spCustomerByName("First Corp.");
            foreach (var customer in result)
            {
                Debug.WriteLine("Id: {0} Company: {1}", customer.CUST_NUM, customer.COMPANY);
            }
            /*
            Id: 2102 Company: First Corp. 
            */
        }

        [TestMethod]
        public void TestLTSProcedure2()
        {
            ShopDBDataContext context = new ShopDBDataContext();
            string company = null;
            decimal? limit = null;

            int result = context.spCustomerById(2102, ref company, ref limit);
            Debug.WriteLine("Result: {0} Company: {1} Limit:{2}", result, company, limit);
            /*
            Result: 0 Company: First Corp. Limit:65000 
            */
        }

        [TestMethod]
        public void TestLTSProcedure3()
        {
            ShopDBDataContext context = new ShopDBDataContext();
            var result = context.spCustomerByName2("First Corp.");
            foreach (var customer in result)
            {
                Debug.WriteLine("Id: {0} Company: {1}", customer.CUST_NUM, customer.COMPANY);
            }
            /*
            Id: 2102 Company: First Corp. 
            */
        }

        [TestMethod]
        public void TestLTSProcedure4()
        {
            ShopDBDataContext context = new ShopDBDataContext();
            string result = context.sfCustomerCompanyById(2102);
            Debug.WriteLine("Id: {0} Company: {1}", 2102, result);
            /*
            Id: 2102 Company: First Corp. 
            */
        }

        [TestMethod]
        public void TestLTSProcedure5()
        {
            ShopDBDataContext context = new ShopDBDataContext();
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
