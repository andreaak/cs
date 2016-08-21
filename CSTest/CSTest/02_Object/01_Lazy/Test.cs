using System;
using System.Diagnostics;
using NUnit.Framework;

namespace CSTest._02_Object._01_Lazy
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestLazy1()
        {
            Debug.WriteLine("Before init");
            LazyNET<TestForLazy> lazy = new LazyNET<TestForLazy>(() =>
            {
                Debug.WriteLine("Factory Method");

                TestForLazy res = new TestForLazy();
                res.Name = "Test";
                return res;
            }, false);
            Debug.WriteLine("Before using");
            TestForLazy test = lazy.Value;
            Debug.WriteLine(test.Name);
            /*
            Before init
            Before using
            Create value
            Factory Method
            Test
            */
        }
    }

    class TestForLazy
    {
        public string Name { get; set; }
    }
}
