using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._25_CS6
{
    [TestClass]
    public class _02_PrimaryCtorTests
    {
#if CS6
        [TestMethod]
        public void Test1()
        {

        }
#endif
    }

    //public struct Money(decimal amount, string currency)
    //{
    //    public Money(int amount) : this(amount, "eur")
    //    {

    //    }

    //    public string Currency { get; } = Check.ArgNotNull("currency", currency);
    //    public decimal Amount { get; } = amount;
    //}


}
