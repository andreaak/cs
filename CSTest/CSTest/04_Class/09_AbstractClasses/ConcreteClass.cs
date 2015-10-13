using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest._04_Class._09_AbstractClasses
{
    // Конкретный класс.
    class ConcreteClass : AbstractClass
    {
        string s = "FIRST";

        // Конструктор (отрабатывает вторым).
        public ConcreteClass()
        {
            Console.WriteLine("3 ConcreteClass()");
            s = "SECOND";
        }

        public override void Method()
        {
            Console.WriteLine("Implementation");
        }

        // Переопределяем виртуальный метод VirtualMethod() базового абстрактного класса.
        // Если мы не переопределим виртуальный метод, то будет использован метод из базового класса.

        public override void VirtualMethod()
        {
            Console.WriteLine("DerivedClass.VirtualMethod();");
        }

        // Реализуем абстрактный метод AbstractMethod() базового абстрактного класса.

        public override void AbstractMethod()
        {
            Console.WriteLine("Реализация метода AbstractMethod() в ConcreteClass  {0}", s);
        }
    }
}
