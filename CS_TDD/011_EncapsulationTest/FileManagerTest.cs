using CS_TDD._003_DI._000_Base;
using CS_TDD._003_DI._011_Encapsulation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _011_EncapsulationTest
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void FindLogFileTest2()
        {
            FileManager mgr = new FileManager(new TestDataObject());
            Assert.IsTrue(mgr.FindLogFile("file2.log"));
        }
    }
}
