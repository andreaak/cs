using System.Collections.Generic;
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
            TestIndexerClass<int> my = new TestIndexerClass<int>();

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

        [Test]
        public void TestIndexer2()
        {
            int[] a = { 0 };

            int temp = ++a[0];
            Debug.WriteLine(temp);

            temp = ++a[0];
            Debug.WriteLine(temp);

            Debug.WriteLine(a[0]);
            /*
            1
            2
            2 
            */
        }

        [Test]
        public void TestIndexer3()
        {
            List<int> a = new List<int> { 0 };

            int temp = ++a[0];
            Debug.WriteLine(temp);

            temp = ++a[0];
            Debug.WriteLine(temp);

            Debug.WriteLine(a[0]);
            /*
            1
            2
            2 
            */
        }
    }
}
