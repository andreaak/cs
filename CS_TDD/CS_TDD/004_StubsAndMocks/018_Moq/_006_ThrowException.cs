using System;
using CS_TDD._004_StubsAndMocks._018_Moq.Setup;
using Moq;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._018_Moq
{
    [TestFixture]
    public class _006_ThrowException
    {
        [Test]
        public void MoqTest5()
        {
            var mock = new Mock<IFoo>();
            // Multiple parameters overloads available
            // throwing when invoked
            mock.Setup(foo => foo.DoSomethingWithReturnBool("reset")).Throws<InvalidOperationException>();
            mock.Setup(foo => foo.DoSomethingWithReturnBool("")).Throws(new ArgumentException("command"));
        }
    }
}
