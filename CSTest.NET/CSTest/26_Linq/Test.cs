using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSTest._26_Linq
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestLinq1DeferedExecution()
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
        public void TestLinq2NotDeferedExecution()
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
        public void TestLinqSelectMany()
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
        public void TestLinq3DefaultIfEmpty()
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
        public void TestLinq4Where()
        {
            TestClass[] a =
            {
                new TestClass {Code = 1},
                new TestClass {Code = 2},
                new TestClass {Code = 3},
            };

            Func<TestClass, bool> first = (x) => 1 < x.Code;
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

        [Test]
        public void TestLinq5GroupBy()
        {
            TestClass2[] a =
            {
                new TestClass2 {Code = 1, Value = 4},
                new TestClass2 {Code = 1, Value = 7},
                new TestClass2 {Code = 2, Value = 9},
            };

            var temp = a.GroupBy(item => item.Code);
            foreach (var group in temp)
            {
                Debug.WriteLine("Key {0}, Max {1}", group.Key, group.Max(item => item.Value));
            }

            Debug.WriteLine("");
            /*
            6
            1
            77
            */
        }

        [Test]
        public void TestLinq6Nullable()
        {
            var items = new List<TestStruct?>()
            {
                new TestStruct {Code = 1, Value = 4},
                new TestStruct {Code = 1, Value = 7},
                new TestStruct {Code = 2, Value = 9},
            };

            var temp = items.FirstOrDefault(item => /*item != null && */item.Value.Code == 4);

            if (temp.HasValue)
            {

            }

            Debug.WriteLine("");
            /*
            6
            1
            77
            */
        }


        [Test]
        public void TestLinq7OrderBy()
        {
            int[] a = { 1, 2, 3, 4, 5 };

            var temp = a.Select(x =>
            {
                Debug.WriteLine("X");
                return x * 2;
            });

            var temp1 = temp.OrderBy(x => x).ToList();
            var temp2 = temp.OrderByDescending(x => x).ToList();
            /*
            X
            X
            X
            X
            X
            X
            X
            X
            X
            X
            */
        }

        [Test]
        public void TestLinq8SequenceEquals()
        {
            int[] a = { 1, 2, 3, 4, 5 };
            int[] b = { 1, 2, 3, 4, 5 };
            int[] c = { 5, 3, 4, 2, 1 };

            bool res = a.SequenceEqual(b);
            Debug.WriteLine(res);
            res = a.SequenceEqual(c);
            Debug.WriteLine(res);
            /*
            false
            */
        }
    }

    class TestClass
    {
        public int Code;
    }

    class TestClass2
    {
        public int Code;
        public int Value;
    }

    struct TestStruct
    {
        public int Code;
        public int Value;
    }
}
