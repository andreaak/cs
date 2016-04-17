using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.Iterator._001_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Aggregate a = new ConcreteAggregate();

            a[0] = "Element A";
            a[1] = "Element B";
            a[2] = "Element C";
            a[3] = "Element D";

            Iterator i = a.CreateIterator();

            for (object e = i.First(); !i.IsDone(); e = i.Next())
                Debug.WriteLine(e);

            Debug.WriteLine(new string('-', 10));

            for (object e = i.First(); !i.IsDone(); e = i.Next())
                Debug.WriteLine(e);
        }
    }
}