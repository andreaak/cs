﻿using System.Diagnostics;
using NUnit.Framework;

namespace Patterns._01_Creational.Singleton._002_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Singleton instance1 = Singleton.Instance();
            Singleton instance2 = Singleton.Instance();
            Debug.WriteLine(ReferenceEquals(instance1, instance2));

            instance1.SingletonOperation();
            string singletonData = instance1.GetSingletonData();
            Debug.WriteLine(singletonData);
        }
    }
}
