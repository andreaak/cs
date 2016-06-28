using System.Diagnostics;
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
        public void CodeWithoutMoqTest01()
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
            Mock<IEmpPersonalDetails> mock = new Mock<IEmpPersonalDetails>();
            EmpPfDetails details = new EmpPfDetails(mock.Object);
            
            //Act
            details.GetPfEmployerControlSofar(It.IsAny<int>());
            
            //Assert
            mock.Verify();
        }

        [Test]
        //Интерфейс
        public void MoqTestBase03Interface()
        {
            //Arrange
            Mock<IFoo> mock = new Mock<IFoo>();
            mock.Setup(x => x.DoSomethingWithReturn(It.IsAny<string>())).Returns((string arg) => arg == "Test2");
            mock.Setup(x => x.DoSomethingWithoutReturn(It.IsAny<string>()));
            //Act
            Debug.WriteLine(mock.Object.DoSomethingWithReturn("Test2"));
            mock.Object.DoSomethingWithoutReturn("Test1");
            //Assert
            mock.Verify();
            /*
            True
            */
        }

        [Test]
        //Абстрактный Класс
        public void MoqTestBase03AbstractClass()
        {
            //Arrange
            Mock<LoggerAbstract> mock = new Mock<LoggerAbstract>();
            //mock.Setup(x => x.WriteLinePublic(It.IsAny<string>()));//не мокается
            mock.Setup(x => x.WriteLinePublicVirt(It.IsAny<string>())).Returns((string arg) => arg == "Test1");
            mock.Setup(x => x.WriteLinePublicAbstract(It.IsAny<string>()));
            //Act
            Debug.WriteLine(mock.Object.WriteLinePublicVirt("Test2"));
            mock.Object.WriteLinePublicAbstract("Test1");
            //Assert
            mock.Verify();
            /*
            False
            */
        }

        [Test]
        //Класс
        public void MoqTestBase03Class()
        {
            //Arrange
            Mock<Logger> mock = new Mock<Logger>();
            //mock.Setup(x => x.WriteLinePublic(It.IsAny<string>()));//не мокается
            mock.Setup(x => x.WriteLinePublicVirt(It.IsAny<string>())).Returns((string arg) => arg == "Test3");
            mock.Setup(x => x.WriteLinePublicAbstract(It.IsAny<string>()));
            //Act
            Debug.WriteLine(mock.Object.WriteLinePublicVirt("Test2"));
            mock.Object.WriteLinePublicAbstract("Test1");
            //Assert
            mock.Verify();
            /*
            False
            */
        }

        [Test]
        /*
            Moq поддерживает две модели проверки поведения: строгую (strict) и свободную (loose). 
            По умолчанию используется свободная модель проверок, которая заключается в том, 
            что тестируемый класс (Class Under Test, CUT), во время выполнения действия (в секции Act) 
            может вызывать какие угодно методы наших зависимостей и мы не обязаны указывать их все. 
        */
        public void MoqTestBase04Loose()
        {
            //Arrange
            var mock = new Mock<IEmpPersonalDetails>(MockBehavior.Loose);
            mock.Setup(x => x.GetDurationWorked(It.IsAny<int>())).Returns(20);

            EmpPfDetails details = new EmpPfDetails(mock.Object);
            //Act
            details.GetPfEmployerControlSofar(It.IsAny<int>());
            //Assert
            mock.Verify(x => x.GetDurationWorked(It.IsAny<int>()));
        }

        [Test]

        public void MoqTestBase05Strict()
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
        /*
            В некоторых случаях бывает полезным получить сам мок-объект по интерфейсу (получить Mock<ISomething> по интерфейсу ISomething). 
            Например, функциональный синтаксис инициализации заглушек возвращает не мок-объект, а сразу требуемый интерфейс. 
            Это бывает удобным для тестирования пары простых методов, но неудобным, если понадобится еще и проверить поведение, 
            или задать метод, возвращающий разные результаты для разных параметров. 
            Так что иногда бывает удобно использовать LINQ-based синтаксис для одной части методов и использовать методы Setup – для другой
        */
        public void MoqTestVerify06WithInterface()
        {
            IFoo logger = Mock.Of<IFoo>(ld => ld.DoSomething() == "D:\\Temp"
                                        && ld.Name == "DefaultLogger");

            // Задаем более сложное поведение метода GetDirectoryByLoggerName
            // для возвращения разных результатов, в зависимости от аргумента
            Mock.Get(logger)
                .Setup(ld => ld.DoSomethingWithReturnString(It.IsAny<string>()))
                .Returns<string>(loggerName => "C:\\" + loggerName);

            Assert.That(logger.DoSomething(), Is.EqualTo("D:\\Temp"));
            Assert.That(logger.Name, Is.EqualTo("DefaultLogger"));
            Assert.That(logger.DoSomethingWithReturnString("Foo"), Is.EqualTo("C:\\Foo"));
            Assert.That(logger.DoSomethingWithReturnString("Boo"), Is.EqualTo("C:\\Boo"));
        }

        [Test]
        //Задание поведения нескольких методов одним выражением с помощью “moq functional specification” (появился в Moq v4): 
        public void MoqTestBase07CoupleMethods()
        {
            // Объединяем заглушки разных методов с помощью логического «И»
            IFoo logger = Mock.Of<IFoo>(
                            d => d.DoSomething() == "D:\\Temp" &&
                            d.Name == "DefaultLogger" &&
                            d.DoSomethingWithReturnString(It.IsAny<string>()) == "C:\\Temp");
 
            Assert.That(logger.DoSomething(), Is.EqualTo("D:\\Temp"));
            Assert.That(logger.Name, Is.EqualTo("DefaultLogger"));
            Assert.That(logger.DoSomethingWithReturnString("CustomLogger"), Is.EqualTo("C:\\Temp"));
        }

        [Test]
        //Задание поведение нескольких методов с помощью вызова методов Setup («старый» v3 синтаксис): 
        public void MoqTestBase08CoupleMethods()
        {
            var stub = new Mock<IFoo>();
            stub.Setup(ld => ld.DoSomething()).Returns("D:\\Temp");
            stub.Setup(ld => ld.DoSomethingWithReturnString(It.IsAny<string>())).Returns("C:\\Temp");
            stub.SetupGet(ld => ld.Name).Returns("DefaultLogger");

            IFoo logger = stub.Object;

            Assert.That(logger.DoSomething(), Is.EqualTo("D:\\Temp"));
            Assert.That(logger.Name, Is.EqualTo("DefaultLogger"));
            Assert.That(logger.DoSomethingWithReturnString("CustomLogger"), Is.EqualTo("C:\\Temp"));
        }
    }
}
