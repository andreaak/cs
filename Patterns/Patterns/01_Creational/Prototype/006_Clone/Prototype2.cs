using System;

// Клонирование ассоциации происходит поверхностно.

namespace Patterns._01_Creational.Prototype._006_Clone
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
