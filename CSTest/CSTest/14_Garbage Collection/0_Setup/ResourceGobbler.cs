using System;

namespace CSTest._14_Garbage_Collection._0_Setup
{
    class ResourceGobbler : IDisposable
    {
        public void Dispose()
        {
            
        }

        public void Test()
        { 
        
        }
    }

    struct ResourceGobblerStruct : IDisposable
    {
        public void Dispose()
        {

        }

        public void Test()
        {

        }
    }
}
