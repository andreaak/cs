using System;
using System.Diagnostics;
using CSTest._05_Delegates_and_Events._01_Delegates._0_Setup;
using NUnit.Framework;

namespace CSTest._05_Delegates_and_Events._01_Delegates
{
    [TestFixture]
    public class _05_CoContrVarianceTest
    {
        delegate TestClassBase TestDelegate(TestClass inParam);

        [Test]
        public void TestDelegateCoContrVariance1()
        {
            TestDelegate first = FirstMethod;
            Debug.WriteLine("Out param: " + first(new TestClass()));
            Debug.WriteLine("");

            first = SecondMethod;
            Debug.WriteLine("Out param: " + first(new TestClass()));
            Debug.WriteLine("");

            first = ThirdMethod;
            Debug.WriteLine("Out param: " + first(new TestClass()));
            Debug.WriteLine("");

            first = FourthMethod;
            Debug.WriteLine("Out param: " + first(new TestClass()));

            /*
            FirstMethod - base
            In param: CSTest._05_Delegates_and_Events.Delegates.TestClass
            Out param: CSTest._05_Delegates_and_Events.Delegates.TestClassBase

            SecondMethod - covariance
            In param: CSTest._05_Delegates_and_Events.Delegates.TestClass
            Out param: CSTest._05_Delegates_and_Events.Delegates.TestClass

            ThirdMethod - contrvariance
            In param: CSTest._05_Delegates_and_Events.Delegates.TestClassBase
            Out param: CSTest._05_Delegates_and_Events.Delegates.TestClassBase

            FourthMethod - covariance and contrvariance
            In param: CSTest._05_Delegates_and_Events.Delegates.TestClassBase
            Out param: CSTest._05_Delegates_and_Events.Delegates.TestClass
            */
        }

        //base
        private TestClassBase FirstMethod(TestClass arg)
        {
            Debug.WriteLine("FirstMethod - base");
            Debug.WriteLine("In param: " + typeof(TestClass));
            return new TestClassBase();
        }

        //covariance
        private TestClass SecondMethod(TestClass arg)
        {
            Debug.WriteLine("SecondMethod - covariance");
            Debug.WriteLine("In param: " + typeof(TestClass));
            return new TestClass();
        }

        //contrvariance
        private TestClassBase ThirdMethod(TestClassBase arg)
        {
            Debug.WriteLine("ThirdMethod - contrvariance");
            Debug.WriteLine("In param: " + typeof(TestClassBase));
            return new TestClassBase();
        }

        //covariance and contrvariance
        private TestClass FourthMethod(TestClassBase arg)
        {
            Debug.WriteLine("FourthMethod - covariance and contrvariance");
            Debug.WriteLine("In param: " + typeof(TestClassBase));
            return new TestClass();
        }

        [Test]
        public void TestDelegateCoContrVariance2FrameworkTypes()
        {
            Func<string, object> del = TestMethodBase;
            Debug.WriteLine(del("___"));
            del = TestMethodDerived;
            Debug.WriteLine(del("___"));
            /*
            Parameter: System.String
            System.Object
            Parameter: System.String
            TEST
            */
        }

        private object TestMethodBase(string p)
        {
            Debug.WriteLine("Parameter: " + p.GetType());
            return new object();
        }

        private string TestMethodDerived(object p)
        {
            Debug.WriteLine("Parameter: " + p.GetType());
            return "TEST";
        }
    }
}
