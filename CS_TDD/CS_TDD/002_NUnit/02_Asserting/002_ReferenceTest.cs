using NUnit.Framework;

namespace CS_TDD._002_NUnit._02_Asserting
{
    [TestFixture]
    class ReferenceTest
    {
        [Test]
        public void AreSame()
        {
            string a = "hello";
            string b = "world!";
            a = b;

            // AreSame - проверяет, ссылаются ли переменные на одну и ту же область памяти.
            Assert.AreSame(a, b);
        }
    }
}
