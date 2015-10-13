using System.Diagnostics;

namespace CSTest._06_Interface
{
    // Наследование интерфейса от интерфейса, у которых совпадают имена членов.
    interface IInterface3
    {
        void Method();
    }

    interface IInterface4 : IInterface3
    {
        // Без new - ошибки не будет, но будет предупреждение компилятора.
        new void Method();
    }

    class _04_InterfaceInheritance : IInterface4
    {
        public void Method()
        {
            Debug.WriteLine("Method - реализация _04_InterfaceInheritance");
        }
        
        void IInterface3.Method()
        {
            Debug.WriteLine("Method - реализация интерфейса IInterface3");
        }

        void IInterface4.Method()
        {
            Debug.WriteLine("Method - реализация интерфейса IInterface4");
        }
    }
}
