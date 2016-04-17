using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.Iterator._002_Bank
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            IEnumerable bank = new Bank();

            IEnumerator cashier = bank.GetEnumerator();

            while(cashier.MoveNext())
            {
                Banknote banknote = cashier.Current as Banknote;
                Debug.WriteLine(banknote.Nominal);
            }            

            // Delay.
            //Console.ReadKey();
        }
    }
}