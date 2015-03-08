using CS_TDD._003_DI._000_Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._003_PropertyInjection
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void FindLogFileTest3()
        {
            FileManager mgr = new FileManager();
            mgr.DataAccessObject = new TestDataObject();
            Assert.IsTrue(mgr.FindLogFile("file2.log"));
        }
    }
}
