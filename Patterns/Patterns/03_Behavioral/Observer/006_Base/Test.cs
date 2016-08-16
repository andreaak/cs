using System.Diagnostics;
using NUnit.Framework;
using Patterns._03_Behavioral.Observer._006_Base.Observer;
using Patterns._03_Behavioral.Observer._006_Base.Subject;

namespace Patterns._03_Behavioral.Observer._006_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            ConcreteSubject aConcreteSubject = new ConcreteSubject();
            ConcreteObserver aConcreteObserver = new ConcreteObserver(aConcreteSubject);
            ConcreteObserver anotherConcreteObserver = new ConcreteObserver(aConcreteSubject);

            aConcreteSubject.aConcreteObserver = aConcreteObserver;
            aConcreteSubject.anotherConcreteObserver = anotherConcreteObserver;

            aConcreteObserver.ChangeState("First state!");

            Debug.WriteLine(new string('-', 70));

            anotherConcreteObserver.ChangeState("Second state!");
        }
    }
}
