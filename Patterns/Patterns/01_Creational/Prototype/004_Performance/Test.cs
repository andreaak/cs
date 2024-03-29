﻿using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._01_Creational.Prototype._004_Performance
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Stopwatch timer = new Stopwatch();

            // Замер времени построения оригинала.

            timer.Start();
            MyClass1 original = new MyClass1();
            timer.Stop();
            Debug.WriteLine("original построен за {0}", timer.Elapsed.Ticks);

            timer.Reset();

            // Замер времени построения клона.

            timer.Start();
            MyClass1 clone = original.Clone() as MyClass1;
            timer.Stop();
            Debug.WriteLine("clone    построен за {0}", timer.Elapsed.Ticks);

            // Delay.
            //Debug.ReadKey();
        }


        [Test]
        public void Test2()
        {
            Stopwatch timer = new Stopwatch();

            // Замер времени построения оригинала.

            timer.Start();
            MyClass2 original = new MyClass2();
            timer.Stop();
            Debug.WriteLine("original построен за {0}", timer.Elapsed.Ticks);

            timer.Reset();

            // Замер времени построения клона.

            timer.Start();
            MyClass2 clone = original.Clone() as MyClass2;
            timer.Stop();
            Debug.WriteLine("clone    построен за {0}", timer.Elapsed.Ticks);

            // Delay.
            //Debug.ReadKey();
        }
    }
}
