using NUnit.Framework;

namespace Patterns._03_Behavioral.TemplateMethod._002_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            AbstractClass instance = new ConcreteClass();
            instance.TemplateMethod();
        }
    }
}
