using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using CS_TDD._004_StubsAndMocks._018_Moq.Application;
using Moq;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._018_Moq
{
    [TestFixture]
    public class _003_Arguments
    {
        [Test]
        public void MoqTestMatchingArguments1()
        {
            Employee employee = new Employee()
            {
                EmpId = 1,
                Name = "Karthik",
                Salary = 4000,
                DurationWorked = 24,
                Grade = 1,
                Email = "karthik@executeautomation.com"
            };

            //Arrange
            var moqPersonalDetail = new Mock<IEmpPersonalDetails>();
            moqPersonalDetail.Setup(x => x.IsValidEmail(It.IsAny<string>()));
            var empDetails = new EmployeesDetails(moqPersonalDetail.Object);
            //Act
            empDetails.GetEmployeeValidEmailAddress(employee);
            //Assert
            moqPersonalDetail.Verify(x => x.IsValidEmail(
                It.Is<string>(arg => arg.Equals(employee.Email))));
        }
        
        [Test]
        public void MoqTestMatchingArguments2()
        {
            var mock = new Mock<IFoo>();
            mock.Setup(foo => foo.DoSomething("ping")).Returns(true);

            // out arguments
            string outString = "ack";
            // TryParse will return true, and the out argument will return "ack", lazy evaluated
            mock.Setup(foo => foo.TryParse("ping", out outString)).Returns(true);

            // ref arguments
            Bar instance = new Bar();
            // Only matches if the ref argument to the invocation is the same instance
            mock.Setup(foo => foo.Submit(ref instance)).Returns(true);

            IFoo objFoo = mock.Object;
            string temp;
            Debug.WriteLine("Result {0}, Value {1}", objFoo.TryParse("ping", out temp), temp);

            Debug.WriteLine("Result {0}, Value {1}", objFoo.Submit(ref instance), instance);
            Bar bar = new Bar();
            Debug.WriteLine("Result {0}, Value {1}", objFoo.Submit(ref bar), bar);

            /*
            Result True, Value ack
            Result True, Value CS_TDD._004_StubsAndMocks._018_Moq.Application.Bar
            Result False, Value CS_TDD._004_StubsAndMocks._018_Moq.Application.Bar
            */
        }
        
        [Test]
        public void MoqTestMatchingArguments3()
        {
            var mock = new Mock<IFoo>();
            // any value
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>())).Returns(true);


            // matching Func<int>, lazy evaluated
            mock.Setup(foo => foo.Add(It.Is<int>(i => i % 2 == 0))).Returns(true);


            // matching ranges
            mock.Setup(foo => foo.Add(It.IsInRange<int>(0, 10, Range.Inclusive))).Returns(true);


            // matching regex
            mock.Setup(x => x.DoSomething2(It.IsRegex("[a-d]+", RegexOptions.IgnoreCase))).Returns("foo");
        }
    }
}
