using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._04_CS7
{
    [TestFixture]
    public class _02_LocalFunction
    {
#if CS7
        [Test]
        public void TestLocalFunction_1()
        {
            MyNumbers();

            // Local Functions
            void MyNumbers()
            {
                int myFavNumber3 = 0b1111111111;
                int myFavNumber4 = 0b1111_1111_11;
                int OneMillion = 1_000_000;
                Debug.WriteLine($"These are my fav numbers: " +
                 $"{myFavNumber3}, {myFavNumber4}, { OneMillion}");
            }

            int AddTen(int n)
            {
                return n + 10;
            }
            int start = 6;
            Debug.WriteLine($"total of 10 + {start} = " + AddTen(start));
            /*
            These are my fav numbers: 1023, 1023, 1000000
            total of 10 + 6 = 16 
            */
        }

        [Test]
        public void TestLocalFunction_2()
        {
            //MyNumbers();
        }

        [Test]
        public void TestLocalFunction_Recursion()
        {
            int myValue = 1;
            int Calc(int n) => (n < 2) ? myValue : Calc(n - 1) + Calc(n - 2);
            Debug.WriteLine(Calc(8));
        }

        [Test]
        public void TestLocalFunction_Iterator()
        {

        }

        public IEnumerable<T> Filter<T>(IEnumerable<T> source, Func<T, bool> filter)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            return Iterator();

            IEnumerable<T> Iterator()
            {
                foreach (var element in source)
                {
                    if (filter(element)) { yield return element; }
                }
            }
        }
#endif
    }
}
