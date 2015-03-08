using CS_TDD._003_DI._000_Base;
using CS_TDD._003_DI._008_AbsFactoryInjection.AbstractFactory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._008_AbsFactoryInjection
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void FindLogFileTest8()
        {
            var client = new Client(new StubConcreteFactory());

            IFileManager mgr = client.Run();

            string fileName = "file1.txt";

            Assert.IsTrue(mgr.FindLogFile(fileName));
        }
    }
}
