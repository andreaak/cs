﻿using CS_TDD._004_StubsAndMocks._020_NSubstitute.Setup;
using NSubstitute;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._020_NSubstitute
{
    [TestFixture]
    class _08_CheckingReceivedCalls
    {
        [Test]
        public void Should_execute_command()
        {
            //Arrange
            var command = Substitute.For<ICommand>();
            var something = new CommandRunner(command);
            //Act
            something.DoSomething();
            //Assert
            command.Received().Execute();
        }
    }
}
