using System;

namespace CSTest._05_Delegates_and_Events.Delegates
{
    class TestClassBase
    {
        public event EventHandler Test;

        public TestClassBase()
        {
            Test = TestClassBase_Test1;
            Test += TestClassBase_Test;
            TestEvent(ref Test);
        }

        private void TestClassBase_Test1(object sender, EventArgs e)
        {
            
        }

        void TestClassBase_Test(object sender, EventArgs e)
        {
        }

        private void TestEvent(ref EventHandler Test)
        {
            Test = null;
        }
    }

    class TestClass : TestClassBase
    {
    }
}
