using System;
using System.Collections;
using System.Diagnostics;
using NUnit.Framework;

namespace CSTest._10_Collections._03_Iterators
{
    [TestFixture]
    class _01_Foreach
    {
        [Test]
        public void TestForeach()
        {
            char[] chrs = { 'А', 'В', 'C', 'D' };
            foreach (var item in chrs)
            {
                Debug.WriteLine(item);
            }
        }

        //Внутренняя реализация цикла foreach
        public static void ForEachIEnumerable(IEnumerable sequence)
        {
            //foreach (var e in sequence)
            //{
            //    Debug.WriteLine(e);
            //}
            IEnumerator enumerator = sequence.GetEnumerator();
            object current = null;
            try
            {

                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    Debug.WriteLine(current);
                }
            }

            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
