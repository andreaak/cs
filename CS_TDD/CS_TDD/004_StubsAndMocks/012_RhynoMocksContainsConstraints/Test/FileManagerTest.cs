using CS_TDD._004_StubsAndMocks._004_Mocks;
using NUnit.Framework;
using Rhino.Mocks;

namespace CS_TDD._004_StubsAndMocks._012_RhynoMocksConstraintsContains.Test
{
    [TestFixture]
    class FileManagerTest
    {

        static MockRepository rhinoEngine;
        static ILogService logService;

        [SetUp]
        public static void SetUpAttribute()
        {
            rhinoEngine = new MockRepository();
            logService = rhinoEngine.DynamicMock<ILogService>();
        }

        [Test]
        public static void RhinoMocksContainsTest_1()
        {
            string fileName = "So.txt";

            using (rhinoEngine.Record())
            {
                logService.LogError("WhatEver");
                LastCall.Constraints(new Rhino.Mocks.Constraints.Contains("Filename"));
            }

            FileManager mgr = new FileManager(logService);

            mgr.Analize(fileName);

            rhinoEngine.Verify(logService);
        }

        [Test]
        public static void RhinoMocksContainsTest_2()
        {
            string fileName = "SomeFileName.exe";

            using (rhinoEngine.Record())
            {
                logService.LogError("WhatEver");
                LastCall.Constraints(new Rhino.Mocks.Constraints.Contains("FileExtension"));
            }

            FileManager mgr = new FileManager(logService);

            mgr.Analize(fileName);

            rhinoEngine.Verify(logService);
        }
    }
}
