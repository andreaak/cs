using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._03_Behavioral.Iterator._002_Bank
{
    [TestFixture]
    public class Test
    {
        [Test]
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