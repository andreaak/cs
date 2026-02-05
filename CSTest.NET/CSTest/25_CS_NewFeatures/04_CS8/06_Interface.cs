using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._04_CS8
{
    [TestFixture]
    public class _06_Interface
    {
#if CS7
        [Test]
        public void TestInterface_1()
        {
            ((ILogger)new Logger()).Log("сообщение");

            ILogger.Prefix = "Журнальный файл";
        }



#endif
    }

    interface ILogger
    {
        void Log(string text) => Debug.WriteLine(text);

        static string Prefix = "TST";
    }

    class Logger : ILogger { }


}
