using System;
using System.Collections.Generic;
using CS_TDD._004_StubsAndMocks._018_Moq.Setup;
using CS_TDD._006_NSubstitute.Setup;
using NSubstitute;
using NUnit.Framework;

namespace CS_TDD._006_NSubstitute
{
    [TestFixture]
    public class _05_Callbacks
    {
        [Test]
        public void TestCallbacks_ReturnValue()
        {
            var calculator = Substitute.For<ICalculator>();

            /*
            This technique can also be used to get a callback whenever a call is made
            */
            var counter = 0;
            calculator
                .Add(0, 0)
                .ReturnsForAnyArgs(x =>
                {
                    counter++;
                    return 0;
                });

            calculator.Add(7, 3);
            calculator.Add(2, 2);
            calculator.Add(11, -3);
            Assert.AreEqual(counter, 3);

            //Alternatively the callback can be specified after the Returns using AndDoes:
            var counter2 = 0;
            calculator
                .Add(0, 0)
                .ReturnsForAnyArgs(x => 0)
                .AndDoes(x => counter2++);

            calculator.Add(7, 3);
            calculator.Add(2, 2);
            Assert.AreEqual(counter2, 2);
        }

        [Test]
        public void TestCallbacks_WhenCalledDoThis()
        {
            /*
            Callbacks for void calls
            Returns() can be used to get callbacks for members that return a value, but for void members we need a different technique, 
            because we can’t call a method on a void return. For these cases we can use the When..Do syntax.

            When..Do uses two calls to configure our callback. First, When() is called on the substitute and passed a function. 
            The argument to the function is the substitute itself, and we can call the member we are interested in here, even if it returns void. 
            We then call Do() and pass in our callback that will be executed when the substitute’s member is called.
             */
            var counter = 0;
            var foo = Substitute.For<IFoo>();
            foo.When(x => x.DoSomething("World"))
                .Do(x => counter++);

            foo.DoSomething("World");
            foo.DoSomething("World");
            Assert.AreEqual(2, counter);

            /*
            The argument passed to the Do() method is the same call information passed to the Returns() callback, 
            which gives us access to the arguments used for the call.

            Note that we can also use When..Do syntax for non-void members, 
            but generally the Returns() syntax is preferred for brevity and clarity. 
            You may still find it useful for non-voids when you want to execute a function without changing a previous return value.
            */

            var calculator = Substitute.For<ICalculator>();

            var counter2 = 0;
            calculator.Add(1, 2).Returns(3);
            calculator
                .When(x => x.Add(Arg.Any<int>(), Arg.Any<int>()))
                .Do(x => counter++);

            var result = calculator.Add(1, 2);
            Assert.AreEqual(3, result);
            Assert.AreEqual(1, counter2);

            /*
            Per argument callbacks
            In cases where we only need callbacks for a particular argument we may be able to use per argument callbacks like Arg.Do() and Arg.Invoke() instead of When..Do.
            Argument callbacks give us slightly more concise code in a style that is more in keeping with the rest of the NSubstitute API. 
            See Actions with arguments for more information and examples.
             */
        }

        [Test]
        public void TestCallbacks_CallbackBuilder()
        {
            /*
            The Callback builder lets us create more complex Do() secenarios. 
            We can use Callback.First() followed by Then(), ThenThrow() and ThenKeepDoing() to build chains of callbacks. 
            We can also use Always() and AlwaysThrow() to specify callbacks called every time. 
            Note that a callback set by an Always() method will be called even if other callbacks will throw an exception. 
            */

            var sub = Substitute.For<IFoo>();

            var calls = new List<string>();
            var counter = 0;

            sub
              .When(x => x.DoSomething())
              .Do(
                Callback.First(x => calls.Add("1"))
                    .Then(x => calls.Add("2"))
                    .Then(x => calls.Add("3"))
                    .ThenKeepDoing(x => calls.Add("+"))
                    .AndAlways(x => counter++)
              );

            for (int i = 0; i < 5; i++)
            {
                sub.DoSomething();
            }
            Assert.That(String.Concat(calls), Is.EqualTo("123++"));
            Assert.That(counter, Is.EqualTo(5));
        }
    }
}
