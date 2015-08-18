using System;

namespace CS_TDD._004_StubsAndMocks._017_EventTriggeredTest
{
    public interface IView
    {
        event EventHandler Load;

        void LoadEventTrigger(object sender, EventArgs e);
    }
}
