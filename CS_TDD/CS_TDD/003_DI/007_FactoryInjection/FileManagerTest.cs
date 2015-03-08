using CS_TDD._003_DI._000_Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._007_FactoryInjection
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void FindLogFileTest7()
        {
            FactoryClass.SetDataAccessObject(new TestDataObject());

            FileManager mgr = new FileManager();

            Assert.IsTrue(mgr.FindLogFile("file1.txt"));
        }
    }
}
