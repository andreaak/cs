using System.Collections.Generic;

namespace Patterns.Other._03_ActiveStateMachine.StateMachine
{
    public class State
    {
        public string StateName { get; private set; }

        public Dictionary<string, Transition> StateTransitionList { get; private set; }

        public List<StateMachineAction> EntryActions { get; private set; }

        public List<StateMachineAction> ExitActions { get; private set; }

        public bool IsDefaultState { get; private set; }

        public State(string stateName, Dictionary<string, Transition> stateTransitionList, List<StateMachineAction> entryActions, List<StateMachineAction> exitActions, bool isDefaultState = false)
        {
            StateName = stateName;
            StateTransitionList = stateTransitionList;
            EntryActions = entryActions;
            ExitActions = exitActions;
            IsDefaultState = isDefaultState;
        }
    }
}
