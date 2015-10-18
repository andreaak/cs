namespace CS_TDD._004_StubsAndMocks._005_MockAndStub.Test
{
    class MockMailService : IMailService
    {
        public string message;
        public string destination;

        public void SendMail(string destination, string message)
        {
            this.message = message;
            this.destination = destination;
        }
    }
}
