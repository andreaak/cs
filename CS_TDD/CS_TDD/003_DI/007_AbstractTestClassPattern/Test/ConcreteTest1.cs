using NUnit.Framework;

namespace CS_TDD._003_DI._007_AbstractTestClassPattern.Test
{
    [TestFixture]
    class FileManagerTest1 : AbstractTestClass
    {
        public override void SetUp()
        {
            base.SetUp();
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
