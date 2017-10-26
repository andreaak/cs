using System.Diagnostics;

namespace CSTest._06_Interface
{
    interface _071_IInterface
    {
        void Method();
    }

    interface _072_IInterface
    {
        void MethodNew();

        void MethodVirtual();
    }

    class _07_BaseClass : _072_IInterface
    {
        public void Method()
        {
            Debug.WriteLine("_07_BaseClass.Method()");
        }

        public void MethodNew()
        {
            Debug.WriteLine("_07_BaseClass.MethodNew()");
        }

        public virtual void MethodVirtual()
        {
            Debug.WriteLine("_07_BaseClass.MethodVirtual()");
        }
    }

    class _071_DerivedClass : _07_BaseClass, _071_IInterface
    {
        // Реализация интерфейса не обязательна, т.к., 
        // сигнатуры методов в классе и интерфейсе совпадают.

        public new void MethodNew()
        {
            Debug.WriteLine("_071_DerivedClass.MethodNew()");
        }

        public override void MethodVirtual()
        {
            Debug.WriteLine("_071_DerivedClass.MethodVirtual()");
        }
    }

    class _072_DerivedClass : _07_BaseClass, _072_IInterface
    {
        public new void MethodNew()
        {
            Debug.WriteLine("_072_DerivedClass.MethodNew()");
        }

        public override void MethodVirtual()
        {
            Debug.WriteLine("_072_DerivedClass.MethodVirtual()");
        }
    }
}
