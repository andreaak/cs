using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest._04_Class._09_AbstractClasses
{
    // Абстрактный класс может быть унаследован от конкретного класса.

    // Конкретный класс A.
    class ConcreteClassA
    {
        public void Operation()
        {
            Console.WriteLine("ConcreteClassA.Operation");
        }
    }

    // Абстрактный класс.
    abstract class AbstractClass2 : ConcreteClassA
    {
        public abstract void Method();
    }

    // Конкретный класс B.
    class ConcreteClassB : AbstractClass2
    {
        public override void Method()
        {
            Console.WriteLine("ConcreteClassB.Method");
        }
    }

    // Абстрактный класс может быть унаследован от абстрактного класса.
    // Реализация абстрактного метода из базового абстрактного класса, в производном абстрактном классе - не обязательна.

    // Абстрактный класс A.
    abstract class AbstractClassA
    {
        public abstract void OperationA();
    }

    // Абстрактный класс B.
    abstract class AbstractClassB : AbstractClassA
    {
        public abstract void OperationB();
    }

    // Конкретный класс.
    class ConcreteClass2 : AbstractClassB
    {
        public override void OperationA()
        {
            Console.WriteLine("ConcreteClass.OperationA");
        }

        public override void OperationB()
        {
            Console.WriteLine("ConcreteClass.OperationB");
        }
    }
}
