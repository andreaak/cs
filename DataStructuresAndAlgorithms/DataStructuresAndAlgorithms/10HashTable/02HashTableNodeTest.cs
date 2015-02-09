using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.HashTable
{
    [TestClass]
    public class HashTableNodeTest
    {
        [TestMethod]
        public void Test1()
        {
            HashTablePair<int, string> instance1 = new HashTablePair<int, string>(0, "Hello");
            HashTablePair<int, string> instance2 = new HashTablePair<int, string>(1, "World");

            Debug.WriteLine("Первое значение в таблице  {0}", instance1.Value);
            Debug.WriteLine("Второе значение в таблице {0}", instance2.Value);
        }
        
        [TestMethod]
        public void TestAdd()
        {
            HashTableNode<int, string> instanse = new HashTableNode<int, string>();

            instanse.Add(0, "Hello");
            instanse.Add(1, "World");
            instanse.Add(2, "!");

            foreach (var item in instanse)
            {
                Debug.WriteLine(item);
            } 
        }

        [TestMethod]
        public void TestUpdate()
        {
            HashTableNode<int, string> instanse = new HashTableNode<int, string>();

            instanse.Add(0, "Hello");
            instanse.Add(1, "World");
            instanse.Add(2, "!");

            instanse.Update(2, ":)");

            foreach (var item in instanse)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestTryGetValue()
        {
            HashTableNode<int, string> instanse = new HashTableNode<int, string>();

            instanse.Add(0, "Hello");
            instanse.Add(1, "World");
            instanse.Add(2, "!");

            string str;

            instanse.TryGetValue(1, out str);
            Debug.WriteLine(str);

            Debug.WriteLine("======================================");

            foreach (var item in instanse)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestRemove()
        {
            HashTableNode<int, string> instanse = new HashTableNode<int, string>();

            instanse.Add(0, "Hello");
            instanse.Add(1, "World");
            instanse.Add(2, "!");

            instanse.Remove(1);

            foreach (var item in instanse)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestClear()
        {
            HashTableNode<int, string> instanse = new HashTableNode<int, string>();

            instanse.Add(0, "Hello");
            instanse.Add(1, "World");
            instanse.Add(2, "!");

            instanse.Clear();
            //очистить массив элементов

            foreach (var item in instanse)
            {
                Debug.WriteLine(item);
                Debug.WriteLine("=====");
            }
        }
    }
}
