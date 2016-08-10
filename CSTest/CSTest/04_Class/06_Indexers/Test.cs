using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._04_Class._06_Indexers
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestIndexer1()
        {
            TestClass my = new TestClass();

            my[0] = 1;
            my[1] = 2;
            my[2] = 3;
            my[3] = 4;
            my[4] = 5;

            Debug.WriteLine(my[0]);
            Debug.WriteLine(my[1]);
            Debug.WriteLine(my[2]);
            Debug.WriteLine(my[3]);
            Debug.WriteLine(my[4]);
        }
    }
}
