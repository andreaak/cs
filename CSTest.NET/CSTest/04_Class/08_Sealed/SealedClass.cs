using System.Diagnostics;

namespace CSTest._04_Class._08_Sealed
{
    sealed class SealedClass
    {
        public int x;
        public int y;
    }

    // Попытка наследования от SealedClass приводит к ошибке компилятора.
    //class DerivedClass : SealedClass   // Error
    //{
    //}

    class ClassA
    {
        public virtual void Method1() { Debug.WriteLine("ClassA.Method1"); }
        public virtual void Method2() { Debug.WriteLine("ClassA.Method2"); }
    }

    class ClassB : ClassA
    {
        public sealed override void Method1() { Debug.WriteLine("ClassB.Method1"); }
        public override void Method2() { Debug.WriteLine("ClassB.Method2"); }
    }

    class ClassC : ClassB
    {
        // Попытка переопределить Method1 приводит к ошибке компилятора: CS0239.
        // public override void Method1() { Debug.WriteLine("ClassC.Method1"); }

        // Переопределение Method2 позволено.
        public override void Method2() { Debug.WriteLine("ClassC.Method2"); }
    }
}
