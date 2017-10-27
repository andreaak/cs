using System.Diagnostics;

namespace CSTest._06_Interface._0_Setup
{
    // Наследование интерфейса от интерфейса.
    // Узкий интерфейс
    interface _41_1IInterface
    {
        void Method1();
    }
    // Широкий интерфейс
    interface _41_2IInterface : _41_1IInterface
    {
        void Method2();
    }

    class _041_InterfaceInheritance : _41_2IInterface
    {
        public void Method1()
        {
            Debug.WriteLine("_041_InterfaceInheritance.Method1");
        }

        public void Method2()
        {
            Debug.WriteLine("_041_InterfaceInheritance.Method2");
        }
    }

    // Наследование интерфейса от интерфейса, у которых совпадают имена членов.
    interface _042_1_IInterface
    {
        void Method();
    }

    interface _042_2_IInterface : _042_1_IInterface
    {
        // Без new - ошибки не будет, но будет предупреждение компилятора.
        new void Method();
    }

    class _042_InterfaceInheritanceSameMethods : _042_2_IInterface
    {
        public void Method()
        {
            Debug.WriteLine("_042_InterfaceInheritanceSameMethods.Method");
        }
        
        void _042_1_IInterface.Method()
        {
            Debug.WriteLine("IInterface42_1.Method");
        }

        void _042_2_IInterface.Method()
        {
            Debug.WriteLine("IInterface42_2.Method");
        }
    }
}
