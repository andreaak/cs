using System;
using System.Diagnostics;

namespace CSTest._04_Class._09_AbstractClasses
{
    // Абстрактный класс.
    abstract class AbstractClass
    {
        // Конструктор (отрабатывает первым).
        public AbstractClass()
        {
            Debug.WriteLine("1 AbstractClass()");

            // Вызывается реализация метода из производного класса - ConcreteClass.
            this.AbstractMethod();

            Debug.WriteLine("2 AbstractClass()");
        }

        public abstract void Method();
        public abstract event EventHandler Changed;
        //public abstract delegate void TestDelegate(); //The modifier 'abstract' is not valid for this item

        // 1.
        // Обычный метод передается производному классу как при наследовании от конкретного класса.
        public void SimpleMethod()
        {
            Debug.WriteLine("AbstractBaseClass.SimpleMethod");
        }

        // 2.
        // Виртуальный метод передается производному классу как при наследовании от конкретного класса.
        virtual public void VirtualMethod()
        {
            Debug.WriteLine("AbstractBaseClass.VirtualMethod");
        }

        // 3.
        // Абстрактный метод - реализуется в производном классе.
        abstract public void AbstractMethod();
    }
}
