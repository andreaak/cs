using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CSTest._10_Collections._02_GenericCollections._10_CollectionExtensions
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestArrayIsEmpty()
        {
            int[] testInt = null;
            Assert.IsTrue(testInt.IsEmpty());

            int[] testInt2 = new int[0];
            Assert.IsTrue(testInt2.IsEmpty());

            int[] testInt3 = { 5 };
            Assert.IsFalse(testInt3.IsEmpty());

            string[] testStr = { "Test" };
            Assert.IsFalse(testStr.IsEmpty());

        }

        [TestMethod]
        public void TestListIsEmpty()
        {
            List<int> testInt = null;
            Assert.IsTrue(testInt.IsEmpty());

            List<int> testInt2 = new List<int>();
            Assert.IsTrue(testInt2.IsEmpty());

            List<int> testInt3 = new List<int> { 5 };
            Assert.IsFalse(testInt3.IsEmpty());

            List<string> testStr = new List<string> { "Test" };
            Assert.IsFalse(testStr.IsEmpty());
        }

        [TestMethod]
        public void TestListIsEmpty_()
        {
            List<int> testInt = null;
            Assert.IsTrue(IsEmpty_(testInt));

            List<int> testInt2 = new List<int>();
            Assert.IsTrue(IsEmpty_(testInt2));

            List<int> testInt3 = new List<int> { 5 };
            Assert.IsFalse(IsEmpty_(testInt3));

        }

        private bool IsEmpty_(List<int> testInt)
        {
            return testInt?.Any() != true;
        }

        [TestMethod]
        public void TestListIsEmpty_NotEmpty()
        {
            List<int> testInt = null;
            Assert.IsFalse(IsNotEmpty_(testInt));

            List<int> testInt2 = new List<int>();
            Assert.IsFalse(IsNotEmpty_(testInt));

            List<int> testInt3 = new List<int> { 5 };
            Assert.IsTrue(IsNotEmpty_(testInt3));
        }

        private bool IsNotEmpty_(List<int> testInt)
        {
            return testInt?.Any() != false;
        }

        [TestMethod]
        public void TestListIsEmpty_NotEmpty2()
        {
            List<int> testInt = null;
            Assert.IsFalse(IsNotEmpty(testInt));

            List<int> testInt2 = new List<int>();
            Assert.IsFalse(IsNotEmpty(testInt2));

            List<int> testInt3 = new List<int> { 5 };
            Assert.IsTrue(IsNotEmpty(testInt3));
        }

        [TestMethod]
        public void TestListIsEmpty_NotEmpty3()
        {
            List<int> testInt = null;
            Assert.IsFalse(IsNotEmpty2(testInt));

            List<int> testInt2 = new List<int>();
            Assert.IsFalse(IsNotEmpty2(testInt2));

            List<int> testInt3 = new List<int> { 5 };
            Assert.IsTrue(IsNotEmpty2(testInt3));
        }

        [TestMethod]
        public void TestListIsEmpty_Empty()
        {
            List<int> testInt = null;
            Assert.IsTrue(IsEmpty(testInt));

            List<int> testInt2 = new List<int>();
            Assert.IsTrue(IsEmpty(testInt2));

            List<int> testInt3 = new List<int> { 5 };
            Assert.IsFalse(IsEmpty(testInt3));
        }

        private bool IsNotEmpty(List<int> testInt)
        {
            //return testInt?.Count > 0;
            return testInt?.Count != 0;
        }

        private bool IsNotEmpty2(List<int> testInt)
        {
            return testInt?.Count > 0;
        }

        private bool IsEmpty(List<int> testInt)
        {
            return testInt?.Count == 0;
        }

        [TestMethod]
        public void TestHashSetIsEmpty()
        {
            HashSet<int> testInt = null;
            Assert.IsTrue(testInt.IsEmpty());

            HashSet<int> testInt2 = new HashSet<int>();
            Assert.IsTrue(testInt2.IsEmpty());

            HashSet<int> testInt3 = new HashSet<int> { 5 };
            Assert.IsFalse(testInt3.IsEmpty());

            HashSet<string> testStr = new HashSet<string> { "Test" };
            Assert.IsFalse(testStr.IsEmpty());
        }

        [TestMethod]
        public void TestSortedSetIsEmpty()
        {
            SortedSet<int> testInt = null;
            Assert.IsTrue(testInt.IsEmpty());

            SortedSet<int> testInt2 = new SortedSet<int>();
            Assert.IsTrue(testInt2.IsEmpty());

            SortedSet<int> testInt3 = new SortedSet<int> { 5 };
            Assert.IsFalse(testInt3.IsEmpty());

            SortedSet<string> testStr = new SortedSet<string> { "Test" };
            Assert.IsFalse(testStr.IsEmpty());
        }

        [TestMethod]
        public void TestDictionaryIsEmpty()
        {
            Dictionary<int, int> testInt = null;
            Assert.IsTrue(testInt.IsEmpty());

            Dictionary<int, int> testInt2 = new Dictionary<int, int>();
            Assert.IsTrue(testInt2.IsEmpty());

            Dictionary<int, int> testInt3 = new Dictionary<int, int> { [5] = 5 };
            Assert.IsFalse(testInt3.IsEmpty());

            Dictionary<string, string> testStr = new Dictionary<string, string> { ["TestKey"] = "TestValue" };
            Assert.IsFalse(testStr.IsEmpty());
        }


        [TestMethod]
        public void TestSortedDictionaryIsEmpty()
        {
            SortedDictionary<int, int> testInt = null;
            Assert.IsTrue(testInt.IsEmpty());

            SortedDictionary<int, int> testInt2 = new SortedDictionary<int, int>();
            Assert.IsTrue(testInt2.IsEmpty());

            SortedDictionary<int, int> testInt3 = new SortedDictionary<int, int> { [5] = 5 };
            Assert.IsFalse(testInt3.IsEmpty());

            SortedDictionary<string, string> testStr = new SortedDictionary<string, string> { ["TestKey"] = "TestValue" };
            Assert.IsFalse(testStr.IsEmpty());
        }

        [TestMethod]
        public void TestStackIsEmpty()
        {
            Stack<int> testInt = null;
            Assert.IsTrue(testInt.IsEmpty());

            Stack<int> testInt2 = new Stack<int>();
            Assert.IsTrue(testInt2.IsEmpty());

            Stack<int> testInt3 = new Stack<int>();
            testInt3.Push(5);
            Assert.IsFalse(testInt3.IsEmpty());

            Stack<string> testStr = new Stack<string>();
            testStr.Push("Test");
            Assert.IsFalse(testStr.IsEmpty());
        }

        [TestMethod]
        public void TestQueueIsEmpty()
        {
            Queue<int> testInt = null;
            Assert.IsTrue(testInt.IsEmpty());

            Queue<int> testInt2 = new Queue<int>();
            Assert.IsTrue(testInt2.IsEmpty());

            Queue<int> testInt3 = new Queue<int>();
            testInt3.Enqueue(5);
            Assert.IsFalse(testInt3.IsEmpty());

            Queue<string> testStr = new Queue<string>();
            testStr.Enqueue("Test");
            Assert.IsFalse(testStr.IsEmpty());
        }
    }
}
