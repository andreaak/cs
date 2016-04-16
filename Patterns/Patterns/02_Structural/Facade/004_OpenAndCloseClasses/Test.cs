using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._02_Structural.Facade._004_OpenAndCloseClasses
{
    [TestClass]
    public class Test
    {
        [TestMethod]
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
