using Patterns.Other._03_ActiveStateMachine.Common;

namespace UIApplication._01_StateMachine
{
    public class TelephoneViewState : IViewState
    {
        public string Name { get; private set; }

        public bool Bell { get; private set; }

        public bool Line { get; private set; }

        public bool ReceiverHungUp { get; private set; }

        public bool IsDefaultViewState { get; private set; }

        public TelephoneViewState(string name, bool bell, bool line, bool receiverHungUp, bool isDefaultViewState = false)
        {
            Name = name;
            Bell = bell;
            Line = line;
            ReceiverHungUp = receiverHungUp;
            IsDefaultViewState = isDefaultViewState;
        }
    }
}
