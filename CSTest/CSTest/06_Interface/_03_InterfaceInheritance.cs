using System.Diagnostics;

namespace CSTest._06_Interface
{
    // Наследование интерфейса от интерфейса.
    // Узкий интерфейс
    interface IInterface1
    {
        void Method1();
    }
    // Широкий интерфейс
    interface IInterface2 : IInterface1
    {
        void Method2();
    }

    class _03_InterfaceInheritance : IInterface2
    {
        public void Method1()
        {
            Debug.WriteLine("Method1 - реализация интерфейса IInterface1");
        }

        public void Method2()
        {
            Debug.WriteLine("Method2 - реализация интерфейса IInterface2");
        }
    }
}
