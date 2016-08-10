using NUnit.Framework;

namespace CSTest._25_CS6
{
    [TestFixture]
    public class _02_PrimaryCtorTests
    {
#if CS6
        [Test]
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
