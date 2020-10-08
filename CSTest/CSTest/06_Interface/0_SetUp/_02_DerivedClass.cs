using System.Diagnostics;

namespace CSTest._06_Interface._0_Setup
{
    class _02_DerivedClass : _02_BaseClass, _02_IVirtualNonVirtual
    {
        public new int field1;

        public _02_DerivedClass()
        {
            base.field1 = 5;
            field1 = 7;
        }

        public new void Method()
        {
            Debug.WriteLine("_02_DerivedClass.Method");
        }

        // Ключевое слово 'new' указывает на то, что этот метод
        // повторно реализует метод MethodNew интерфейса _02_IVirtualNonVirtual
        public new void MethodNew()
        {
            Debug.WriteLine("_02_DerivedClass.MethodNew");
            Debug.WriteLine("Field1: " + field1);
        }

        public override void MethodVirtual()
        {
            Debug.WriteLine("_02_DerivedClass.MethodVirtual");
            Debug.WriteLine("Field1: " + field1);
        }
    }
}
