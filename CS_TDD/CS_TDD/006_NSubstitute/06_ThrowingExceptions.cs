using System;
using CS_TDD._006_NSubstitute.Setup;
using NSubstitute;
using NUnit.Framework;

namespace CS_TDD._006_NSubstitute
{
    [TestFixture]
    public class _06_ThrowingExceptions
    {
        [Test]
        public void ThrowingExceptions_()
        {
            var calculator = Substitute.For<ICalculator>();


            //Callbacks can be used to throw exceptions when a member is called.

            //For non-voids:
            calculator.Add(-1, -1).Returns(x => { throw new Exception(); });

            //For voids and non-voids:
            calculator
                .When(x => x.Add(-2, -2))
                .Do(x => { throw new Exception(); });

            //Both calls will now throw.
            Assert.Throws<Exception>(() => calculator.Add(-1, -1));
            Assert.Throws<Exception>(() => calculator.Add(-2, -2));
        }
    }
}
