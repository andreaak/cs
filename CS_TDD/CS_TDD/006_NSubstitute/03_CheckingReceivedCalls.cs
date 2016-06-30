using System;
using System.Collections.Generic;
using CS_TDD._006_NSubstitute.Setup;
using NSubstitute;
using NUnit.Framework;

namespace CS_TDD._006_NSubstitute
{
    [TestFixture]
    public class _03_CheckingReceivedCalls
    {
        [Test]
        public void TestCheckingReceivedCalls_()
        {
            /*
            In some cases (particularly for void methods) it is useful to check that a specific call has been received by a substitute. 
            This can be checked using the Received() extension method, followed by the call being checked. 
            */

            //Arrange
            var command = Substitute.For<ICommand>();
            var something = new CommandRunner(command);
            //Act
            something.DoSomething();
            //Assert
            command.Received().Execute();

            /*
            In this case command did receive a call to Execute(), and so will complete successfully. 
            If Execute() has not been received NSubstitute will throw a ReceivedCallsException 
            and let you know what call was expected and with which arguments, 
            as well as listing actual calls to that method and which the arguments differed. 
             */
        }

        [Test]
        public void TestCheckingReceivedCalls_CallWasNotReceived()
        {
            /*
            NSubstitute can also make sure a call was not received using the DidNotReceive() extension method. 
            */

            var command = Substitute.For<ICommand>();
            var something = new CommandRunner(command);
            //Act
            something.DontDoAnything();
            //Assert
            command.DidNotReceive().Execute();
        }

        [Test]
        public void TestCheckingReceivedCalls_CallWasReceivedSpecificNumberOfTimes()
        {
            /*
            The Received() extension method will assert that at least one call was made to a member, 
            and DidNotReceive() asserts that zero calls were made. 
            NSubstitute also gives you the option of asserting a specific number of calls were received by passing an integer to Received(). 
            This will throw if the substitute does not receive exactly that many matching calls. Too few, or too many, and the assertion will fail. 
            */

            var command = Substitute.For<ICommand>();
            var repeater = new CommandRunner(command);
            //Act
            repeater.DoSomething();
            repeater.DoSomething();
            repeater.DoSomething();
            //Assert
            command.Received(3).Execute(); // << This will fail if 2 or 4 calls were received

            /*
            We can also use Received(1) to check a call was received once and only once. 
            This differs from the standard Received() call, which checks a call was received at least once. 
            Received(0) behaves the same as DidNotReceive(). 
             */
        }


        [Test]
        public void TestCheckingReceivedCalls_ReceivedWithSpecificArguments()
        {
            var calculator = Substitute.For<ICalculator>();

            /*
            We can also use argument matchers to check calls were received (or not) with particular arguments. 
            This is covered in more detail in the argument matchers topic, but the following examples show the general idea 
            */

            calculator.Add(1, 2);
            calculator.Add(-100, 100);

            //Check received with second arg of 2 and any first arg:
            calculator.Received().Add(Arg.Any<int>(), 2);
            //Check received with first arg less than 0, and second arg of 100:
            calculator.Received().Add(Arg.Is<int>(x => x < 0), 100);
            //Check did not receive a call where second arg is >= 500 and any first arg:
            calculator
                .DidNotReceive()
                .Add(Arg.Any<int>(), Arg.Is<int>(x => x >= 500));
        }

        [Test]
        public void TestCheckingReceivedCalls_IgnoringArguments()
        {
            var calculator = Substitute.For<ICalculator>();

            /*
            NSubstitute can also check calls were received or not received but ignore the arguments used, just like we can for setting returns for any arguments. 
            In this case we need ReceivedWithAnyArgs() and DidNotReceiveWithAnyArgs(). 
            */

            calculator.Add(1, 3);

            calculator.ReceivedWithAnyArgs().Add(1, 1);
            calculator.DidNotReceiveWithAnyArgs().Subtract(0, 0);
        }

        [Test]
        public void TestCheckingReceivedCalls_CheckingCallsToProperties()
        {
            var calculator = Substitute.For<ICalculator>();

            /*
            The same syntax can be used to check calls on properties. Normally we’d want to avoid this, 
            as we’re really more interested in testing the required behaviour rather than the precise implementation details 
            (i.e. we would set the property to return a value and check that was used properly, rather than assert that the property getter was called). 
            Still, there are probably times when checking getters and setters were called can come in handy, so here’s how you do it 
            */

            var mode = calculator.Mode;
            calculator.Mode = "TEST";

            //Check received call to property getter
            //We need to assign the result to a variable to keep
            //the compiler happy.
            var temp = calculator.Received().Mode;

            //Check received call to property setter with arg of "TEST"
            calculator.Received().Mode = "TEST";
        }

        [Test]
        public void TestCheckingReceivedCalls_CheckingCallsIndexers()
        {
            var calculator = Substitute.For<ICalculator>();

            /*
            An indexer is really just another property, so we can use the same syntax to check calls to indexers 
            */

            var dictionary = Substitute.For<IDictionary<string, int>>();
            dictionary["test"] = 1;

            dictionary.Received()["test"] = 1;
            dictionary.Received()["test"] = Arg.Is<int>(x => x < 5);
        }

        [Test]
        public void TestCheckingReceivedCalls_ClearingReceivedCalls()
        {
            /*
            A substitute can forget all the calls previously made to it using the ClearReceivedCalls() extension method.
            Say we have an ICommand interface, and we want a OnceOffCommandRunner that will take an ICommand and only run it once. 
            */

            var command = Substitute.For<ICommand>();
            var runner = new CommandRunner(command);

            //First run
            runner.DoSomething();
            command.Received().Execute();

            //Forget previous calls to command
            command.ClearReceivedCalls();

            //Second run
            runner.DoSomething();
            command.DidNotReceive().Execute();

            /*
            ClearReceivedCalls() will not clear any results set up for the substitute using Returns(). 
            If we need to this, we can replace previously specified results by calling Returns() again.
            */
        }
    }
}
