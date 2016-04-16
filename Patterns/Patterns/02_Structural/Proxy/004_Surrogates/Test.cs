using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._02_Structural.Proxy._004_Surrogates
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            IHuman BruceWillis = new Operator();
            IHuman surrogate = new Surrogate(BruceWillis);
            surrogate.Request();
        }
    }
}
