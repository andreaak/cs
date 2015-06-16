using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest.Classes.ExtensionMethods
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            SealedClass obj = null;
            obj.Extension();
        }
    }
}
