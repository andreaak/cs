using CS_TDD._003_DI._000_Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._005_LocalFactoryMethod
{
    class FileManagerUnderTest : FileManager
    {
        protected override IDataAccessObject LocalFactoryMethod()
        {
            return new TestDataObject();
        }
    }


    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void FindLogFileTest9()
        {
            var mgr = new FileManagerUnderTest();

            var result = mgr.FindLogFile("file1.txt");

            Assert.IsTrue(result);
        }
    }
}
