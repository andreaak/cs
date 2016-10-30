using System.Collections.Generic;
using System.Diagnostics;

namespace CSTest._04_Class._07_Inheritance
{
    class _01_ConstructorsBase
    {
        static int c = InitStaticVariable();
        int b = InitInstanceVariable();

        static _01_ConstructorsBase()
        {
            Debug.WriteLine("Base.StaticCtor");
        }

        public _01_ConstructorsBase()
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

    class _01_ConstructorsDerived : _01_ConstructorsBase
    {
        static int c = InitStaticVariable();
        int b = InitInstanceVariable();

        List<int> list = new List<int>();

        static _01_ConstructorsDerived()
        {
            Debug.WriteLine("Derived.StaticCtor");
        }

        public _01_ConstructorsDerived()
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
            Debug.WriteLine("Derived.VirtualMethod");
        }
    }
}