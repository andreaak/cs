namespace CS_TDD._004_StubsAndMocks._002_MocksAndStubs.Test
{
    public class MockEmailService : IEmailService
    {
        public string To;
        public string Subject;
        public string Body;

        public void SendEmail(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
