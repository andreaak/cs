using System.Collections.Generic;
using CS_TDD._003_DI._000_Base;
using CS_TDD._003_DI._002_ConstructorInjection;
using NUnit.Framework;
using Rhino.Mocks;

namespace CS_TDD._004_StubsAndMocks._010_RhynoMocksStubs.Test
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public static void RhinoStubTest()
        {
            MockRepository rhinoEngine = new MockRepository();
            var stub = rhinoEngine.Stub<IDataAccessObject>();

            using (rhinoEngine.Record())
            {
                stub.GetFiles();
                LastCall.Return<List<string>>(new List<string> {"file1.txt", "file2.log", "file3.exe" });
            }

            FileManager mgr = new FileManager(stub);
            
            //Assert.IsTrue(mgr.FindLogFile("file2.log"));

            rhinoEngine.VerifyAll();
        }
    }
}
