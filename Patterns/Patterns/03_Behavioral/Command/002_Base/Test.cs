using NUnit.Framework;

namespace Patterns._03_Behavioral.Command._002_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
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
