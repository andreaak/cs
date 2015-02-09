using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.HashTable
{
    [TestClass]
    public class HashTableTest
    {
        [TestMethod]
        public void TestAdd()
        {
            HashTableArray<int, string> instance = new HashTableArray<int, string>(5);

            instance.Add(0, "Hello");
            instance.Add(1, "World");
            instance.Add(2, "Again");
            instance.Add(3, "!");

            foreach (var item in instance)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestUpdate()
        {
            HashTableArray<int, string> instance = new HashTableArray<int, string>(5);

            instance.Add(0, "Hello");
            instance.Add(1, "World");
            instance.Add(2, "Again");
            instance.Add(3, "!");

            instance.Update(2, "Change");

            foreach (var item in instance)
            {
                Debug.WriteLine(item.Value);
            }
        }

        [TestMethod]
        public void TestTryGetValue()
        {
            HashTableArray<int, string> instance = new HashTableArray<int, string>(5);

            instance.Add(0, "Hello");
            instance.Add(1, "World");
            instance.Add(2, "Again");
            instance.Add(3, "!");

            string str;

            instance.TryGetValue(2, out str);

            Debug.WriteLine(str);

            Debug.WriteLine("====================");

            foreach (var item in instance)
            {
                Debug.WriteLine(item.Value);
            }
        }

        [TestMethod]
        public void TestRemove()
        {
            HashTableArray<int, string> instance = new HashTableArray<int, string>(5);

            instance.Add(0, "Hello");
            instance.Add(1, "World");
            instance.Add(2, "Again");
            instance.Add(3, "!");
            instance.Remove(3);

            foreach (var item in instance)
            {
                Debug.WriteLine(item.Value);
            }
        }

        [TestMethod]
        public void TestClear()
        {
            HashTable<int, string> instance = new HashTable<int, string>(5);

            instance.Add(0, "Hello");
            instance.Add(1, "World");
            instance.Add(2, "Again");
            instance.Add(3, "!");
            instance.Add(4, "!");

            foreach (var item in instance)
            {
                Debug.WriteLine(item.Value);
            }
        }
    }
}
