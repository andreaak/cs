using System;
using System.Diagnostics;
using NUnit.Framework;

namespace CSTest._25_CS6
{
    [TestFixture]
    public class _07_ExceptionFiltersTests
    {
#if CS6
        [Test]
        public void Test1()
        {
            TestMethod(true);
            Assert.Catch<Exception>(() => TestMethod(false));
            /*
            Exception thrown: 'System.Exception' in CSTest.dll
            System.ArgumentNullException: Value cannot be null.
            Parameter name: Inner
            Exception thrown: 'System.Exception' in CSTest.dll
            */
        }

        private static void TestMethod(bool isHandlingException)
        {
            try
            {
                if(isHandlingException)
                    throw new Exception("Test Handling", new ArgumentNullException("Inner"));
                throw new Exception("Test Not Handling");
            }
            catch (Exception ex) when (ex.InnerException != null)
            {
                Debug.WriteLine(ex.InnerException);
            }
        }

        private static void TestMethod2(bool isHandlingException)
        {
            try
            {
                if (isHandlingException)
                    throw new Exception("Test Handling", new ArgumentNullException("Inner"));
                throw new Exception("Test Not Handling");
            }
            catch (Exception ex) when (ex.InnerException != null)
            {
                Debug.WriteLine(ex.InnerException);
            }
        }
#endif
    }
}
