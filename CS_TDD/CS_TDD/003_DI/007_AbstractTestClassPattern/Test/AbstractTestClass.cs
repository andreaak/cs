using CS_TDD._003_DI._000_Base;
using CS_TDD._003_DI._006_FactoryInjection;
using NUnit.Framework;

namespace CS_TDD._003_DI._007_AbstractTestClassPattern.Test
{
    abstract class AbstractTestClass
    {
        [SetUp]
        public virtual void SetUp()
        {
            FactoryClass.SetDataAccessObject(new FileDataObject()); 
        }

        [Test]
        public void AbstractTestClassPatternTest()
        {
            FileManager mgr = new FileManager();

            Assert.IsTrue(mgr.FindLogFile("file1.txt"));
        }
    }
}
