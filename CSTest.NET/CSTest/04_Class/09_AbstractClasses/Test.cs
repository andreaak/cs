using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._04_Class._09_AbstractClasses
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestAbstract1()
        {
            AbstractClass instance = new ConcreteClass();
            instance.Method();
        }

        [Test]
        public void TestAbstract2()
        {
            AbstractClass2 instance = new ConcreteClassB();

            instance.Method();
            instance.Operation();
        }

        [Test]
        public void TestAbstract3()
        {
            AbstractClassA instance = new ConcreteClass2();

            instance.OperationA();

            //instance.OperationB();  // Вопрос: почему недоступен данный метод?
        }

        [Test]
        public void TestAbstract4()
        {
            ConcreteClass instance = new ConcreteClass();

            instance.SimpleMethod();
            instance.VirtualMethod();
            instance.AbstractMethod();
        }

        [Test]
        public void TestAbstract5()
        {
            AbstractClass instance = new ConcreteClass();

            Debug.WriteLine(new string('-', 55));

            instance.AbstractMethod();
        }
    }
}
