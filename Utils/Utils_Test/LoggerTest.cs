using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils.WorkWithFiles;

namespace Utils_Test
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void Open()
        {
            using (Logger logger = Logger.GetInstance())
            {
                Assert.IsNotNull(logger);
            }
        }

        [TestMethod]
        public void WriteDebug()
        {
            using (Logger logger = Logger.GetInstance())
            {
                Assert.IsNotNull(logger);
                logger.WriteLine("Test");
            }
        }

        [TestMethod]
        public void WriteConsole()
        {
            using (Logger logger = Logger.GetInstance())
            {
                Assert.IsNotNull(logger);
                logger.Init(null);
                logger.WriteLine("Test");
            }
        }

        [TestMethod]
        public void WriteFile()
        {
            using (Logger logger = Logger.GetInstance())
            {
                Assert.IsNotNull(logger);
                logger.Init(@"D:\Test.txt");
                logger.WriteLine("Test in file");
            }
        }
    }
}
