using System;

namespace CSTest._05_Delegates_and_Events._02_Events
{
    class TestClass
    {
        public event EventHandler Test;

        public TestClass()
        {
            Test = TestClass_Test1;
            Test += TestClass_Test;
            TestEvent(ref Test);
        }

        private void TestClass_Test1(object sender, EventArgs e)
        {

        }

        private void TestClass_Test(object sender, EventArgs e)
        {
        }

        private void TestEvent(ref EventHandler Test)
        {
            Test = null;
        }
    }
}
