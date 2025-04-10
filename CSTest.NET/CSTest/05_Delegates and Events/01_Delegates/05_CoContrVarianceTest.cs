using System;
using System.Diagnostics;
using System.Linq;
using CSTest._05_Delegates_and_Events._01_Delegates._0_Setup;
using CSTest._05_Delegates_and_Events._02_Events._01_Theory;
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
            var testClass = new TestCoContrVarianceClass();

            TestDelegate first = testClass.FirstMethod;
            Debug.WriteLine("Out param: " + first(new DerivedClass()));
            Debug.WriteLine("");

            first = testClass.Covariant;
            Debug.WriteLine("Out param: " + first(new DerivedClass()));
            Debug.WriteLine("");

            first = testClass.Contrvariant;
            Debug.WriteLine("Out param: " + first(new DerivedClass()));
            Debug.WriteLine("");

            first = testClass.CoContrvariant;
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



        [Test]
        public void TestDelegateCoContrVariance2ReferenceTypes()
        {
            var testClass = new TestCoContrVarianceClass();

            Func<string, object> del = testClass.TestMethodRefBase;
            Debug.WriteLine(del("___"));
            del = testClass.TestMethodRefCoContrvariant;
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
            var testClass = new TestCoContrVarianceClass();

            Func<int, object> del = testClass.TestMethodValBase;
            Debug.WriteLine(del(1));

            //del = TestMethodValCovariant;//'int _05_CoContrVarianceTest.TestMethodValCovariant(int)' has the wrong return type
            //del = TestMethodValContrvariant;//No overload for '_05_CoContrVarianceTest.TestMethodValContrvariant(object)' matches delegate 'Func<int, object>'
            //Debug.WriteLine(del(2));

            /*
            */



        }
    }
}
