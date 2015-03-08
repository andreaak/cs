using CS_TDD._003_DI._000_Base;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._010_Ninject
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void FindLogFileTest6()
        {
            // Ninject Initialization
            IKernel ninjectKernel = new StandardKernel(new ConfigFileObjectData());

            FileManager manager = ninjectKernel.Get<FileManager>();

            Assert.IsTrue(manager.FindLogFile("file2.log"));
        }
    }
}
