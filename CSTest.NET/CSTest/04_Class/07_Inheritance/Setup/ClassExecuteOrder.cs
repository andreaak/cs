using System.Collections.Generic;
using System.Diagnostics;

namespace CSTest._04_Class._07_Inheritance.Setup
{
    class BaseClassExecuteOrder
    {
        static int c = InitStaticVariable();
        int b = InitInstanceVariable();

        static BaseClassExecuteOrder()
        {
            Debug.WriteLine("Base.StaticCtor");
        }

        public BaseClassExecuteOrder()
        {
            Debug.WriteLine("Base.Ctor");
            VirtualMethod();
        }

        private static int InitStaticVariable()
        {
            Debug.WriteLine("Base.InitStaticVariable");
            return 0;
        }

        private static int InitInstanceVariable()
        {
            Debug.WriteLine("Base.InitInstanceVariable");
            return 0;
        }

        public virtual void VirtualMethod()
        {
            Debug.WriteLine("Base.VirtualMethod");
        }
    }

    class DerivedClassExecuteOrder : BaseClassExecuteOrder
    {
        static int c = InitStaticVariable();
        int b = InitInstanceVariable();

        List<int> list = new List<int>();

        static DerivedClassExecuteOrder()
        {
            Debug.WriteLine("Derived.StaticCtor");
        }

        public DerivedClassExecuteOrder()
        {
            Debug.WriteLine("Derived.Ctor");
        }

        private static int InitStaticVariable()
        {
            Debug.WriteLine("Derived.InitStaticVariable");
            return 0;
        }

        private static int InitInstanceVariable()
        {
            Debug.WriteLine("Derived.InitInstanceVariable");
            return 0;
        }

        public override void VirtualMethod()
        {
            list.Add(1);
            Debug.WriteLine("Derived.VirtualMethod ");
        }
    }
}