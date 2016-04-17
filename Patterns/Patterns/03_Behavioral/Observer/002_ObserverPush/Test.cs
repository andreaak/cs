using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._03_Behavioral.Observer._002_ObserverPush.Observer;
using Patterns._03_Behavioral.Observer._002_ObserverPush.Subject;

namespace Patterns._03_Behavioral.Observer._002_ObserverPush
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            ConcreteSubject subject = new ConcreteSubject();
            subject.Attach(new ConcreteObserver(subject));
            subject.Attach(new ConcreteObserver(subject));
            subject.State = "Some State ...";
            subject.Notify();
        }
    }
}
