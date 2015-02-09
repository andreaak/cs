using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithms.Base
{
    [TestClass]
    public class SingleListTest
    {
        [TestMethod]
        public void Test1()
        {

            // +-----+------+ 
            // |  3  | null + 
            // +-----+------+

            Node first = new Node()
            {
                Value = 3           // инициализация первого (first) узла списка значением 3
            };

            // +-----+------+    +-----+------+ 
            // |  3  | null +    |  5  | null + 
            // +-----+------+    +-----+------+ 

            Node middle = new Node()
            {
                Value = 5         // инициализация второго (middle) узла списка значением 5
            };

            // Создание связи между первым и вторым узлом списка

            // +-----+------+    +-----+------+ 
            // |  3  |  *---+--->|  5  | null +
            // +-----+------+    +-----+------+ 

            first.Next = middle;

            // +-----+------+    +-----+------+   +-----+------+ 
            // |  3  |  *---+--->|  5  | null +   |  7  | null + 
            // +-----+------+    +-----+------+   +-----+------+ 

            Node last = new Node()
            {
                Value = 7
            };

            // создание связи между вторым и третьим узлом списка

            // +-----+------+    +-----+------+   +-----+------+ 
            // |  3  |  *---+--->|  5  |  *---+-->|  7  | null + 
            // +-----+------+    +-----+------+   +-----+------+ 

            middle.Next = last;

            PrintValues(first);
        }

        private static void PrintValues(Node node)
        {
            while (node != null)
            {
                Debug.WriteLine(node.Value);
                node = node.Next;
            }
        } 

    }
}
