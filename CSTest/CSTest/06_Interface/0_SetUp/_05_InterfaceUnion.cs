using System.Diagnostics;

namespace CSTest._06_Interface._0_Setup
{
    // Наследование от интерфейсов у которых совпадают имена членов.
    // Объединение реализации одноименных абстрактных членов.
    interface _051_IInterface
    {
        void Method();
    }

    interface _052_IInterface
    {
        void Method();
    }

    class _05_InterfaceUnion : _051_IInterface, _052_IInterface
    {
        public void Method()
        {
            Debug.WriteLine("_05_InterfaceUnion.Method");
        }
    }
}
