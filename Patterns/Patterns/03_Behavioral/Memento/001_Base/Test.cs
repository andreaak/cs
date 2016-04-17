using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.Memento._001_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Originator originator = new Originator();
            originator.State = "On";

            Caretaker caretaker = new Caretaker();
            caretaker.Memento = originator.CreateMemento();

            originator.State = "Off";

            originator.SetMemento(caretaker.Memento);
        }
    }
}
