using System;

// Клонирование ассоциации происходит поверхностно.

namespace Creational.Prototype.Example6
{
    class A2 { public int a = 1; }

    class Prototype2 : ICloneable
    {
        public A2 A = new A2();

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
