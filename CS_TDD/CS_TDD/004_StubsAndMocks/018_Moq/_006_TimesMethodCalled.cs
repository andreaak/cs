using System.Collections.Generic;
using CS_TDD._004_StubsAndMocks._018_Moq.Application;
using Moq;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._018_Moq
{
    class _006_TimesMethodCalled
    {
        [Test]
        public void MoqTestTimesMethodCalled1()
        {
            Mock<IFoo> mock = new Mock<IFoo>();

            mock.Object.DoSomething("Test");
            // Method should never be called
            mock.Verify(foo => foo.DoSomething("ping"), Times.Never());

            mock.Object.DoSomething("ping");
           
            // Проверка того, что метод ILogWriter.Write вызвался в точности один раз (ни больше, ни меньше)
            mock.Verify(foo => foo.DoSomething("ping"), Times.Once());
            
            // Проверка того, что метод DoSomething вызвался по крайней мере один раз
            mock.Verify(foo => foo.DoSomething("ping"), Times.AtLeastOnce());
        }

        [Test]
        public void MoqTestTimesMethodCalled3()
        {
            //Arrange
            var moqPersonalDetail = new Mock<IEmpPersonalDetails>();

            var pfDetail = new EmpPfDetails(moqPersonalDetail.Object);

            //Act
            pfDetail.IsPfEligible(It.IsAny<int>());

            //Assert
            moqPersonalDetail.Verify(x => x.GetEmployeeSalary(It.IsAny<int>()), Times.Exactly(1));
        }

        [Test]
        public void MoqTest4TimesMethodCalled4Complex()
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
            var empDetails = new EmployeesDetails(moqPersonalDetail.Object);
            //Act
            empDetails.GetHigherGradeEmployee(employees);
            //Assert
            moqPersonalDetail.Verify(x => x.GetEmployeeDetails(It.IsAny<int>()), Times.Exactly(employees.Count));
        }
    }
}
