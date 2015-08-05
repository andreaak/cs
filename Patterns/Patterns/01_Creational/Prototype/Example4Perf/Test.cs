using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Creational.Prototype.Example4
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Stopwatch timer = new Stopwatch();

            // Замер времени построения оригинала.

            timer.Start();
            MyClass1 original = new MyClass1();
            timer.Stop();
            Console.WriteLine("original построен за {0}", timer.Elapsed.Ticks);

            timer.Reset();

            // Замер времени построения клона.

            timer.Start();
            MyClass1 clone = original.Clone() as MyClass1;
            timer.Stop();
            Console.WriteLine("clone    построен за {0}", timer.Elapsed.Ticks);

            // Delay.
            //Console.ReadKey();
        }


        [TestMethod]
        public void Test2()
        {
            Stopwatch timer = new Stopwatch();

            // Замер времени построения оригинала.

            timer.Start();
            MyClass2 original = new MyClass2();
            timer.Stop();
            Console.WriteLine("original построен за {0}", timer.Elapsed.Ticks);

            timer.Reset();

            // Замер времени построения клона.

            timer.Start();
            MyClass2 clone = original.Clone() as MyClass2;
            timer.Stop();
            Console.WriteLine("clone    построен за {0}", timer.Elapsed.Ticks);

            // Delay.
            //Console.ReadKey();
        }
    }
}
