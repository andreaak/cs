using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Set
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void TestAdd()
        {
            Set<int> instance = new Set<int>();

            instance.Add(1);
            instance.Add(2);
            instance.Add(3);
            instance.Add(4);
            instance.Add(5);
            instance.Add(5);

            foreach (var item in instance)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestAddRange()
        {
            int[] array = { 10, 20, 30, 40, 50 };

            Set<int> instance = new Set<int>();

            instance.Add(1);
            instance.Add(2);
            instance.Add(3);

            instance.AddRange(array);

            foreach (var item in instance)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestRemove()
        {
            Set<int> instance = new Set<int>();

            instance.Add(1);
            instance.Add(2);
            instance.Add(3);
            instance.Add(4);
            instance.Add(5);

            instance.Remove(10);

            foreach (var item in instance)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestUnion()
        {
            int[] array = { 10, 20, 30, 40, 50 };

            Set<int> set1 = new Set<int>();
            Set<int> set2 = new Set<int>(array);
            Set<int> set3 = new Set<int>();

            set1.Add(1);
            set1.Add(2);
            set1.Add(3);
            set1.Add(4);
            set1.Add(5);

            set3 = set1.Union(set2);

            foreach (var item in set3)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestIntersection()
        {
            int[] array = { 3, 4, 5, 6 };

            Set<int> set1 = new Set<int>();
            Set<int> set2 = new Set<int>(array);
            Set<int> set3 = new Set<int>();

            set1.Add(1);
            set1.Add(2);
            set1.Add(3);
            set1.Add(4);

            set3 = set1.Intersection(set2);

            foreach (var item in set3)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestDifference()
        {
            int[] array = { 3, 4, 5, 6 };

            Set<int> set1 = new Set<int>();
            Set<int> set2 = new Set<int>(array);
            Set<int> set3 = new Set<int>();

            set1.Add(1);
            set1.Add(2);
            set1.Add(3);
            set1.Add(4);

            set3 = set1.Difference(set2);

            foreach (var item in set3)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestSymmetricDifference()
        {
            int[] array = { 3, 4, 5, 6 };

            Set<int> set1 = new Set<int>();
            Set<int> set2 = new Set<int>(array);
            Set<int> set3 = new Set<int>();

            set1.Add(1);
            set1.Add(2);
            set1.Add(3);
            set1.Add(4);

            set3 = set1.SymmetricDifference(set2);

            foreach (var item in set3)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestIsSubset()
        {
            bool rez;

            int[] array = { 1, 2, 3, 4, 5, 6 };

            Set<int> set1 = new Set<int>();
            Set<int> set2 = new Set<int>(array);

            set1.Add(1);
            set1.Add(2);
            set1.Add(3);
            set1.Add(4);
            // set1.Add(7); //Расскоментировать!

            rez = set1.IsSubset(set2);

            Debug.WriteLine("Множество set1 является подмножеством множества set2: {0} ", rez);
        }
    }
}
