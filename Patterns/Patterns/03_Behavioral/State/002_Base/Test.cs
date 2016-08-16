using NUnit.Framework;

namespace Patterns._03_Behavioral.State._002_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Context context = new Context(new ConcreteStateA());
            context.Request();
            context.Request();
        }
    }
}
