using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.Visitor._004_VisitorExtMethod
{
    public static class Visitors
    {
        public static void VisitConcreteElementA(this ElementA element)
        {
            element.OperationA();
        }

        public static void VisitConcreteElementB(this ElementB element)
        {
            element.OperationB();
        }
    }

    public class ElementA
    {
        public void OperationA()
        {
            Debug.WriteLine("OperationA");
        }
    }

    public class ElementB
    {
        public void OperationB()
        {
            Debug.WriteLine("OperationB");
        }
    }

    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            ElementA elementA = new ElementA();
            ElementB elementB = new ElementB();

            elementA.VisitConcreteElementA();
            elementB.VisitConcreteElementB();

            // Задержка. 
            //Console.ReadKey();
        }
    }
}
