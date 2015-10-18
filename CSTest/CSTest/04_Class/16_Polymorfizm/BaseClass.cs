using System.Diagnostics;

namespace CSTest._04_Class._16_Polymorfizm
{
    class BaseClass
    {
        public virtual void DoWork()
        {
            Debug.WriteLine("Base.DoWork()");
        }

        public void NonVirtualInterface()
        {
            PreDoWork();
            CoreDoWork();
        }

        private void PreDoWork()
        {
            Debug.WriteLine("Base.PreDoWork();");
        }

        protected virtual void CoreDoWork()
        {
            Debug.WriteLine("Base.CoreDoWork()");
        }

        public void SomeMetod1()
        {
            Debug.WriteLine("Base.1");
        }

        public virtual void SomeMetod3()
        {
            Debug.WriteLine("Base.3");
        }

        public virtual void SomeMetod4()
        {
            Debug.WriteLine("Base.4");
        }

        public void BaseClassMethod()
        {
            SomeMetod3();
            SomeMetod4();
        }

        public virtual void SomeVirtualMethods()
        {
            Debug.WriteLine("Base.SomeVirtualMethods");
            SomeMetod3();
            SomeMetod4();
        }
    }

    class DerivedFromDerivedClass : DerivedClass
    {
    }
}
