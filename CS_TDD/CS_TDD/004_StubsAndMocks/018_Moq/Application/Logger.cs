using System.Diagnostics;

namespace CS_TDD._004_StubsAndMocks._018_Moq.Application
{
    public interface ILogWriter
    {
        void SetLogger(string logger);
        void Write(string message);
    }

    public abstract class LoggerAbstract
    {
        public virtual bool WriteLinePublicVirt(string message)
        {
            Debug.WriteLine(message);
            return false;
        }

        public abstract void WriteLinePublicAbstract(string message);

        //Не мокаются
        public bool WriteLinePublic(string message)
        {
            Debug.WriteLine(message);
            return false;
        }

        //public static void WriteLinePublicStatic(string message)
        //{
        //    Debug.WriteLine(message);
        //}

        //protected virtual void WriteLineVirtProtected(string message)
        //{
        //    Debug.WriteLine(message);
        //}

        //protected static void WriteLineProtectedStatic(string message)
        //{
        //    Debug.WriteLine(message);
        //}

        //protected void WriteLineProtected(string message)
        //{
        //    Debug.WriteLine(message);
        //}

        //private void WriteLinePrivate(string message)
        //{
        //    Debug.WriteLine(message);
        //}
    }

    public class Logger : LoggerAbstract
    {
        private readonly ILogWriter _logWriter;

        public Logger()
        {
        }

        public Logger(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public void SetLogger(string logger)
        {
            _logWriter.SetLogger(logger);
        }

        public void WriteLine(string message)
        {
            _logWriter.Write(message);
        }

        public override bool WriteLinePublicVirt(string message)
        {
            Debug.WriteLine("WriteLinePublicVirt - " + message);
            return false;
        }

        public override void WriteLinePublicAbstract(string message)
        {
            Debug.WriteLine("WriteLinePublicAbstract - " + message);
        }
    }
}
