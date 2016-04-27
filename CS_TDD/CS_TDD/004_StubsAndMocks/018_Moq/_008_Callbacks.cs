using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS_TDD._004_StubsAndMocks._018_Moq.Application;
using Moq;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._018_Moq
{
    [TestFixture]
    public class _008_Callbacks
    {
        [Test]
        public void MoqTestCallbacks()
        {
            int callsCount = 0;
            List<string> calls = new List<string>();

            Mock<IFoo> mock = new Mock<IFoo>();
            mock.Setup(foo => foo.DoSomething("ping"))
                .Returns(true)
                .Callback(() => callsCount++);

            // access invocation arguments
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Returns(true)
                .Callback((string s) => calls.Add(s));

            // alternate equivalent generic method syntax
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Returns(true)
                .Callback<string>(s => calls.Add(s));

            // access arguments for methods with multiple parameters
            mock.Setup(foo => foo.DoSomething(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(true)
                .Callback<int, string>((i, s) => calls.Add(s));

            // callbacks can be specified before and after invocation
            mock.Setup(foo => foo.DoSomething("ping"))
                .Callback(() => Console.WriteLine("Before returns"))
                .Returns(true)
                .Callback(() => Console.WriteLine("After returns"));
        }
    }
}
