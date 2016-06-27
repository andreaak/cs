using NUnit.Framework;

namespace CS_TDD._002_NUnit._02_Asserting
{
    [TestFixture]
    class TypeAssertsTest
    {
        [Test]
        public void InInstanceOf()
        {
            // Методы для проверки типов объектов.
            Assert.IsInstanceOf(typeof(object), "Hello");
            Assert.IsNotInstanceOf(typeof(string), 5);
        }
    }
}
