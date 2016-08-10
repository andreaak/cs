using System.Diagnostics;

namespace CSTest._06_Interface
{
    interface IInterface7
    {
        void Method();
    }

    interface IInterface8
    {
        void Method2();

        void Method3();
    }

    class BaseClass2 : IInterface8
    {
        public void Method()
        {
            Debug.WriteLine("BaseClass.Method()");
        }

        public void Method2()
        {
            Debug.WriteLine("BaseClass.Method2()");
        }

        public virtual void Method3()
        {
            Debug.WriteLine("BaseClass.Method3()");
        }
    }

    class _07_InterfaceWithBaseClass : BaseClass2, IInterface7
    {
        // Реализация интерфейса не обязательна, т.к., 
        // сигнатуры методов в классе и интерфейсе совпадают.

        public new void Method2()
        {
            Debug.WriteLine("DerivedClass.Method2()");
        }

        public override void Method3()
        {
            Debug.WriteLine("DerivedClass.Method3()");
        }
    }

    class _08_InterfaceWithBaseClass : BaseClass2, IInterface8
    {
        public new void Method2()
        {
            Debug.WriteLine("DerivedClass.Method2()");
        }

        public override void Method3()
        {
            Debug.WriteLine("DerivedClass.Method3()");
        }
    }
}
