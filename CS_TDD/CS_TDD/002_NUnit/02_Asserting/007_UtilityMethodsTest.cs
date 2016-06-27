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

        [Test]
        [Ignore]
        public void Ignore()
        {
            //throw new Exception();

            // Этот метод помечает тест, который будет игнорирован при запуске всех тестов
            // и не будет выполнен.
            Assert.Ignore("Ignore");
        }
    }
}
