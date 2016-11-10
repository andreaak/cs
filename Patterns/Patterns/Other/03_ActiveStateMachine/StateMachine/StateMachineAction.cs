using System;

namespace Patterns.Other._03_ActiveStateMachine
{
    class StateMachineAction
    {
        private Action _method;

        public string Name { get; }

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
