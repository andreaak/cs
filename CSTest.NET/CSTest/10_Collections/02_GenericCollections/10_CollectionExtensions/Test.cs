using NUnit.Framework;
using System.Collections.Generic;
using NUnit.Framework.Legacy;

namespace CSTest._10_Collections._02_GenericCollections._10_CollectionExtensions
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestArrayIsEmpty()
        {
            int[] testInt = null;
            ClassicAssert.IsTrue(testInt.IsEmpty());

            int[] testInt2 = new int[0];
            ClassicAssert.IsTrue(testInt2.IsEmpty());

            int[] testInt3 = { 5 };
            ClassicAssert.IsFalse(testInt3.IsEmpty());

            string[] testStr = { "Test" };
            ClassicAssert.IsFalse(testStr.IsEmpty());

        }

        [Test]
        public void TestListIsEmpty()
        {
            List<int> testInt = null;
            ClassicAssert.IsTrue(testInt.IsEmpty());

            List<int> testInt2 = new List<int>();
            ClassicAssert.IsTrue(testInt2.IsEmpty());

            List<int> testInt3 = new List<int> { 5 };
            ClassicAssert.IsFalse(testInt3.IsEmpty());

            List<string> testStr = new List<string> { "Test" };
            ClassicAssert.IsFalse(testStr.IsEmpty());
        }

        [Test]
        public void TestListIsEmpty_()
        {
            List<int> testInt = null;
            ClassicAssert.IsTrue(IsEmpty_(testInt));

            List<int> testInt2 = new List<int>();
            ClassicAssert.IsTrue(IsEmpty_(testInt2));

            List<int> testInt3 = new List<int> { 5 };
            ClassicAssert.IsFalse(IsEmpty_(testInt3));

        }

        private bool IsEmpty_(List<int> testInt)
        {
            return testInt?.Any() != true;
        }

        [Test]
        public void TestListIsEmpty_NotEmpty()
        {
            List<int> testInt = null;
            ClassicAssert.IsFalse(IsNotEmpty_(testInt));

            List<int> testInt2 = new List<int>();
            ClassicAssert.IsFalse(IsNotEmpty_(testInt));

            List<int> testInt3 = new List<int> { 5 };
            ClassicAssert.IsTrue(IsNotEmpty_(testInt3));
        }

        private bool IsNotEmpty_(List<int> testInt)
        {
            return (testInt?.Any() ?? false) != false;
        }

        [Test]
        public void TestListIsEmpty_NotEmpty2()
        {
            List<int> testInt = null;
            ClassicAssert.IsFalse(IsNotEmpty(testInt));

            List<int> testInt2 = new List<int>();
            ClassicAssert.IsFalse(IsNotEmpty(testInt2));

            List<int> testInt3 = new List<int> { 5 };
            ClassicAssert.IsTrue(IsNotEmpty(testInt3));
        }

        [Test]
        public void TestListIsEmpty_NotEmpty3()
        {
            List<int> testInt = null;
            ClassicAssert.IsFalse(IsNotEmpty2(testInt));

            List<int> testInt2 = new List<int>();
            ClassicAssert.IsFalse(IsNotEmpty2(testInt2));

            List<int> testInt3 = new List<int> { 5 };
            ClassicAssert.IsTrue(IsNotEmpty2(testInt3));
        }

        [Test]
        public void TestListIsEmpty_Empty()
        {
            List<int> testInt = null;
            ClassicAssert.IsTrue(IsEmpty(testInt));

            List<int> testInt2 = new List<int>();
            ClassicAssert.IsTrue(IsEmpty(testInt2));

            List<int> testInt3 = new List<int> { 5 };
            ClassicAssert.IsFalse(IsEmpty(testInt3));
        }

        private bool IsNotEmpty(List<int> testInt)
        {
            //return testInt?.Count > 0;
            return (testInt?.Count ?? 0) != 0;
        }

        private bool IsNotEmpty2(List<int> testInt)
        {
            return testInt?.Count > 0;
        }

        private bool IsEmpty(List<int> testInt)
        {
            return (testInt?.Count ?? 0) == 0;
        }

        [Test]
        public void TestHashSetIsEmpty()
        {
            HashSet<int> testInt = null;
            ClassicAssert.IsTrue(testInt.IsEmpty());

            HashSet<int> testInt2 = new HashSet<int>();
            ClassicAssert.IsTrue(testInt2.IsEmpty());

            HashSet<int> testInt3 = new HashSet<int> { 5 };
            ClassicAssert.IsFalse(testInt3.IsEmpty());

            HashSet<string> testStr = new HashSet<string> { "Test" };
            ClassicAssert.IsFalse(testStr.IsEmpty());
        }

        [Test]
        public void TestSortedSetIsEmpty()
        {
            SortedSet<int> testInt = null;
            ClassicAssert.IsTrue(testInt.IsEmpty());

            SortedSet<int> testInt2 = new SortedSet<int>();
            ClassicAssert.IsTrue(testInt2.IsEmpty());

            SortedSet<int> testInt3 = new SortedSet<int> { 5 };
            ClassicAssert.IsFalse(testInt3.IsEmpty());

            SortedSet<string> testStr = new SortedSet<string> { "Test" };
            ClassicAssert.IsFalse(testStr.IsEmpty());
        }

        [Test]
        public void TestDictionaryIsEmpty()
        {
            Dictionary<int, int> testInt = null;
            ClassicAssert.IsTrue(testInt.IsEmpty());

            Dictionary<int, int> testInt2 = new Dictionary<int, int>();
            ClassicAssert.IsTrue(testInt2.IsEmpty());

            Dictionary<int, int> testInt3 = new Dictionary<int, int> { [5] = 5 };
            ClassicAssert.IsFalse(testInt3.IsEmpty());

            Dictionary<string, string> testStr = new Dictionary<string, string> { ["TestKey"] = "TestValue" };
            ClassicAssert.IsFalse(testStr.IsEmpty());
        }


        [Test]
        public void TestSortedDictionaryIsEmpty()
        {
            SortedDictionary<int, int> testInt = null;
            ClassicAssert.IsTrue(testInt.IsEmpty());

            SortedDictionary<int, int> testInt2 = new SortedDictionary<int, int>();
            ClassicAssert.IsTrue(testInt2.IsEmpty());

            SortedDictionary<int, int> testInt3 = new SortedDictionary<int, int> { [5] = 5 };
            ClassicAssert.IsFalse(testInt3.IsEmpty());

            SortedDictionary<string, string> testStr = new SortedDictionary<string, string> { ["TestKey"] = "TestValue" };
            ClassicAssert.IsFalse(testStr.IsEmpty());
        }

        [Test]
        public void TestStackIsEmpty()
        {
            Stack<int> testInt = null;
            ClassicAssert.IsTrue(testInt.IsEmpty());

            Stack<int> testInt2 = new Stack<int>();
            ClassicAssert.IsTrue(testInt2.IsEmpty());

            Stack<int> testInt3 = new Stack<int>();
            testInt3.Push(5);
            ClassicAssert.IsFalse(testInt3.IsEmpty());

            Stack<string> testStr = new Stack<string>();
            testStr.Push("Test");
            ClassicAssert.IsFalse(testStr.IsEmpty());
        }

        [Test]
        public void TestQueueIsEmpty()
        {
            Queue<int> testInt = null;
            ClassicAssert.IsTrue(testInt.IsEmpty());

            Queue<int> testInt2 = new Queue<int>();
            ClassicAssert.IsTrue(testInt2.IsEmpty());

            Queue<int> testInt3 = new Queue<int>();
            testInt3.Enqueue(5);
            ClassicAssert.IsFalse(testInt3.IsEmpty());

            Queue<string> testStr = new Queue<string>();
            testStr.Enqueue("Test");
            ClassicAssert.IsFalse(testStr.IsEmpty());
        }
    }
}
