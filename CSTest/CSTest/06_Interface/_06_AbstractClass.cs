using System.Diagnostics;

namespace CSTest._06_Interface
{
    // Наследование абстрактных классов от интерфейсов.
    interface Interface
    {
        //abstract virtual
        void Method();

        void Method2();
        
        void Method3();
    }

    abstract class AbstractClass : Interface
    {
        // Реализация абстрактного метода из интерфейса, в абстрактном классе обязательна.
        // virtual final
        public void Method()
        {
            Debug.WriteLine("Метод - реализация интерфейса в абстрактном классе.");
        }

        // Замещение абстрактного метода из интерфейса, в абстрактном классе обязательно.
        // Можно объявить метод интерфейса как абстрактный.
        // abstract virtual
        public abstract void Method2();

        //virtual
        public virtual void Method3()
        {
            Debug.WriteLine("Метод - реализация интерфейса в абстрактном классе.");
        }
    }

    class RealClass : AbstractClass
    {
        // Реализация абстрактного метода из абстрактного класса, в конкретном классе обязательна.
        //virtual
        public override void Method2()
        {
            Debug.WriteLine("Метод - реализация интерфейса в конкретном классе."); ;
        }

        public void Method4()
        {
            Debug.WriteLine("Метод - реализация интерфейса в абстрактном классе.");
        }
    }
}
