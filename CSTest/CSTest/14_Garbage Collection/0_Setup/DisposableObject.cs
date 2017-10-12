using System;
using System.Diagnostics;

namespace CSTest._14_Garbage_Collection._0_Setup
{
    class DisposableObject : IDisposable
    {
        public void Dispose()
        {
            Debug.WriteLine("Dispose in DisposableObject");

        }

        public void Test()
        {
            Debug.WriteLine("Test in DisposableObject");
        }
    }

    class NotIDisposableObject
    {
        public void Dispose()
        {

        }

        public void Test()
        {

        }
    }

    struct DisposableStruct : IDisposable
    {
        public void Dispose()
        {
            Debug.WriteLine("Dispose in DisposableStruct");
        }

        public void Test()
        {
            Debug.WriteLine("Test in DisposableStruct");
        }
    }
}
