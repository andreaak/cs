using System.Collections.Generic;
using System.Linq;
using CS_TDD._004_StubsAndMocks._020_NSubstitute.Setup;
using NSubstitute;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._020_NSubstitute
{
    [TestFixture]
    public class _04_ArgumentMatchers
    {
        /*
        Argument matchers can be used when setting return values and when checking received calls. 
        They provide a way to specify a call or group of calls, 
        so that a return value can be set for all matching calls, or to check a matching call has been received.
        */

        [Test]
        public void TestArgumentMatchers_IgnoringArguments()
        {
            var calculator = Substitute.For<ICalculator>();

            //An argument of type T can be ignored using Arg.Any<T>().
            calculator.Add(Arg.Any<int>(), 5).Returns(7);

            Assert.AreEqual(7, calculator.Add(42, 5));
            Assert.AreEqual(7, calculator.Add(123, 5));
            Assert.AreNotEqual(7, calculator.Add(1, 7));
            /*
            In this example we return 7 when adding any number to 5.We use Arg.Any<int>() to tell NSubstitute to ignore the first argument.
            */

            //We can also use this to match any argument of a specific sub-type.
            var formatter = Substitute.For<IFormatter>();
            formatter.Format(new object());
            formatter.Format("some string");
            //formatter.Format(5);

            formatter.Received().Format(Arg.Any<object>());
            formatter.Received().Format(Arg.Any<string>());
            formatter.DidNotReceive().Format(Arg.Any<int>());
        }

        [Test]
        public void TestArgumentMatchers_ConditionallyMatchingArgument()
        {
            var calculator = Substitute.For<ICalculator>();

            //An argument of type T can be conditionally matched using Arg.Is<T>(Predicate<T> condition).

            calculator.Add(1, -10);

            //Received call with first arg 1 and second arg less than 0:
            calculator.Received().Add(1, Arg.Is<int>(x => x < 0));
            //Received call with first arg 1 and second arg of -2, -5, or -10:
            calculator
                .Received()
                .Add(1, Arg.Is<int>(x => new[] { -2, -5, -10 }.Contains(x)));
            //Did not receive call with first arg greater than 10:
            calculator.DidNotReceive().Add(Arg.Is<int>(x => x > 10), -10);

            //If the condition throws an exception for an argument, then it will be assumed that the argument does not match. The exception itself will be swallowed.
            var formatter = Substitute.For<IFormatter>();

            formatter.Format(Arg.Is<string>(x => x.Length <= 10)).Returns("matched");

            Assert.AreEqual("matched", formatter.Format("short"));
            Assert.AreNotEqual("matched", formatter.Format("not matched, too long"));
            // Will not match because trying to access .Length on null will throw an exception when testing
            // our condition. NSubstitute will assume it does not match and swallow the exception.
            Assert.AreNotEqual("matched", formatter.Format(null));
        }

        [Test]
        public void TestArgumentMatchers_MatchingSpecificArgument()
        {
            var calculator = Substitute.For<ICalculator>();

            //An argument of type T can be matched using Arg.Is<T>(T value).

            calculator.Add(0, 42);

            //This won't work; NSubstitute isn't sure which arg the matcher applies to:
            //calculator.Received().Add(0, Arg.Any<int>());

            calculator.Received().Add(Arg.Is(0), Arg.Any<int>());

            /*
            This matcher normally isn’t required; most of the time we can just use 0 instead of Arg.Is(0).
            In some cases though, NSubstitute can’t work out which matcher applies to which argument 
            (arg matchers are actually fuzzily matched; not passed directly to the function call). 
            In these cases it will throw an AmbiguousArgumentsException and ask you to specify one or more additional argument matchers. 
            In some cases you may have to explicitly use argument matchers for every argument.
            */
        }

        /*
        In addition to specifying calls, matchers can also be used to perform a specific action with an argument whenever a matching call is made to a substitute. 
        This is a fairly rare requirement, but can make test setup a little simpler in some cases.
        Warning: Once we start adding non-trivial behaviour to our substitutes we run the risk of over-specifying or tightly coupling our tests and code. 
        It may be better to pick a different abstraction that better encapsulates this behaviour, 
        or even use a real collaborator and switch to coarser grained tests for the class being tested.
        */

        [Test]
        public void TestArgumentMatchers_InvokingCallbacks()
        {
            /*
            Say our class under test needs to call a method on a dependency, 
            and provide a callback so it can be notified when the dependent object has finished. 
            We can use Arg.Invoke() to immediately invoke the callback argument as soon as the substitute is called.
            Let’s look at a contrived example. Say we are testing an OrderPlacedCommand, which needs to use an IOrderProcessor to process the order, 
            then raise and event using IEvents when this completes successfully. 
            */


            /*
            In our test we can use Arg.Invoke to simulate the situation where the IOrderProcessor finishes processing the order 
            and invokes the callback to tell the calling code it is finished. 
             */
            //Arrange
            var cart = Substitute.For<ICart>();
            var events = Substitute.For<IEvents>();
            var processor = Substitute.For<IOrderProcessor>();
            cart.OrderId = 3;
            //Arrange for processor to invoke the callback arg with `true` whenever processing order id 3
            processor.ProcessOrder(3, Arg.Invoke(true));

            //Act
            var command = new OrderPlacedCommand(processor, events);
            command.Execute(cart);

            //Assert
            events.Received().RaiseOrderProcessed(3);

            /*
            Here we setup the processor to invoke the callback whenever processing an order with id 3. 
            We set it up to pass true to this callback using Arg.Invoke(true).
            There are several overloads to Arg.Invoke that let us invoke callbacks with varying numbers and types of arguments. 
            We can also invoke custom delegate types (those that are not just simple Action delegates) using Arg.InvokeDelegate. 
             */
        }

        [Test]
        public void TestArgumentMatchers_PerformingActionsWithArguments()
        {
            /*
            Sometimes we may not want to invoke a callback immediately. 
            Or maybe we want to store all instances of a particular argument passed to a method. 
            Or even just capture a single argument for inspection later. 
            We can use Arg.Do for these purposes. Arg.Do calls the action we give it with the argument used for each matching call. 
            */

            var calculator = Substitute.For<ICalculator>();

            var argumentUsed = 0;
            calculator.Multiply(Arg.Any<int>(), Arg.Do<int>(x => argumentUsed = x));

            calculator.Multiply(123, 42);

            Assert.AreEqual(42, argumentUsed);

            /*
            Here we specify that a call to Multiply with any first argument should pass the second argument and put it in the argumentUsed variable. 
            This can be quite useful when we want to assert several properties on an argument (for types more complex than int that is). 
             */

            var firstArgsBeingMultiplied = new List<int>();
            calculator.Multiply(Arg.Do<int>(x => firstArgsBeingMultiplied.Add(x)), 10);

            calculator.Multiply(2, 10);
            calculator.Multiply(5, 10);
            calculator.Multiply(7, 4567); //Will not match our Arg.Do as second arg is not 10

            Assert.AreEqual(firstArgsBeingMultiplied, new[] { 2, 5 });

            /*Here our Arg.Do takes whatever int is passed as the first argument to Multiply and adds it to a list whenever the second argument is 10.*/
        }

        [Test]
        public void TestArgumentMatchers_ArgumentActionsAndCallSpecification()
        {
            /*
            Argument actions act just like the Arg.Any<T>() argument matcher in that they specify a call where that argument is any type compatible with T 
            (and so can be used for setting return values and checking received calls). 
            They just have the added element of interacting with a specific argument of any call that matches that specification. 
            */

            var calculator = Substitute.For<ICalculator>();

            var numberOfCallsWhereFirstArgIsLessThan0 = 0;
            //Specify a call where the first arg is less than 0, and the second is any int.
            //When this specification is met we'll increment a counter in the Arg.Do action for 
            //the second argument that was used for the call, and we'll also make it return 123.
            calculator
                .Multiply(
                    Arg.Is<int>(x => x < 0),
                    Arg.Do<int>(x => numberOfCallsWhereFirstArgIsLessThan0++)
                ).Returns(123);

            var results = new[] {
                calculator.Multiply(-4, 3),
                calculator.Multiply(-27, 88),
                calculator.Multiply(-7, 8),
                calculator.Multiply(123, 2) //First arg greater than 0, so spec won't be met.
            };

            Assert.AreEqual(3, numberOfCallsWhereFirstArgIsLessThan0); //3 of 4 calls have first arg < 0
            Assert.AreEqual(results, new[] { 123, 123, 123, 0 }); //Last call returns 0, not 123
        }
    }
}
