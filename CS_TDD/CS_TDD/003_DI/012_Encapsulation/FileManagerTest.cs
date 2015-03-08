using CS_TDD._003_DI._000_Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._012_Encapsulation
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void FindLogFileTest12()
        {
#if DEBUG
            FileManager mgr = new FileManager(new TestDataObject());
            Assert.IsTrue(mgr.FindLogFile("file2.log"));
#endif
        }
    }
}
