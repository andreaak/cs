using CS_TDD._004_StubsAndMocks._017_EventTriggeredTest.Dll_;
using NUnit.Framework;

namespace CS_TDD._004_StubsAndMocks._017_EventTriggeredTest.Test
{
    [TestFixture]
    class EventRelatedTests
    {
        [Test]
        public static void EventTriggeredTest1()
        {
            bool triggerFlag = false;

            TestView view = new TestView();

            view.Load += delegate { triggerFlag = true; };

            view.LoadEventTrigger(null, null);

            Assert.IsTrue(triggerFlag);
        }

        [Test]
        public static void EventTriggeredTest2()
        {
            EventsVerifier eventsVerifier = new EventsVerifier();

            TestView view = new TestView();

            eventsVerifier.Expect(view, "Load", null, null);

            view.LoadEventTrigger(null, null);

            eventsVerifier.Verify();
        }
    }
}
