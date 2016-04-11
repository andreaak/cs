using System;
using System.Diagnostics;
using System.Threading;

// Клонирование с использованием MemberwiseClone().

// Преимущество клонирования с использованием MemberwiseClone() в том, что
// при клонировании не вызывается конструктор, а клонирование происходит через
// копирование дампа памяти - тела оригинала.

namespace Creational.Prototype._004_Performance
{
    class MyClass1 : ICloneable
    {
        int a, b;

        public MyClass1()
        {
            Thread.Sleep(1000);
            a = new Random().Next(1, 100);
            Thread.Sleep(1000);
            b = new Random().Next(1, 100);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
