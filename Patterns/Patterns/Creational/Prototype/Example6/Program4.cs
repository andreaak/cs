using System;

// Проблемы при клонировани двухсторонней ассоциации 
// (круговых ссылок - циклических зависимостей).

namespace Creational.Prototype.Example6
{
    class A4 : ICloneable
    {
        public int a = 1;
        public B4 B = null;

        public A4(B4 B)
        {
            this.B = B;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    class B4 : ICloneable
    {
        public int b = 2;
        public A4 A = null;

        public B4()
        {
            this.A = new A4(this);
        }

        public object Clone()
        {
            B4 clone = this.MemberwiseClone() as B4;
            clone.A = this.A.Clone() as A4;
            return clone;
        }
    }
}
