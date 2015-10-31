using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace CSTest._03_Structure._02_Enums
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestFlagEnum1()
        {
            TestFlagsEnum en = TestFlagsEnum.Pending;
            Debug.WriteLine(en);
            en |= TestFlagsEnum.View;
            Debug.WriteLine(en);
            en = en.Set(TestFlagsEnum.MarkedTestAsResult);
            Debug.WriteLine(en);

            Debug.WriteLine(en.IsSet(TestFlagsEnum.View));
            en = en.Clear(TestFlagsEnum.MarkedTestAsResult);
            Debug.WriteLine(en);

            /*
            Pending
            View, Pending
            View, Pending, MarkedTestAsResult
            True
            View, Pending
            */
        }

        [TestMethod]
        public void TestFlagEnum()
        {
            TestFlagsEnum en = TestFlagsEnum.None;
            Debug.WriteLine(en);
            en |= TestFlagsEnum.Pending;
            Debug.WriteLine(en);
            en |= TestFlagsEnum.View;
            Debug.WriteLine(en);
            en = en.Set(TestFlagsEnum.MarkedTestAsResult);
            Debug.WriteLine(en);

            Debug.WriteLine(en.IsSet(TestFlagsEnum.View));
            en = en.Clear(TestFlagsEnum.MarkedTestAsResult);
            Debug.WriteLine(en);


            /*
            None
            Pending
            View, Pending
            View, Pending, MarkedTestAsResult
            True
            View, Pending
            */
        }

    }
}
