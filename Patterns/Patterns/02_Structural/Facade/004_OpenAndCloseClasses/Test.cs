using NUnit.Framework;

namespace Patterns._02_Structural.Facade._004_OpenAndCloseClasses
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            new Facade().Method();

            // Невозможно получить доступ к закрытым классам подсистемы напрямую, минуя фасад.
            // SubsystemClassA и SubsystemClassB - закрыты.

            // Delay.
            //Console.ReadKey();
        }
    }
}
