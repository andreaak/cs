using System;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._002_MocksAndStubs.Test
{
    [TestFixture]
    class _LogAnalyzerTest
    {
        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            StubLogService stubService = new StubLogService();
            stubService.ToThrow = new Exception("fake exception");

            MockEmailService mockEmail = new MockEmailService();
            LogAnalyzer log = new LogAnalyzer();

            //we use setters instead of
            //constructor parameters for easier coding
            log.Service = stubService;
            log.Email = mockEmail;
            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);

            Assert.AreEqual("a", mockEmail.To);
            Assert.AreEqual("fake exception", mockEmail.Body);
            Assert.AreEqual("subject", mockEmail.Subject);
        }
    }
}
