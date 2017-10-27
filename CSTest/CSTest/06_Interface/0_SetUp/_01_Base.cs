using System.Diagnostics;

namespace CSTest._06_Interface._0_Setup
{
    interface _01_IInterface
    {
        void Method();
    }

    class _01_Base : _01_IInterface
    {
        public void Method()
        {
            Debug.WriteLine("_01_Base.Method");
        }
    }
}
