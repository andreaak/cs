using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._03_Behavioral.Observer._007_ObserverChangeManager.Manager;

namespace Patterns._03_Behavioral.Observer._007_ObserverChangeManager
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            ChangeManager manager = SimpleChangeManager.Singleton;
            // При удалении комментария со следующей строки commonObserver будет обновлен только 1 раз.
            // manager = DAGChangeManager.Singleton;

            Subject.Subject subject1 = new Subject.Subject("Subject1", manager);
            Subject.Subject subject2 = new Subject.Subject("Subject2", manager);

            Observer.Observer commonObserver = new Observer.Observer("CommonObserver");

            manager.Register(subject1, commonObserver);
            manager.Register(subject1, new Observer.Observer("Observer1"));
            manager.Register(subject1, new Observer.Observer("Observer2"));

            manager.Register(subject2, commonObserver);
            manager.Register(subject2, new Observer.Observer("Observer3"));

            subject1.Notify();

            // Delay.
            //Console.ReadKey();
        }
    }
}
