using System;
using CS_TDD._004_StubsAndMocks._020_NSubstitute.Setup;
using NSubstitute;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._020_NSubstitute
{
    [TestFixture]
    public class _09_CallOrder
    {
        [Test]
        public void TestCommandRunWhileConnectionIsOpen()
        {
            /*
            Sometimes calls need to be made in a specific order. 
            Depending on the timing of calls like this is known as temporal coupling. 
            Ideally we’d change our design to remove this coupling, 
            but for times when we can’t NSubstitute lets us resort to asserting the order of calls.
            */

            var connection = Substitute.For<IConnection>();
            var command = Substitute.For<ICommand>();
            var subject = new Controller(connection, command);

            subject.DoStuff();

            Received.InOrder(() => {
                connection.Open();
                command.Execute(connection);
                connection.Close();
            });

            /*
            If the calls were received in a different order then Received.InOrder will throw an exception and show the expected and actual calls.
            We can use standard argument matchers to match calls, just as we would when checking for a single received call.
            */

            connection = Substitute.For<IConnection>();
            connection.SomethingHappened += () => { /* some event handler */ };
            connection.Open();

            Received.InOrder(() => {
                connection.SomethingHappened += Arg.Any<Action>();
                connection.Open();
            });
        }
    }
}
