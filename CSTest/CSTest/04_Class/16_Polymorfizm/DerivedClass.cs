using System.Diagnostics;

namespace CSTest._04_Class._16_Polymorfizm
{
    class DerivedClass : BaseClass
    {
        // Переопределение виртуального метода.
        public override void DoWork()
        {
            Debug.WriteLine("Derived.DoWork()");
        }

        //NVI
        protected override void CoreDoWork()
        {
            Debug.WriteLine("Derived.CoreDoWork()");
        }

        public new void SomeMetod1()
        {
            Debug.WriteLine("Derived.1");
        }

        // Не является переопределением.
        // NEW метод - (перекрытие)
        public new void SomeMetod3()
        {
            Debug.WriteLine("Derived.3");
        }

        public sealed override void SomeMetod4()
        {
            Debug.WriteLine("Derived.4");
        }

        public override void SomeVirtualMethods()
        {
            Debug.WriteLine("Derived.SomeVirtualMethods");
            base.SomeMetod3();
            base.SomeMetod4();
            SomeMetod4();
            this.SomeMetod3();
            this.SomeMetod4();
        }
    }
}
