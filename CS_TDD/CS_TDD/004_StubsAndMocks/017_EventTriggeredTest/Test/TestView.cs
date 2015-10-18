using System;

namespace CS_TDD._004_StubsAndMocks._017_EventTriggeredTest.Test
{
    public class TestView : IView
    {
        public event EventHandler Load;

        public void LoadEventTrigger(object sender, EventArgs e)
        {
            Load.Invoke(sender, e);
        }
    }
}
