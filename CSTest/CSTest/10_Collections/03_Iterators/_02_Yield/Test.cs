using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CSTest._10_Collections._03_Iterators._02_Yield
{
    [TestClass]
    public partial class Test
    {
        [TestMethod]
        public void TestYield1()
        {
            _01_Yield mc = new _01_Yield();
            foreach (char ch in mc)
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");
            foreach (char ch2 in mc.IterateEnumerable())
            {
                Debug.Write(ch2 + " ");
            }
            Debug.WriteLine("");

            /*
            yield return
            A yield return
            B yield return
            C yield return
            D 
            yield return
            A yield return
            B yield return
            C yield return
            D 
            */

            Debug.WriteLine("Возвратить по очереди первые 7 букв:");
            foreach (char ch in mc.IterateEnumerable(7))
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("\n");
            Debug.WriteLine("Возвратить по очереди буквы от F до L:");
            foreach (char ch in mc.IterateEnumerable(5, 12))
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");
          
            /*
            Возвратить по очереди первые 7 букв:
            A B C D E F G 

            Возвратить по очереди буквы от F до L:
            F G H I J K L  
            */
        }

        [TestMethod]
        public void TestYield2()
        {
            _02_YieldBreak mc = new _02_YieldBreak();
            foreach (char ch in mc)
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");
            /*
            yield return
            A yield return
            B yield return
            C yield return
            D yield return
            E yield return
            F yield return
            G yield return
            H yield return
            I yield return
            J yield break
            */
        }

        [TestMethod]
        public void TestYield3()
        {
            _03_YieldMulti mc = new _03_YieldMulti();
            foreach (char ch in mc)
            {
                Debug.Write(ch + " ");
            }
            Debug.WriteLine("");
            /*
            GetEnumerator
            A yield return
            В yield return
            C yield return
            D yield return
            Е yield return last 
            */
        }

        [TestMethod]
        public void TestYield4()
        {
            int[] nums = { 4, 3, 6, 4, 7, 9 };
            _04_YieldGeneric<int> mc = new _04_YieldGeneric<int>(nums);
            foreach (int x in mc)
            {
                Debug.Write(x + " ");
            }
            Debug.WriteLine("");

            foreach (int x in mc.IterateEnumerable())
            {
                Debug.Write(x + " ");
            }
            Debug.WriteLine("");

            bool[] bVals = { true, true, false, true };
            _04_YieldGeneric<bool> mc2 = new _04_YieldGeneric<bool>(bVals);
            foreach (bool b in mc2)
            {
                Debug.Write(b + " ");
            }
            Debug.WriteLine("");
            /*
            4 3 6 4 7 9 
            4 3 6 4 7 9 
            True True False True 
            */
        }

        [TestMethod]
        public void TestYield5()
        {
            _01_Yield mc = new _01_Yield();
            foreach (char ch in mc.IterateEnumerable(10))
            {
                Debug.WriteLine(ch + " ");
            }
            Debug.WriteLine("");
            foreach (char ch2 in mc.IterateEnumerableAll(10))
            {
                Debug.Write(ch2 + " ");
            }
            Debug.WriteLine("");
            /*
            yield return A
            A 
            yield return B
            B 
            yield return C
            C 
            yield return D
            D 
            yield return E
            E 
            yield return F
            F 
            yield return G
            G 
            yield return H
            H 
            yield return I
            I 
            yield return J
            J 

            Value A
            Value B
            Value C
            Value D
            Value E
            Value F
            Value G
            Value H
            Value I
            Value J
            A B C D E F G H I J 
            */
        }

        [TestMethod]
        public void TestYield6()
        {

            char[] chars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M' };
            _04_YieldGeneric<char> mc = new _04_YieldGeneric<char>(chars);
            mc.IterateEnumerable(10).First();
            Debug.WriteLine("");
            mc.IterateEnumerableAll(10).First();
            /*
            yield return A

            Value A
            Value B
            Value C
            Value D
            Value E
            Value F
            Value G
            Value H
            Value I
            Value J
            */
        }
    }
}
