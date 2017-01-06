using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace CSTest._26_Linq
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestDeferedExecution()
        {
            int[] a = { 1, 4, 3, 7, 8, 2, 9, 5, 6 };

            var b = a.Where(Filter);
            if (!b.Any())
            {

            }

            int count = b.Count();
            int c = b.First();
        }

        [Test]
        public void TestNotDeferedExecution()
        {
            int[] a = { 1, 4, 3, 7, 8, 2, 9, 5, 6 };

            var b = a.Where(Filter).ToArray();
            if (!b.Any())
            {

            }

            int count = b.Count();
            int c = b.First();
        }

        private bool Filter(int arg)
        {
            return arg > 5;
        }

        [Test]
        public void TestSelectMany()
        {
            int[] a = { 1, 2, 3, 4, 5 };
            int[] b = { 6, 7, 8, 9, 10 };
            int[] d = { };

            int[][] c = { a, b, d };
            Filter(c);
            Filter(a, b, d);
        }

        private void Filter(params int[][] arg)
        {
            var a = arg.SelectMany(arr => arr).ToArray();
            foreach (var item in a)
            {
                Debug.WriteLine(item);
            }
        }

        [Test]
        public void TestDefaultIfEmpty()
        {
            int[] a = { 1, 2, 3, 4, 5 };
            int[] b = { 6, 7, 8, 9, 10 };
            int[] d = { };

            int[][] c = { a, b, d };

            var res = b.DefaultIfEmpty(66).First();
            Debug.WriteLine(res);
            object[] e = { 1, 2, 3, 4, 5 };
            var res2 = e.Cast<int>().DefaultIfEmpty(55).First();
            Debug.WriteLine(res2);
            object[] f = { };
            var res3 = f.Cast<int>().DefaultIfEmpty(77).First();
            Debug.WriteLine(res3);
            /*
            6
            1
            77
            */
        }

        [Test]
        public void Test1()
        {
            TestClass[] a =
            {
                new TestClass {Code = 1},
                new TestClass {Code = 2},
                new TestClass {Code = 3},
            };

            Func<TestClass, bool> first = (x) =>  1 < x.Code;
            Func<TestClass, bool> second = (x) => x.Code < 3;

            Func<TestClass, bool> res = (x) => first(x) && second(x);
            var test = a.Where(res).ToArray();

            Debug.WriteLine("");
            /*
            6
            1
            77
            */
        }
    }

    class TestClass
    {
        public int Code;
    }
}
