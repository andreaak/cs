using System.Diagnostics;

namespace CSTest._06_Interface
{
    // Наследование абстрактных классов от интерфейсов.
    interface Interface
    {
        void Method();

        void Method2();
    }

    abstract class AbstractClass : Interface
    {
        // Реализация абстрактного метода из интерфейса, в абстрактном классе обязательна.
        public void Method()
        {
            Debug.WriteLine("Метод - реализация интерфейса в абстрактном классе.");
        }

        // Замещение абстрактного метода из интерфейса, в абстрактном классе обязательно.
        // Можно объявить метод интерфейса как абстрактный.
        public abstract void Method2();
    }

    class _06_AbstractClass : AbstractClass
    {
        // Реализация абстрактного метода из абстрактного класса, в конкретном классе обязательна.
        public override void Method2()
        {
            Debug.WriteLine("Метод - реализация интерфейса в конкретном классе."); ;
        }
    }
}
