using System.Collections.Generic;

namespace Patterns.Other._03_ActiveStateMachine.StateMachine
{
    public class Transition
    {
        public string Name { get; }

        public string SourceStateName { get; }

        public string TargetStateName { get; }

        public List<StateMachineAction> GuardList{ get; }

        public List<StateMachineAction> TransitionActionList{ get; }

        public string Trigger { get; }

        public Transition(string name, string sourceStateName, string targetStateName, List<StateMachineAction> guardList, List<StateMachineAction> transitionActionList, string trigger)
        {
            Name = name;
            SourceStateName = sourceStateName;
            TargetStateName = targetStateName;
            GuardList = guardList;
            TransitionActionList = transitionActionList;
            Trigger = trigger;
        }
    }
}
