using NUnit.Framework;

namespace CS_TDD._002_NUnit._02_Asserting
{
    [TestFixture]
    class UtilityMethodsTest
    {
        [Test]
        public void Fail()
        {
            // Метод создает тест, который не будет пройден с сообщением Fail!.
            Assert.Fail("Fail!");
        }
    }
}
