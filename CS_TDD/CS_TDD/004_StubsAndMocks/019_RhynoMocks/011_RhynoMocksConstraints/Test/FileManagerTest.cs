using System;
using CS_TDD._004_StubsAndMocks._004_Mocks;
using CS_TDD._004_StubsAndMocks._005_MockAndStub;
using NUnit.Framework;
using Rhino.Mocks;

namespace CS_TDD._004_StubsAndMocks._011_RhynoMocksConstraints.Test
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public static void RhinoMocksStubTest_2()
        {
            string fileName = "SomeFile.log";

            MockRepository rhinoEngine = new MockRepository();
            var logService = rhinoEngine.Stub<ILogService>();
            var mailService = rhinoEngine.DynamicMock<IMailService>();

            using (rhinoEngine.Record())
            {
                logService.LogError("Whatever");
                LastCall.Constraints(new Rhino.Mocks.Constraints.Anything());
                LastCall.Throw(new Exception("TestMessage"));

                mailService.SendMail("some@mail.mail", "TestMessage");
            }

            _005_MockAndStub.FileManager mgr = new _005_MockAndStub.FileManager(logService, mailService);

            mgr.Analize(fileName);

            rhinoEngine.VerifyAll();
        }
    }
}
