using System;

namespace Patterns.Other._03_ActiveStateMachine.StateMachine
{
    public class StateMachineAction
    {
        public string Name { get; private set; }

        private Action _method;

        public StateMachineAction(string name, Action method)
        {
            Name = name;
            _method = method;
        }

        public void Execute()
        {
            _method();
        }
    }
}
