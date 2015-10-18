using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._001_Mocks.Test
{
    [TestFixture]
    class LogAnalyzerTest
    {
        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            MockLogService mockService = new MockLogService();

            LogAnalyzer log = new LogAnalyzer(mockService);
            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);

            Assert.AreEqual("Filename too short:abc.ext", mockService.LastError);
        }
    }
}
