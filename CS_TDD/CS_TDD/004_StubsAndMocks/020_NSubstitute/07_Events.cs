using System;
using System.ComponentModel;
using CS_TDD._004_StubsAndMocks._020_NSubstitute.Setup;
using NSubstitute;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._020_NSubstitute
{
    [TestFixture]
    public class _07_Events
    {
        [Test]
        public void Events_CheckingEventSubscriptions()
        {
            /*
            As with properties, we’d normally favour testing the required behaviour over checking subscriptions to particular event handlers. 
            We can do that by raising an event on the substitute and asserting our class performs the correct behaviour in response: 
            */

            var command = Substitute.For<ICommand>();
            var watcher = new CommandWatcher(command);

            command.Executed += Raise.Event();

            Assert.That(watcher.DidStuff);

            //If required though, Received will let us assert that the subscription was received

            command = Substitute.For<ICommand>();
            watcher = new CommandWatcher(command);

            // Not recommended. Favour testing behaviour over implementation specifics.
            // Can check subscription:
            command.Received().Executed += watcher.OnExecuted;
            // Or, if the handler is not accessible:
            command.Received().Executed += Arg.Any<EventHandler>();
        }


        [Test]
        public void Events_RaisingEvents()
        {
            /*
            Sometimes it is necessary to raise events
            Events are “interesting” creatures in the.NET world, as you can’t pass around references to them like you can with other members. 
            Instead, you can only add or remove handlers to events, and it is this add handler syntax that NSubstitute uses to raise events.
            */
            var engine = Substitute.For<IEngine>();

            var wasCalled = false;
            engine.Idling += (sender, args) => wasCalled = true;

            //Tell the substitute to raise the event with a sender and EventArgs:
            engine.Idling += Raise.EventWith(new object(), new EventArgs());

            Assert.True(wasCalled);

            /*
            In the example above we don’t really mind what sender and EventArgs our event is raised with, just that it was called. 
            In this case NSubstitute can make our life easier by creating the required arguments for our event handler: 
             */
            wasCalled = false;
            engine.Idling += Raise.Event();
            Assert.True(wasCalled);
        }

        [Test]
        public void Events_RaisingEventsWhenArgumentsDonotHaveDefaultConstructor()
        {
            /*
            NSubstitute will not always be able to create the event arguments for you. 
            If your event args do not have a default constructor you will have to provide them yourself using Raise.EventWith<TEventArgs>(...), 
            as is the case for the LowFuelWarning event. NSubstitute will still create the sender for you if you don’t provide it though.
            */
            var engine = Substitute.For<IEngine>();

            int numberOfEvents = 0;

            engine.LowFuelWarning += (sender, args) => numberOfEvents++;

            //Raise event with specific args, any sender:
            engine.LowFuelWarning += Raise.EventWith(new LowFuelWarningEventArgs(10));
            //Raise event with specific args and sender:
            engine.LowFuelWarning += Raise.EventWith(new object(), new LowFuelWarningEventArgs(10));

            Assert.AreEqual(2, numberOfEvents);
        }

        [Test]
        public void Events_RaisingDelegateEvents()
        {
            /*
            Sometimes events are declared with a delegate that does not inherit from EventHandler<T> or EventHandler. 
            These events can be raised using Raise.Event<TypeOfEventHandlerDelegate>(arguments). 
            NSubsitute will try and guess the arguments required for the delegate, but if it can’t it will tell you what arguments you need to supply.
            The following examples shows raising an INotifyPropertyChanged event, which uses a PropertyChangedEventHandler delegate and requires two parameters.
            */
            var sub = Substitute.For<INotifyPropertyChanged>();
            bool wasCalled = false;
            sub.PropertyChanged += (sender, args) => wasCalled = true;

            sub.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(this, new PropertyChangedEventArgs("test"));

            Assert.That(wasCalled);

            /*
            In the IEngine example the RevvedAt event is declared as an Action<int>. 
            This is another example of a delegate event, and we can use Raise.Event<Action<int>>() to raise our event.
            */

            var engine = Substitute.For<IEngine>();

            int revvedAt = 0; ;
            engine.RevvedAt += rpm => revvedAt = rpm;

            engine.RevvedAt += Raise.Event<Action<int>>(123);

            Assert.AreEqual(123, revvedAt);
        }
    }
}
