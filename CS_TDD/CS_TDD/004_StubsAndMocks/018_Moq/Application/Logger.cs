
namespace CS_TDD._004_StubsAndMocks._018_Moq.Application
{
    public interface ILogWriter
    {
        string GetLogger();
        void SetLogger(string logger);
        void Write(string message);
    }

    public class Logger
    {
        private readonly ILogWriter _logWriter;

        public Logger(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public void WriteLine(string message)
        {
            _logWriter.Write(message);
        }
    }
}
