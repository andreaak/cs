using System;

// Граф наследования клонируется глубоко.

namespace Creational.Prototype.Example6
{
    class A1 { public int a = 1; }


    class Prototype1 : A1, ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
