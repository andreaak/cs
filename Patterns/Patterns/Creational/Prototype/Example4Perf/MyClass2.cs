using System;
using System.Diagnostics;
using System.Threading;

// Клонирование с использованием конструктора.

namespace Creational.Prototype.Example4
{
    class MyClass2 : ICloneable
    {
        int a, b;

        public MyClass2()
        {
            Thread.Sleep(1000);
            a = new Random().Next(1, 100);
            Thread.Sleep(1000);
            b = new Random().Next(1, 100);
        }

        public object Clone()
        {
            return new MyClass2();
        }
    }
}
