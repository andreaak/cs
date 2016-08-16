using NUnit.Framework;

namespace Patterns._03_Behavioral.ChainOfResponsibility._001_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Handler h1 = new ConcreteHandler1();
            Handler h2 = new ConcreteHandler2();

            h1.Successor = h2;
            h1.HandleRequest(1);
            h1.HandleRequest(2);
        }
    }
}
