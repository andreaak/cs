using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._03_Behavioral.Observer._006_Base.Observer;
using Patterns._03_Behavioral.Observer._006_Base.Subject;

namespace Patterns._03_Behavioral.Observer._006_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
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
