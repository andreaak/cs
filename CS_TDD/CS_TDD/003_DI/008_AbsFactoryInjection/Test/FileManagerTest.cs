using CS_TDD._003_DI._008_AbsFactoryInjection.AbstractFactory;
using CS_TDD._003_DI._008_AbsFactoryInjection.Test.AbstractFactory;
using NUnit.Framework;

namespace CS_TDD._003_DI._008_AbsFactoryInjection.Test
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void AbstractFactoryInjectionTest()
        {
            var client = new Client(new StubConcreteFactory());

            IFileManager mgr = client.Run();

            string fileName = "file1.txt";

            Assert.IsTrue(mgr.FindLogFile(fileName));
        }
    }
}
