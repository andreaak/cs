using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest._05_Delegates_and_Events.Delegates
{
    delegate TestClassBase TestDelegate(TestClass inParam);

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
            throw new NotImplementedException();
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
