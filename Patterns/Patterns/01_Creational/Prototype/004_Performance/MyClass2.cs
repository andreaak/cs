using System;
using System.Threading;

// Клонирование с использованием конструктора.

namespace Patterns._01_Creational.Prototype._004_Performance
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
