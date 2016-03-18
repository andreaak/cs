using CS_TDD._004_StubsAndMocks._018_Moq.Application;
using Moq;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._018_Moq
{
    [TestFixture]
    public class _001_Base
    {
        /*
        Moq – это простой и легковесный изоляционный фреймврк (Isolation Framework), 
        который построен на основе анонимных методов и деревьев выражений. 
        Для создания моков он использует кодогенерацию, поэтому позволяет «мокать» интерфейсы, виртуальные методы (и даже защищенные методы) 
        и не позволяет «мокать» невиртуальные и статические методы. 

        В Moq нет разделения между «стабами» (stubs) и «моками» (mocks) или, более формально, 
        нет разделения на верификацию состояния и верификацию поведения. 
        И хотя в большинстве случаев различия между стабами и моками не так уж и важны, 
        а иногда одна и та же заглушка выполняет обе роли, мы будем рассматривать примеры от простых к сложным, 
        поэтому вначале рассмотрим примеры проверки состояния, а уже потом перейдем к проверке поведения.
        */

        [Test]
        public void MoqTestBase01()
        {
            //Code without Moq

            //Arrange
            EmpPfDetails pfDetails = new EmpPfDetails(new EmpPersonalDetails());
            //Act
            float contrib = pfDetails.GetPfEmployerControlSofar(1);
            //Assert
            Assert.That(contrib, Is.EqualTo(3456), "Its not as expected");
        }

        [Test]
        public void MoqTestBase02()
        {
            //Code with Moq
            //Arrange
            var mock = new Mock<IEmpPersonalDetails>();
            EmpPfDetails details = new EmpPfDetails(mock.Object);
            
            //Act
            details.GetPfEmployerControlSofar(It.IsAny<int>());
            
            //Assert
            mock.Verify();
        }

        [Test]
        /*
            Moq поддерживает две модели проверки поведения: строгую (strict) и свободную (loose). 
            По умолчанию используется свободная модель проверок, которая заключается в том, 
            что тестируемый класс (Class Under Test, CUT), во время выполнения действия (в секции Act) 
            может вызывать какие угодно методы наших зависимостей и мы не обязаны указывать их все. 
        */
        public void MoqTestBase03Loose()
        {
            //Arrange
            var mock = new Mock<IEmpPersonalDetails>(MockBehavior.Loose);

            EmpPfDetails details = new EmpPfDetails(mock.Object);
            //Act
            details.GetPfEmployerControlSofar(It.IsAny<int>());
            //Assert
            mock.Verify(x => x.GetDurationWorked(It.IsAny<int>()));
        }

        [Test]

        public void MoqTestBase04Strict()
        {
            //Arrange
            var mock = new Mock<IEmpPersonalDetails>(MockBehavior.Strict);
            mock.Setup(x => x.GetDurationWorked(It.IsAny<int>())).Returns(20);
            mock.Setup(x => x.GetEmployeeSalary(It.IsAny<int>())).Returns(20);

            EmpPfDetails details = new EmpPfDetails(mock.Object);
            //Act
            details.GetPfEmployerControlSofar(It.IsAny<int>());
            //Assert
            mock.Verify(x => x.GetDurationWorked(It.IsAny<int>()));
        }

        [Test]
        //Задание поведения нескольких методов одним выражением с помощью “moq functional specification” (появился в Moq v4): 
        public void MoqTestBase05CoupleMethods()
        {
            // Объединяем заглушки разных методов с помощью логического «И»
            IFoo logger = Mock.Of<IFoo>(
                            d => d.DoSomething() == "D:\\Temp" &&
                            d.Name == "DefaultLogger" &&
                            d.DoSomething2(It.IsAny<string>()) == "C:\\Temp");
 
            Assert.That(logger.DoSomething(), Is.EqualTo("D:\\Temp"));
            Assert.That(logger.Name, Is.EqualTo("DefaultLogger"));
            Assert.That(logger.DoSomething2("CustomLogger"), Is.EqualTo("C:\\Temp"));
        }

        [Test]
        //Задание поведение нескольких методов с помощью вызова методов Setup («старый» v3 синтаксис): 
        public void MoqTestBase06CoupleMethods()
        {
            var stub = new Mock<IFoo>();
            stub.Setup(ld => ld.DoSomething()).Returns("D:\\Temp");
            stub.Setup(ld => ld.DoSomething2(It.IsAny<string>())).Returns("C:\\Temp");
            stub.SetupGet(ld => ld.Name).Returns("DefaultLogger");

            IFoo logger = stub.Object;

            Assert.That(logger.DoSomething(), Is.EqualTo("D:\\Temp"));
            Assert.That(logger.Name, Is.EqualTo("DefaultLogger"));
            Assert.That(logger.DoSomething2("CustomLogger"), Is.EqualTo("C:\\Temp"));
        }

        [Test]
        public void MoqTestBase07VerifyCallWithAnyArgument()
        {
            var mock = new Mock<ILogWriter>();
            Logger logger = new Logger(mock.Object);

            logger.WriteLine("Hello, logger!");

            // Проверяем, что вызвался метод Write нашего мока с любым аргументом
            mock.Verify(lw => lw.Write(It.IsAny<string>()));
        }

        [Test]
        public void MoqTestBase08VerifyCallWithArgument()
        {
            Mock<IFoo> mock = new Mock<IFoo>();
            mock.Object.DoSomething("ping");
            mock.Verify(foo => foo.DoSomething("ping"));
            
            // Verify with custom error message for failure
            mock.Object.DoSomething("ping2");
            mock.Verify(foo => foo.DoSomething("ping2"), "When doing operation X, the service should be pinged always");
            
            var mock2 = new Mock<ILogWriter>();
            Logger logger = new Logger(mock2.Object);

            logger.WriteLine("Hello, logger!");

            // Проверка вызова метода ILogWriter.Write с заданным аргументами
            mock2.Verify(lw => lw.Write("Hello, logger!"));
        }

        [Test]
        //Проверка поведения с помощью метода Verify (может быть удобной, когда нужно проверить несколько допущений)
        public void MoqTestBase09Verify()
        {
            var mock = new Mock<ILogWriter>();
            mock.Setup(lw => lw.Write(It.IsAny<string>()));

            var logger = new Logger(mock.Object);
            logger.WriteLine("Hello, logger!");

            // Мы не передаем методу Verify никаких дополнительных параметров.
            // Это значит, что будут использоваться ожидания установленные
            // с помощью mock.Setup
            mock.Verify();
        }

        [Test]
        //Проверка нескольких вызовов с помощью метода Verify
        /*
            В некоторых случаях неудобно использовать несколько методов Verify для проверки нескольких вызовов. 
            Вместо этого можно создать мок-объект и задать ожидаемое поведение с помощью методов Setup 
            и проверять все эти допущения путем вызова одного метода Verify(). 
            Такая техника может быть удобной для повторного использования мок-объектов, создаваемых в методе Setup теста.
        */
        public void MoqTestBase10Verify()
        {
            var mock = new Mock<ILogWriter>();
            mock.Setup(lw => lw.Write(It.IsAny<string>()));
            mock.Setup(lw => lw.SetLogger(It.IsAny<string>()));

            var logger = new Logger(mock.Object);
            logger.WriteLine("Hello, logger!");

            mock.Verify();
        }
        
        [Test]
        //Проверка нескольких вызовов с помощью метода Verify
        /*
            В некоторых случаях бывает полезным получить сам мок-объект по интерфейсу (получить Mock<ISomething> по интерфейсу ISomething). 
            Например, функциональный синтаксис инициализации заглушек возвращает не мок-объект, а сразу требуемый интерфейс. 
            Это бывает удобным для тестирования пары простых методов, но неудобным, если понадобится еще и проверить поведение, 
            или задать метод, возвращающий разные результаты для разных параметров. 
            Так что иногда бывает удобно использовать LINQ-based синтаксис для одной части методов и использовать методы Setup – для другой
        */
        public void MoqTestBase11Verify()
        {
            IFoo logger = Mock.Of<IFoo>( ld => ld.DoSomething() == "D:\\Temp"
                                        && ld.Name == "DefaultLogger");

            // Задаем более сложное поведение метода GetDirectoryByLoggerName
            // для возвращения разных результатов, в зависимости от аргумента
            Mock.Get(logger)
                .Setup(ld => ld.DoSomething2(It.IsAny<string>()))
                .Returns<string>(loggerName => "C:\\" + loggerName);

            Assert.That(logger.DoSomething(), Is.EqualTo("D:\\Temp"));
            Assert.That(logger.Name, Is.EqualTo("DefaultLogger"));
            Assert.That(logger.DoSomething2("Foo"), Is.EqualTo("C:\\Foo"));
            Assert.That(logger.DoSomething2("Boo"), Is.EqualTo("C:\\Boo"));
        }
    }
}
