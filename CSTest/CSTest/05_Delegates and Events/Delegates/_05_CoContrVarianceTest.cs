﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace CSTest._05_Delegates_and_Events.Delegates
{
    [TestClass]
    public class _05_CoContrVarianceTest
    {
        [TestMethod]
        public void TestDelegateCoContrVariance1()
        {
            TestDelegate first = FirstMethod;
            TestClassBase res = first(new TestClass());
            first = SecondMethod;
            res = first(new TestClass());
            first = ThirdMethod;
            res = first(new TestClass());
            first = FourthMethod;
            res = first(new TestClass());
        }
        //base
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