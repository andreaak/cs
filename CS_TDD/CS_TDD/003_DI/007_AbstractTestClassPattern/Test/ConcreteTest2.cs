using CS_TDD._003_DI._000_Base.Test;
using CS_TDD._003_DI._006_FactoryInjection;
using NUnit.Framework;

namespace CS_TDD._003_DI._007_AbstractTestClassPattern.Test
{

    [TestFixture]
    class FileManagerTest2 : AbstractTestClass
    {
        public override void SetUp()
        {
            FactoryClass.SetDataAccessObject(new StubFileDataObject());
        }

        //We can add more test methods here
        //. . .

        // Inherited member:

        //[Test]
        //public void FindLogFileTest1()
        //{
        //    FileManager mgr = new FileManager();

        //    Assert.IsTrue(mgr.FindLogFile("file1.txt"));
        //}
    }
}
