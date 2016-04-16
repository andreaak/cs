using System;

// Граф наследования клонируется глубоко.

namespace Patterns._01_Creational.Prototype._006_Clone
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
