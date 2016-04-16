
using System.Diagnostics;
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

    public abstract class Logger2
    {
        public virtual void WriteLineVirtPublic(string message)
        {
            Debug.WriteLine(message);
        }

        protected virtual void WriteLineVirtProtected(string message)
        {
            Debug.WriteLine(message);
        }

        private void WriteLinePrivate(string message)
        {
            Debug.WriteLine(message);
        }

        public abstract void WriteLinePublic(string message);

        protected void WriteLineProtected(string message)
        {
            Debug.WriteLine(message);
        }

        public static void WriteLinePublicStatic(string message)
        {
            Debug.WriteLine(message);
        }

        protected static void WriteLineProtectedStatic(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
