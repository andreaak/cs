using NUnit.Framework;

namespace CS_TDD._002_NUnit._01_LifeCycle
{
    [TestFixture]
    class _02_SkipTest
    {
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
