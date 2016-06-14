using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSTest._10_Collections._02_GenericCollections
{
    /*
    Коллекция представляет собой набор объектов схожих типов, сгруппированных вместе.  
    Емкость коллекции — это число элементов, которое она может содержать.  
    Количество элементов коллекции — это число элементов, которое она реально содержит. 
    Нижняя граница коллекции — это индекс ее первого элемента. 
    
    Коллекции стоит применять, если:
    - Отдельные элементы используются для одинаковых целей и одинаково важны.
    - На момент компиляции число элементов неизвестно или не зафиксировано.
    - Необходима поддержка операции перебора всех элементов.
    - Необходима поддержка упорядочивания элементов.
    - Необходимо использовать элементы из библиотеки, от которой 
        потребитель ожидает наличия типа коллекции.
    */

    [TestClass]
    public partial class _01_List
    {
        const int Iterations = 1000;

        [TestMethod]
        public void TestList1GrowList()
        {
            List<int> lst = new List<int>();
            for (int i = 0; i < Iterations; i++)
            {
                lst.Add(i);
                Debug.WriteLine("Capacity {0} Count {1}", lst.Capacity, lst.Count);
            }
            Debug.WriteLine("");
            for (int i = Iterations - 1; i >= 0; i--)
            {
                lst.Remove(i);
                Debug.WriteLine("Capacity {0} Count {1}", lst.Capacity, lst.Count);
            }

            /*
            Capacity 4 Count 1
            Capacity 4 Count 2
            Capacity 4 Count 3
            Capacity 4 Count 4
            Capacity 8 Count 5
            Capacity 8 Count 6
            Capacity 8 Count 7
            Capacity 8 Count 8
            Capacity 16 Count 9
            Capacity 16 Count 10
            Capacity 16 Count 11
            Capacity 16 Count 12
            Capacity 16 Count 13
            Capacity 16 Count 14
            Capacity 16 Count 15
            Capacity 16 Count 16
            Capacity 32 Count 17

            Capacity 1024 Count 16
            Capacity 1024 Count 15
            Capacity 1024 Count 14
            Capacity 1024 Count 13
            Capacity 1024 Count 12
            Capacity 1024 Count 11
            Capacity 1024 Count 10
            Capacity 1024 Count 9
            Capacity 1024 Count 8
            Capacity 1024 Count 7
            Capacity 1024 Count 6
            Capacity 1024 Count 5
            Capacity 1024 Count 4
            Capacity 1024 Count 3
            Capacity 1024 Count 2
            Capacity 1024 Count 1
            Capacity 1024 Count 0
            */
        }

        [TestMethod]
        public void TestList2GrowList()
        {
            List<int> lst = new List<int>();
            Debug.WriteLine("Initial - Capacity {0} Count {1}", lst.Capacity, lst.Count);
            for (int i = 0; i < 35; i++)
            {
                lst.Add(i);
            }
            Debug.WriteLine("Base List - Capacity {0} Count {1}", lst.Capacity, lst.Count);
            List<int> lst2 = new List<int>();
            lst2.AddRange(lst);
            Debug.WriteLine("Capacity {0} Count {1}", lst2.Capacity, lst2.Count);

            /*
            Initial - Capacity 0 Count 0
            Base List - Capacity 64 Count 35
            Capacity 35 Count 35
            */
        }
    }
}
