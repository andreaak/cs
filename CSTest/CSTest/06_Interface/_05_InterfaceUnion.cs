using System.Diagnostics;

namespace CSTest._06_Interface
{
    // Наследование от интерфейсов у которых совпадают имена членов.
    // Объединение реализации одноименных абстрактных членов.
    interface IInterface5
    {
        void Method();
    }

    interface IInterface6
    {
        void Method();
    }

    class _05_InterfaceUnion : IInterface5, IInterface6
    {
        public void Method()
        {
            Debug.WriteLine("Method - реализация интерфейса IInterface (5-6)");
        }
    }
}
