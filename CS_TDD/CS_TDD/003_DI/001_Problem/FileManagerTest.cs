using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._001_Problem
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void ProblemTest()
        {
            FileManager mgr = new FileManager();
            Assert.IsTrue(mgr.FindLogFile("file2.log"));
        }
    }
}
