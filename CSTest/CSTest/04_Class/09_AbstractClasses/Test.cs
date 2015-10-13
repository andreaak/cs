using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CSTest._04_Class._09_AbstractClasses
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestAbstract1()
        {
            AbstractClass instance = new ConcreteClass();
            instance.Method();
        }
        
        [TestMethod]
        public void TestAbstract2()
        {
            AbstractClass2 instance = new ConcreteClassB();

            instance.Method();
            instance.Operation();
        }

        [TestMethod]
        public void TestAbstract3()
        {
            AbstractClassA instance = new ConcreteClass2();

            instance.OperationA();

            //instance.OperationB();  // Вопрос: почему недоступен данный метод?
        }

        [TestMethod]
        public void TestAbstract4()
        {
            ConcreteClass instance = new ConcreteClass();

            instance.SimpleMethod();
            instance.VirtualMethod();
            instance.AbstractMethod();
        }

        [TestMethod]
        public void TestAbstract5()
        {
            AbstractClass instance = new ConcreteClass();

            Debug.WriteLine(new string('-', 55));

            instance.AbstractMethod();

        }
    }
}
