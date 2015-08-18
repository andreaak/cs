namespace CS_TDD._004_StubsAndMocks._013_RhynoMocksPropertyConstraints
{
    public class MailMessage
    {
        string destination;
        string theme;
        string messageText;

        public string Destination { get { return destination; } }
        public string Theme { get { return theme; } }
        public string MessageText { get { return messageText; } }

        public MailMessage(string destination, string theme, string messageText)
        {
            this.destination = destination;
            this.theme = theme;
            this.messageText = messageText;
        }

        public MailMessage()
        {

        }
    }
}
