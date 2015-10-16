using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest._05_Delegates_and_Events.Delegates
{
    delegate TestClassBase TestDelegate(TestClass inParam);

    class TestClassBase
    {
    }

    class TestClass : TestClassBase
    {
    }
}
