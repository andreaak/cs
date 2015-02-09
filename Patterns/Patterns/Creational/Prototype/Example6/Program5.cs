using System;

// Проблемы при клонировани двухсторонней ассоциации 
// (круговых ссылок - циклических зависимостей).

namespace Creational.Prototype.Example6
{
    class A5 : ICloneable
    {
        public int a = 1;
        public B5 B = null;

        public A5(B5 B)
        {
            this.B = B;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    class B5 : ICloneable
    {
        public int b = 2;
        public A5 A = null;

        public B5()
        {
            this.A = new A5(this);
        }

        public object Clone()
        {
            B5 clone = this.MemberwiseClone() as B5;
            clone.A = this.A.Clone() as A5;
            clone.A.B = clone; // Корректировка обратной ассоциации.
            return clone;
        }
    }
}
