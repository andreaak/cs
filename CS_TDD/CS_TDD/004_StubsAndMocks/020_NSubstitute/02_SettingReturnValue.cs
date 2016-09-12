using System;
using CS_TDD._004_StubsAndMocks._018_Moq.Setup;
using CS_TDD._004_StubsAndMocks._020_NSubstitute.Setup;
using NSubstitute;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._020_NSubstitute
{
    [TestFixture]
    public class _02_SettingReturnValue
    {
        [Test]
        public void TestSettingReturnValue_ForMethod()
        {
            /*
            To set a return value for a method call on a substitute, call the method as normal, 
            then follow it with a call to NSubstitute’s Returns() extension method.
            */
            var calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2).Returns(3);

            /*
            This value will be returned every time this call is made. 
            Returns() will only apply to this combination of arguments, 
            so other calls to this method will return a default value instead.
            */

            //Make a call return 3:
            calculator.Add(1, 2).Returns(3);
            Assert.AreEqual(calculator.Add(1, 2), 3);
            Assert.AreEqual(calculator.Add(1, 2), 3);

            //Call with different arguments does not return 3
            Assert.AreNotEqual(calculator.Add(3, 6), 3);
        }

        [Test]
        public void TestSettingReturnValue_ForProperties()
        {
            var calculator = Substitute.For<ICalculator>();

            /*
            The return value for a property can be set in the same was as for a method, using Returns(). 
            You can also just use plain old property setters for read/write properties; they’ll behave just the way you expect them to.
            */
            calculator.Mode.Returns("DEC");
            Assert.AreEqual(calculator.Mode, "DEC");

            calculator.Mode = "HEX";
            Assert.AreEqual(calculator.Mode, "HEX");
        }

        [Test]
        public void TestSettingReturnValue_ReturnForSpecificArgs()
        {
            var calculator = Substitute.For<ICalculator>();

            /*
            Return values can be configured for different combinations of arguments passed to calls using argument matchers.
            */

            //Return when first arg is anything and second arg is 5:
            calculator.Add(Arg.Any<int>(), 5).Returns(10);
            Assert.AreEqual(10, calculator.Add(123, 5));
            Assert.AreEqual(10, calculator.Add(-9, 5));
            Assert.AreNotEqual(10, calculator.Add(-9, -9));

            //Return when first arg is 1 and second arg less than 0:
            calculator.Add(1, Arg.Is<int>(x => x < 0)).Returns(345);
            Assert.AreEqual(345, calculator.Add(1, -2));
            Assert.AreNotEqual(345, calculator.Add(1, 2));

            //Return when both args equal to 0:
            calculator.Add(Arg.Is(0), Arg.Is(0)).Returns(99);
            Assert.AreEqual(99, calculator.Add(0, 0));
        }

        [Test]
        public void TestSettingReturnValue_ReturnForAnyArgs()
        {
            var calculator = Substitute.For<ICalculator>();

            /*
            A call can be configured to return a value regardless of the arguments passed using the ReturnsForAnyArgs() extension method
            The same behaviour can also be achieved using argument matchers: 
            it is simply a shortcut for replacing each argument with Arg.Any<T>().
            ReturnsForAnyArgs() has the same overloads as Returns(), 
            so you can also specify multiple return values or calculated return values using this approach.
            */

            calculator.Add(1, 2).ReturnsForAnyArgs(100);
            Assert.AreEqual(calculator.Add(1, 2), 100);
            Assert.AreEqual(calculator.Add(-7, 15), 100);
        }

        [Test]
        public void TestSettingReturnValue_ReturnFromFunction()
        {
            var calculator = Substitute.For<ICalculator>();

            /*
            The return value for a call to a property or method can be set to the result of a function. 
            This allows more complex logic to be put into the substitute. 
            Although this is normally a bad practice, there are some situations in which it is useful.
            */
            /*
            In this example argument matchers are used to match all calls to Add(), and a lambda function is used to return the sum of the first and second arguments passed to the call. 
            */
            calculator.Add(Arg.Any<int>(), Arg.Any<int>())
                      .Returns(x => (int)x[0] + (int)x[1]);

            Assert.That(calculator.Add(1, 1), Is.EqualTo(2));
            Assert.That(calculator.Add(20, 30), Is.EqualTo(50));
            Assert.That(calculator.Add(-73, 9348), Is.EqualTo(9275));


            /*
            The function we provide to Returns() and ReturnsForAnyArgs() is of type Func<CallInfo,T>, where T is the type the call is returning, 
            and CallInfo is a type which provides access to the arguments used for the call. 
            In the previous example we accessed these arguments using an indexer (x[1] for the second argument). 
            CallInfo also has a couple of convenience methods to pick arguments in a strongly typed way:
                T Arg<T>(): Gets the argument of type T passed to this call.
                T ArgAt<T>(int position): Gets the argument passed to this call at the specified zero-based position, converted to type T. 
             */

            var foo = Substitute.For<IFoo>();
            foo.DoSomethingWithReturnString(0, "").ReturnsForAnyArgs(x => "Hello " + x.Arg<string>());

            Assert.That(foo.DoSomethingWithReturnString(1, "World"), Is.EqualTo("Hello World"));

            /*
            Here x.Arg<string>() will return the string argument passed to the call, rather than having to use (string) x[1]. 
            If there are two string arguments to a call, NSubstitute will throw an exception and let you know that it can’t work out which argument you mean. 
             */
        }

        [Test]
        public void TestSettingReturnValue_MultipleReturnValues()
        {
            var calculator = Substitute.For<ICalculator>();

            /*
            A call can also be configured to return a different value over multiple calls. 
            The following example shows this for a call to a property, but it works the same way for method calls.
            */

            calculator.Mode.Returns("DEC", "HEX", "BIN");
            Assert.AreEqual("DEC", calculator.Mode);
            Assert.AreEqual("HEX", calculator.Mode);
            Assert.AreEqual("BIN", calculator.Mode);

            //This can also be achieved by returning from a function, but passing multiple values to Returns() is simpler and reads better.

            /*
            Returns() also supports passing multiple functions to return from, 
            which allows one call in a sequence to throw an exception or perform some other action. 
             */

            calculator = Substitute.For<ICalculator>();
            calculator.Mode.Returns(x => "DEC", x => "HEX", x => { throw new Exception(); });
            Assert.AreEqual("DEC", calculator.Mode);
            Assert.AreEqual("HEX", calculator.Mode);
            Assert.Throws<Exception>(() => { var result = calculator.Mode; });
        }

        [Test]
        public void TestSettingReturnValue_ReplacingReturnValues()
        {
            var calculator = Substitute.For<ICalculator>();

            /*
            The return value for a method or property can be set as many times as required. Only the most recently set value will be returned.
            */

            calculator.Mode.Returns("DEC,HEX,OCT");
            calculator.Mode.Returns(x => "???");
            calculator.Mode.Returns("HEX");
            calculator.Mode.Returns("BIN");
            Assert.AreEqual(calculator.Mode, "BIN");
        }

        [Test]
        public void TestSettingReturnValue_SettingOutAndRefArgs()
        {
            //Out and ref arguments can be set using a Returns() callback, or using When..Do.
            //For the interface we can configure the return value and set the output of the second argument like this:

            //Arrange
            var value = "";
            var lookup = Substitute.For<IFoo>();
            lookup
                .TryParse("hello", out value)
                .Returns(x => {
                    x[1] = "world!";
                    return true;
                });

            //Act
            var result = lookup.TryParse("hello", out value);

            //Assert
            Assert.True(result);
            Assert.AreEqual(value, "world!");
        }
    }
}
