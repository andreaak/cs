using System.Diagnostics;

namespace CSTest._06_Interface._0_Setup
{
    // Наследование абстрактных классов от интерфейсов.
    interface _06_Interface
    {
        //abstract virtual
        void Method();

        void Method2();

        void Method3();
    }

    abstract class _06_AbstractClass : _06_Interface
    {
        // Реализация абстрактного метода из интерфейса, в абстрактном классе обязательна.
        // virtual final
        public void Method()
        {
            Debug.WriteLine("_06_AbstractClass.Method");
        }

        // Замещение абстрактного метода из интерфейса, в абстрактном классе обязательно.
        // Можно объявить метод интерфейса как абстрактный.
        // abstract virtual
        public abstract void Method2();

        //virtual
        public virtual void Method3()
        {
            Debug.WriteLine("_06_AbstractClass.Method3");
        }
    }

    class _06_RealClass : _06_AbstractClass
    {
        // Реализация абстрактного метода из абстрактного класса, в конкретном классе обязательна.
        //virtual
        public override void Method2()
        {
            Debug.WriteLine("_06_RealClass.Method2"); ;
        }

        public void Method4()
        {
            Debug.WriteLine("_06_RealClass.Method4");
        }
    }
}
