using System;

namespace CSTest._05_Delegates_and_Events.Events._0_Setup
{
    class TestClass
    {
        public event EventHandler Test;

        public TestClass()
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
}
