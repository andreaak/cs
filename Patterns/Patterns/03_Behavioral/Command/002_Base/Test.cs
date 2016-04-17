using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.Command._002_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Receiver receiver = new Receiver();
            Command command = new ConcreteCommand(receiver);
            Invoker invoker = new Invoker();

            invoker.StoreCommand(command);
            invoker.ExecuteCommand();
        }
    }
}
