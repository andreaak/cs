using System;
using System.Diagnostics;
using System.Linq;
using CSTest._05_Delegates_and_Events._01_Delegates._0_Setup;
using NUnit.Framework;

namespace CSTest._05_Delegates_and_Events._01_Delegates
{
    [TestFixture]
    public class _04_ChainingTest
    {
        delegate BaseClass TestDelegate(DerivedClass inParam);

        [Test]
        public void TestDelegateChaining()
        {
            var testClass = new TestCoContrVarianceClass();

            TestDelegate del = testClass.FirstMethod;
            del += testClass.Covariant;
            del += testClass.Contrvariant;
            del += testClass.Contrvariant;
            del += testClass.Contrvariant;
            Debug.WriteLine(string.Join(",", del.GetInvocationList().Select(item => item.Method.Name)));
            del -= testClass.Contrvariant;
            Debug.WriteLine(string.Join(",", del.GetInvocationList().Select(item => item.Method.Name)));
            del -= testClass.Contrvariant;
            Debug.WriteLine(string.Join(",", del.GetInvocationList().Select(item => item.Method.Name)));
            del -= testClass.Contrvariant;
            Debug.WriteLine(string.Join(",", del.GetInvocationList().Select(item => item.Method.Name)));
            del -= testClass.FirstMethod;
            Debug.WriteLine(string.Join(",", del.GetInvocationList().Select(item => item.Method.Name)));
            del -= testClass.Covariant;
            Debug.WriteLine(string.Join(",", del.GetInvocationList().Select(item => item.Method.Name)));


            /*
            FirstMethod,SecondMethod,ThirdMethod,ThirdMethod,ThirdMethod
            FirstMethod,SecondMethod,ThirdMethod,ThirdMethod
            FirstMethod,SecondMethod,ThirdMethod
            FirstMethod,SecondMethod
            SecondMethod
            Exception thrown: 'System.NullReferenceException' in CSTest.dll
            */
        }


    }
}
