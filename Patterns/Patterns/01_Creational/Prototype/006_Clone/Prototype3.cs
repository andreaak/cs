using System;

// Клонирование ассоциации.

namespace Patterns._01_Creational.Prototype._006_Clone
{
    class A3 : ICloneable
    {
        public int a = 1;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    class Prototype3 : ICloneable
    {
        public A3 A = new A3();

        public object Clone()
        {
            Prototype3 clone = this.MemberwiseClone() as Prototype3;
            clone.A = this.A.Clone() as A3;
            return clone;
        }
    }
}
