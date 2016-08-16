using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._03_Behavioral.Iterator._008_Yield
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Enumerable enumerable = new Enumerable();

            foreach (var item in enumerable)
            {
                Debug.WriteLine(item);
            }
        }

        [Test]
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
