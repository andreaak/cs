using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._03_Behavioral.Iterator._008_Yield
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Enumerable enumerable = new Enumerable();

            foreach (var item in enumerable)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void Test2()
        {
            Enumerable2 enumerable = new Enumerable2();

            foreach (var item in enumerable)
            {
                Debug.WriteLine(item);
            }
        }
    }
}
