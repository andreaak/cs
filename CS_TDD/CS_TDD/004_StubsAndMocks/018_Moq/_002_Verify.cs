using CS_TDD._004_StubsAndMocks._018_Moq.Application;
using Moq;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._018_Moq
{
    [TestFixture]
    public class _002_Verify
    {
        /*
        mock.Verify(foo => foo.Execute("ping"));

        // Verify with custom error message for failure
        mock.Verify(foo => foo.Execute("ping"), "When doing operation X, the service should be pinged always");

        // Method should never be called
        mock.Verify(foo => foo.Execute("ping"), Times.Never());

        // Called at least once
        mock.Verify(foo => foo.Execute("ping"), Times.AtLeastOnce());

        // Verify getter invocation, regardless of value.
        mock.VerifyGet(foo => foo.Name);

        // Verify setter invocation, regardless of value.
        mock.VerifySet(foo => foo.Name);

        // Verify setter called with specific value
        mock.VerifySet(foo => foo.Name ="foo");

        // Verify setter with an argument matcher
        mock.VerifySet(foo => foo.Value = It.IsInRange(1, 5, Range.Inclusive));
        */
        [Test]
        public void MoqTestVerify01VerifyCallWithAnyArgument()
        {
            Mock<ILogWriter> mock = new Mock<ILogWriter>();
            Logger logger = new Logger(mock.Object);

            logger.WriteLine("Hello, logger!");

            // Проверяем, что вызвался метод Write нашего мока с любым аргументом
            mock.Verify(lw => lw.Write(It.IsAny<string>()));
        }

        [Test]
        public void MoqTestVerify02VerifyCallWithArgument()
        {
            Mock<IFoo> mock = new Mock<IFoo>();
            mock.Object.DoSomething("ping");
            mock.Verify(foo => foo.DoSomething("ping"));

            //--------------

            Mock<ILogWriter> mock2 = new Mock<ILogWriter>();
            Logger logger = new Logger(mock2.Object);
            logger.WriteLine("Hello, logger!");

            // Проверка вызова метода ILogWriter.Write с заданным аргументами
            mock2.Verify(lw => lw.Write("Hello, logger!"));
        }

        [Test]
        public void MoqTestVerify02VerifyCallWithArgumentAndErrorMessage()
        {
            Mock<IFoo> mock = new Mock<IFoo>();
            // Verify with custom error message for failure
            mock.Object.DoSomething("ping2");
            mock.Verify(foo => foo.DoSomething("ping3"), "When doing operation X, the service should be pinged always");
        }

        [Test]
        //Проверка поведения с помощью метода Verify (может быть удобной, когда нужно проверить несколько допущений)
        public void MoqTestVerify03Verify()
        {
            var mock = new Mock<ILogWriter>(MockBehavior.Strict);
            mock.Setup(lw => lw.Write(It.IsAny<string>()));

            var logger = new Logger(mock.Object);
            logger.WriteLine("Hello, logger!");

            // Мы не передаем методу Verify никаких дополнительных параметров.
            // Это значит, что будут использоваться ожидания установленные
            // с помощью mock.Setup
            mock.Verify();
        }

        [Test]
        /*
            Проверка нескольких вызовов с помощью метода Verify
            В некоторых случаях неудобно использовать несколько методов Verify для проверки нескольких вызовов. 
            Вместо этого можно создать мок-объект и задать ожидаемое поведение с помощью методов Setup 
            и проверять все эти допущения путем вызова одного метода Verify(). 
            Такая техника может быть удобной для повторного использования мок-объектов, создаваемых в методе Setup теста.
        */
        public void MoqTestVerify04Verify()
        {
            var mock = new Mock<ILogWriter>(MockBehavior.Strict);
            mock.Setup(lw => lw.Write(It.IsAny<string>()));
            mock.Setup(lw => lw.SetLogger(It.IsAny<string>()));

            var logger = new Logger(mock.Object);
            logger.SetLogger("Test logger");
            logger.WriteLine("Hello, logger!");
            mock.Verify();
        }
    }
}
