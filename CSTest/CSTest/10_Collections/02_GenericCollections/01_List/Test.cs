using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CSTest._10_Collections._02_GenericCollections._01_List
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

    [TestFixture]
    public class _01_List
    {
        const int Iterations = 1000;

        [Test]
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

        [Test]
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

        [Test]
        public void TestList3ToList()
        {
            IList<string> lst = new string[] {"One", "Two", "Three"};
            IList<string> lst2 = lst.ToList();
            Debug.WriteLine("Ref Equals lst lst2 : " + ReferenceEquals(lst, lst2));

            List<string> lst3 = new List<string> {"One", "Two", "Three"};
            List<string> lst4 = lst.ToList();
            Debug.WriteLine("Ref Equals lst3 lst4 : " + ReferenceEquals(lst3, lst4));

            List<string> lst5 = lst4.ToList();
            Debug.WriteLine("Ref Equals lst3 lst5 : " + ReferenceEquals(lst3, lst5));
            Debug.WriteLine("Ref Equals lst4 lst5 : " + ReferenceEquals(lst4, lst5));


            IEnumerable<string> empty = Enumerable.Empty<string>();
            IEnumerable<string> empty2 = Enumerable.Empty<string>();
            Debug.WriteLine("Ref Equals empty empty2 : " + ReferenceEquals(empty, empty2));

            /*
            Ref Equals lst lst2 : False
            Ref Equals lst3 lst4 : False
            Ref Equals lst3 lst5 : False
            Ref Equals lst4 lst5 : False
            Ref Equals empty empty2 : True
            */
        }

        [Test]
        public void TestList4Cast()
        {

            List<string> lst3 = new List<string> { "One", "Two", "Three" };
            Debug.WriteLine("lst3 Count : " + lst3.Count);
            IEnumerable<string> lst4 = lst3;
            List<string> lst5 = lst4 as List<string>;
            if(lst5 != null)
            {
                lst5.Add("Four");
            }

            Debug.WriteLine("lst3 Count : " + lst3.Count);

            List<int> lst6 = new List<int> { 1, 2, 3 };
            IEnumerable<int> lst7 = lst6;
            //IEnumerable<object> lst8 = lst6;//Error CS0266  Cannot implicitly convert type 'System.Collections.Generic.List<int>' to 'System.Collections.Generic.IEnumerable<object>'.
                                            //An explicit conversion exists (are you missing a cast?)  CSTest D:\My\cs\CSTest\CSTest\10_Collections\02_GenericCollections\_01_List.cs	148	Active


            if (lst3.Count != 0)
            {
                Debug.WriteLine(lst3.Count);
            }

            if (lst3.Count == 0)
            {
                Debug.WriteLine(lst3.Count);
            }

            /*
            lst3 Count : 3
            lst3 Count : 4
            */
        }

        [Test]
        public void TestList5Add()
        {

            IList<string> lst3 = new List<string> { "One", "Two", "Three" };
            Debug.WriteLine("lst3 Count : " + lst3.Count);
            lst3.Add("Four");
            Debug.WriteLine("lst3 Count : " + lst3.Count);

            IList<string> lst4 = new List<string> { "One", "Two", "Three" }.ToArray();
            lst4.First();
            //lst4.Add("Four");//An exception of type 'System.NotSupportedException' occurred in mscorlib.dll but was not handled in user code
            //Debug.WriteLine("lst3 Count : " + lst3.Count);
            /*
            lst3 Count : 3
            lst3 Count : 4
            */
        }
    }
}
