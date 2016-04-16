using CSTest._14_Garbage_Collection._0_Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace CSTest._14_Garbage_Collection
{
    [TestClass]
    public class _01_Dispose
    {
        /*
        */

        [TestMethod]
        public void TestDispose1()
        {
            using (ResourceGobbler theInstance = new ResourceGobbler())//type used in a using statement must be implicitly convertible to 'System.IDisposable'
            {
                // выполнить работу 
                theInstance.Test();
                //theInstance = null;//Cannot assign to 'theInstance' because it is a 'using variable
            }
            //theInstance.Test(); //The name 'theInstance' does not exist in the current context
        }

        [TestMethod]
        public void TestDispose2Compiled()
        {
            ResourceGobbler theInstance = new ResourceGobbler();
            try
            {
                // выполнить работу 
                theInstance.Test();
            }
            finally
            {
                // Явное закрытие файла после записи
                if (theInstance != null)
                {
                    ((IDisposable)theInstance).Dispose();
                }
            }
        }
        
        [TestMethod]
        public void TestDispose4NotRecomended()
        {
            ResourceGobbler theInstance = new ResourceGobbler();
            using (theInstance)
            {
                // выполнить работу 
                theInstance.Test();
                theInstance = null;//Possibly incorrect assignment to local 'theInstance' which is the argument to a using or lock statement. 
                //The Dispose call or unlocking will happen on the original value of the local.
            }
            theInstance.Test(); // theInstance is still in scope but the method call throws an exception
        }
    }
}
