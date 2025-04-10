using System.Diagnostics;

namespace CSTest._04_Class._09_AbstractClasses
{
    // Конкретный класс A.
    class ConcreteClassA
    {
        public void Operation()
        {
            Debug.WriteLine("ConcreteClassA.Operation");
        }
    }

    // Абстрактный класс может быть унаследован от конкретного класса.
    abstract class AbstractClass2 : ConcreteClassA
    {
        public abstract void Method();
    }

    // Конкретный класс B.
    class ConcreteClassB : AbstractClass2
    {
        public override void Method()
        {
            Debug.WriteLine("ConcreteClassB.Method");
        }
    }

    /*
    Абстрактный класс может быть унаследован от абстрактного класса.
    Реализация абстрактного метода из базового абстрактного класса, 
    в производном абстрактном классе - не обязательна.
    */
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
            Debug.WriteLine("ConcreteClass.OperationA");
        }

        public override void OperationB()
        {
            Debug.WriteLine("ConcreteClass.OperationB");
        }
    }
}
