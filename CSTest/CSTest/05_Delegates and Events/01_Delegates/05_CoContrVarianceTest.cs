using System;
using System.Diagnostics;
using CSTest._05_Delegates_and_Events._01_Delegates._0_Setup;
using NUnit.Framework;

namespace CSTest._05_Delegates_and_Events._01_Delegates
{
    [TestFixture]
    public class _05_CoContrVarianceTest
    {
        delegate BaseClass TestDelegate(DerivedClass inParam);

        [Test]
        public void TestDelegateCoContrVariance1()
        {
            TestDelegate first = FirstMethod;
            Debug.WriteLine("Out param: " + first(new DerivedClass()));
            Debug.WriteLine("");

            first = SecondMethod;
            Debug.WriteLine("Out param: " + first(new DerivedClass()));
            Debug.WriteLine("");

            first = ThirdMethod;
            Debug.WriteLine("Out param: " + first(new DerivedClass()));
            Debug.WriteLine("");

            first = FourthMethod;
            Debug.WriteLine("Out param: " + first(new DerivedClass()));

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
        private BaseClass FirstMethod(DerivedClass arg)
        {
            Debug.WriteLine("FirstMethod - base");
            Debug.WriteLine("In param: " + typeof(DerivedClass));
            return new BaseClass();
        }

        //covariance
        private DerivedClass SecondMethod(DerivedClass arg)
        {
            Debug.WriteLine("SecondMethod - covariance");
            Debug.WriteLine("In param: " + typeof(DerivedClass));
            return new DerivedClass();
        }

        //contrvariance
        private BaseClass ThirdMethod(BaseClass arg)
        {
            Debug.WriteLine("ThirdMethod - contrvariance");
            Debug.WriteLine("In param: " + typeof(BaseClass));
            return new BaseClass();
        }

        //covariance and contrvariance
        private DerivedClass FourthMethod(BaseClass arg)
        {
            Debug.WriteLine("FourthMethod - covariance and contrvariance");
            Debug.WriteLine("In param: " + typeof(BaseClass));
            return new DerivedClass();
        }

        [Test]
        public void TestDelegateCoContrVariance2ReferenceTypes()
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

        [Test]
        public void TestDelegateCoContrVariance3ValueTypes()
        {
            Func<int, object> del = TestMethodBase2;
            Debug.WriteLine(del(1));
            //int _05_CoContrVarianceTest.TestMethodDerived2(int)' has 
            //the wrong return type 
            //del = TestMethodDerived2;
            //del = TestMethodDerived3;
            //Debug.WriteLine(del(2));

            /*
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

        private object TestMethodBase2(int p)
        {
            Debug.WriteLine("Parameter: " + p.GetType());
            return new object();
        }

        private int TestMethodDerived2(int p)
        {
            Debug.WriteLine("Parameter: " + p.GetType());
            return 22;
        }

        private object TestMethodDerived3(object p)
        {
            Debug.WriteLine("Parameter: " + p.GetType());
            return 22;
        }
    }
}
