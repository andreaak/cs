using System.Collections.Generic;
using CS_TDD._004_StubsAndMocks._018_Moq.Setup;
using Moq;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._018_Moq
{
    [TestFixture]
    public class _05_Properties
    {
        [Test]
        public void MoqTestProperty1()
        {
            var mock = new Mock<IFoo>();

            mock.Setup(foo => foo.Name).Returns("bar");


            // auto-mocking hierarchies (a.k.a. recursive mocks)
            //mock.Setup(foo => foo.Bar.Baz.Name).Returns("baz");

            // Verify getter invocation, regardless of value.
            mock.VerifyGet(foo => foo.Name);
            
            // Verify setter invocation, regardless of value.
            mock.VerifySet(foo => foo.Name);
            
            // expects an invocation to set the value to "foo"
            mock.SetupSet(foo => foo.Name = "foo");

            // or verify the setter directly
            // Verify setter called with specific value
            mock.VerifySet(foo => foo.Name = "foo");

            // Verify setter with an argument matcher
            mock.VerifySet(foo => foo.Value = It.IsInRange(1, 5, Range.Inclusive));

        }

        [Test]
        //Setup a property so that it will automatically start tracking its value (also known as Stub)
        public void MoqTestProperty2Stub()
        {
            var mock = new Mock<IFoo>();

            // start "tracking" sets/gets to this property
            mock.SetupProperty(f => f.Name);

            // alternatively, provide a default value for the stubbed property
            mock.SetupProperty(f => f.Name, "foo");


            // Now you can do:

            IFoo foo = mock.Object;
            // Initial value was stored
            Assert.AreEqual("foo", foo.Name);

            // New value set which changes the initial value
            foo.Name = "bar";
            Assert.AreEqual("bar", foo.Name);
            
            // Свойство Name нашей заглушки будет возвращать указанное значение
            IFoo logger = Mock.Of<IFoo>( d => d.Name == "DefaultLogger");
            string defaultLogger = logger.Name;
            Assert.That(defaultLogger, Is.EqualTo("DefaultLogger"));
        }

        [Test]
        //Контролирует было ли установленно свойство
        public void MoqTestProperty3Set()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    EmpId = 1,
                    Name = "Karthik",
                    Salary = 4000,
                    DurationWorked = 24,
                    Grade = 1,
                    Email = "karthik@executeautomation.com"
                },

                new Employee()
                {
                    EmpId = 2,
                    Name = "Prashanth",
                    Salary = 7000,
                    DurationWorked = 30,
                    Grade = 2,
                    Email = "prashanth@executeautomation.com"

                }
            };

            //Arrange
            var moqPersonalDetail = new Mock<IEmpPersonalDetails>();
            var pfDetails = new EmployeesDetails(moqPersonalDetail.Object);

            //Act
            pfDetails.GetGratuityEligibleCount(employees);

            //Assert
            moqPersonalDetail.VerifySet(x => x.GraduityEligibleCount = It.IsAny<int>());
        }

        [Test]
        public void MoqTestProperty4Get()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    EmpId = 1,
                    Name = "Karthik",
                    Salary = 4000,
                    DurationWorked = 24,
                    Grade = 1,
                    Email = "karthik@executeautomation.com"
                },

                new Employee()
                {
                    EmpId = 2,
                    Name = "Prashanth",
                    Salary = 7000,
                    DurationWorked = 30,
                    Grade = 2,
                    Email = "prashanth@executeautomation.com"

                }
            };

            //Arrange
            var moqPersonalDetail = new Mock<IEmpPersonalDetails>();

            moqPersonalDetail.Setup(x => x.GraduityEligibleCount).Returns(2);

            var pfDetails = new EmployeesDetails(moqPersonalDetail.Object);

            //Act
            pfDetails.GetGratuityEligibleCount2(employees);

            //Assert
            moqPersonalDetail.VerifyGet(x => x.GraduityEligibleCount);
        }

        [Test]
        public void MoqTestProperty5Get()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    EmpId = 1,
                    Name = "Karthik",
                    Salary = 4000,
                    DurationWorked = 24,
                    Grade = 1,
                    Email = "karthik@executeautomation.com"
                },

                new Employee()
                {
                    EmpId = 2,
                    Name = "Prashanth",
                    Salary = 7000,
                    DurationWorked = 30,
                    Grade = 2,
                    Email = "prashanth@executeautomation.com"

                }
            };

            //Arrange
            var moqPersonalDetail = new Mock<IEmpPersonalDetails>();
            //Stubbing a property
            moqPersonalDetail.SetupProperty(x => x.GraduityEligibleCount, 2);

            var pfDetails = new EmployeesDetails(moqPersonalDetail.Object);

            //Act
            pfDetails.GetGratuityEligibleCount2(employees);

            //Assert
            moqPersonalDetail.VerifyGet(x => x.GraduityEligibleCount);
        }

        [Test]
        public void MoqTestProperty6Get()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    EmpId = 1,
                    Name = "Karthik",
                    Salary = 4000,
                    DurationWorked = 24,
                    Grade = 1,
                    Email = "karthik@executeautomation.com"
                },

                new Employee()
                {
                    EmpId = 2,
                    Name = "Prashanth",
                    Salary = 7000,
                    DurationWorked = 30,
                    Grade = 2,
                    Email = "prashanth@executeautomation.com"

                }
            };

            //Arrange
            var moqPersonalDetail = new Mock<IEmpPersonalDetails>();
            //Stubbing a property
            moqPersonalDetail.SetupAllProperties();
            moqPersonalDetail.Object.GraduityEligibleCount = 2;

            var pfDetails = new EmployeesDetails(moqPersonalDetail.Object);

            //Act
            pfDetails.GetGratuityEligibleCount2(employees);

            //Assert
            moqPersonalDetail.VerifyGet(x => x.GraduityEligibleCount);
        }
    }
}
