using System.Diagnostics;

namespace CSTest._06_Interface._0_Setup
{
    class _02_DerivedClass : _02_BaseClass, _02_IVirtualNonVirtual
    {
        public new void Method()
        {
            Debug.WriteLine("_02_DerivedClass.Method");
        }

        // Ключевое слово 'new' указывает на то, что этот метод
        // повторно реализует метод MethodNew интерфейса _02_IVirtualNonVirtual
        public new void MethodNew()
        {
            Debug.WriteLine("_02_DerivedClass.MethodNew");
        }

        public override void MethodVirtual()
        {
            Debug.WriteLine("_02_DerivedClass.MethodVirtual");
        }
    }
}
