using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace CSTest._05_Delegates_and_Events.Delegates
{
    [TestClass]
    public class _05_CoContrVarianceTest
    {
        [TestMethod]
        public void Test1()
        {
            Func<TestClass, TestClassBase> first = FirstMethod;
            first(new TestClass());
            first = SecondMethod;
            first(new TestClass());
            first = ThirdMethod;
            first(new TestClass());
            first = FourthMethod;
            first(new TestClass());
        }
        //invariance
        private TestClassBase FirstMethod(TestClass arg)
        {
            Debug.WriteLine("FirstMethod");
            return new TestClassBase();
        }
        //covariance
        private TestClass SecondMethod(TestClass arg)
        {
            Debug.WriteLine("SecondMethod");
            return new TestClass();
        }
        //contrvariance
        private TestClassBase ThirdMethod(TestClassBase arg)
        {
            Debug.WriteLine("ThirdMethod");
            return new TestClassBase();
        }
        //covariance and contrvariance
        private TestClass FourthMethod(TestClassBase arg)
        {
            Debug.WriteLine("FourthMethod");
            return new TestClass();
        }
    }
}
