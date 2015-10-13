using System.Diagnostics;

namespace CSTest._06_Interface
{
    interface IInterface
    {
        void Method();
    }

    class _01_Base : IInterface
    {
        public void Method()
        {
            Debug.WriteLine("Метод - реализация Интерфейса.");
        }
    }
}
