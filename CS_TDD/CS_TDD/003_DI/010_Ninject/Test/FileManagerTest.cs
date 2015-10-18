using Ninject;
using NUnit.Framework;

namespace CS_TDD._003_DI._010_Ninject.Test
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void NinjectTest()
        {
            // Ninject Initialization
            IKernel ninjectKernel = new StandardKernel(new ConfigFileObjectData());

            FileManager manager = ninjectKernel.Get<FileManager>();

            Assert.IsTrue(manager.FindLogFile("file2.log"));
        }
    }
}
