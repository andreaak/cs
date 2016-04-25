using System;
using CS_TDD._004_StubsAndMocks._004_Mocks;
using CS_TDD._004_StubsAndMocks._013_RhynoMocksPropertyConstraints;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using FileManager = CS_TDD._004_StubsAndMocks._013_RhynoMocksPropertyConstraints.FileManager;

namespace CS_TDD._004_StubsAndMocks._014_RhynoMocksIsMatchingConstraint.Test
{
    [TestFixture]
    class FileManagerTest
    {

        [Test]
        public static void RhynoMocksIsMatchingConstraintTest()
        {
            var excepetionToThrow = new Exception("TestMessage");

            MockRepository rhinoEngine = new MockRepository();

            var mailService = rhinoEngine.DynamicMock<IMailService>();
            var logService = rhinoEngine.Stub<ILogService>();
            FileManager mgr = new FileManager(logService, mailService);

            using (rhinoEngine.Record())
            {
                logService.LogError("Anything");
                LastCall.Constraints(new Anything());
                LastCall.Throw(excepetionToThrow);

                mailService.SendMail(null);
                LastCall.Constraints(Rhino.Mocks.Constraints.Is.Matching<MailMessage>(
                    message =>
                    {
                        if (message.Destination == "TechSupport@mail.com" && message.Theme == excepetionToThrow.GetType().Name && message.MessageText == excepetionToThrow.Message)
                            return true;

                        return false;
                    }));
            }

            mgr.Analize("file.exe");

            rhinoEngine.VerifyAll();
        }
    }
}
