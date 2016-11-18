using System.Collections.Generic;

namespace Patterns.Other._03_ActiveStateMachine.StateMachine
{
    public class State
    {
        public string StateName { get; }

        public Dictionary<string, Transition> StateTransitionList { get; }

        public List<StateMachineAction> EntryActions { get; }

        public List<StateMachineAction> ExitActions { get; }

        public bool IsDefaultState { get; }

        public State(string stateName, Dictionary<string, Transition> stateTransitionList, List<StateMachineAction> entryActions, List<StateMachineAction> exitActions, bool isDefaultState = true)
        {
            StateName = stateName;
            StateTransitionList = stateTransitionList;
            EntryActions = entryActions;
            ExitActions = exitActions;
            IsDefaultState = isDefaultState;
        }
    }
}
