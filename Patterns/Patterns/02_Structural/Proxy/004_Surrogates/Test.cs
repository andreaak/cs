using NUnit.Framework;

namespace Patterns._02_Structural.Proxy._004_Surrogates
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            IHuman BruceWillis = new Operator();
            IHuman surrogate = new Surrogate(BruceWillis);
            surrogate.Request();
        }
    }
}
