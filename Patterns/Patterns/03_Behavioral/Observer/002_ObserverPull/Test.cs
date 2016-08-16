using NUnit.Framework;
using Patterns._03_Behavioral.Observer._002_ObserverPull.Observer;
using Patterns._03_Behavioral.Observer._002_ObserverPull.Subject;

namespace Patterns._03_Behavioral.Observer._002_ObserverPull
{
    [TestFixture]
    public class Test
    {
        [Test]
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
