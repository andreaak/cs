﻿using System.Diagnostics;
using CS_TDD._004_StubsAndMocks._018_Moq.Setup;
using Moq;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._018_Moq
{
    [TestFixture]
    public class _04_ReturnValue
    {
        [Test]
        public void MoqTestReturnValue1()
        {
            //Arrange
            var moqEmpPersonalDetails = new Mock<IEmpPersonalDetails>();
            moqEmpPersonalDetails.Setup(x => x.GetEmployeeSalary(It.IsAny<int>()))
                .Returns(() => 5000);

            var pfDetails = new EmpPfDetails(moqEmpPersonalDetails.Object);
            //Act
            bool isEligible = pfDetails.IsPfEligible(It.IsAny<int>());
            //Assert
            Assert.IsTrue(isEligible, "Should be true");
        }

        [Test]
        public void MoqTestReturnValue2()
        {
            var mock = new Mock<IFoo>();
            mock.Setup(foo => foo.DoSomethingWithReturnBool("ping")).Returns(true);

            // access invocation arguments when returning a value
            mock.Setup(x => x.DoSomethingWithReturnString(It.IsAny<string>()))
                    .Returns((string s) => s.ToLower());
        }

        [Test]
        public void MoqTestReturnValue3ReturnDifferentValueEachInvocation()
        {
            // returning different values on each invocation
            var mock = new Mock<IFoo>();
            int calls = 0;
            mock.Setup(foo => foo.GetCountThing())
                .Returns(() => calls)
                .Callback(() => calls++);
            // returns 0 on first invocation, 1 on the next, and so on
            Debug.WriteLine(mock.Object.GetCountThing());
        }

        [Test]
        public void MoqTestReturnValue4ReturnOneResult()
        {
            // Mock.Of возвращает саму зависимость (прокси-объект), а не мок-объект.
            // Следующий код означает, что при вызове DoSomething() мы получим "D:\\Temp"
            IFoo mock = Mock.Of<IFoo>(d => d.DoSomethingWithReturnString() == "D:\\Temp");
            string currentDirectory = mock.DoSomethingWithReturnString();

            Assert.That(currentDirectory, Is.EqualTo("D:\\Temp"));
        }
        
        [Test]
        public void MoqTestReturnValue5ReturnOneResultWithArgument()
        {
            // Для любого аргумента метода DoSomething2 вернуть "C:\\Foo".
            IFoo loggerDependency = Mock.Of<IFoo>(
                ld => ld.DoSomethingWithReturnString(It.IsAny<string>()) == "C:\\Foo");

            string directory = loggerDependency.DoSomethingWithReturnString("anything");

            Assert.That(directory, Is.EqualTo("C:\\Foo"));
        }
        
        [Test]
        public void MoqTestReturnValue5ReturnResultDependedFromArgument()
        {
            // Инициализируем заглушку таким образом, чтобы возвращаемое значение
            // метода GetDirrectoryByLoggerName зависело от аргумента метода.
            // Код аналогичен заглушке вида:
            // public string DoSomething2(string s) { return "C:\\" + s; }
            Mock<IFoo> stub = new Mock<IFoo>();

            stub.Setup(ld => ld.DoSomethingWithReturnString(It.IsAny<string>()))
                .Returns<string>(name => "C:\\" + name);

            string loggerName = "SomeLogger";
            IFoo logger = stub.Object;
            string directory = logger.DoSomethingWithReturnString(loggerName);
            Assert.That(directory, Is.EqualTo("C:\\" + loggerName));
        }
    }
}
