using Patterns._04_Other._03_ActiveStateMachine.StateMachine;

namespace Patterns._04_Other._03_ActiveStateMachine.TelephoneStateMachine
{
    public class TelephoneStateMachine : ActiveStateMachine
    {
        private TelephoneStateMachineConfiguration _config;

        //Load config and init event manager
        public TelephoneStateMachine(TelephoneStateMachineConfiguration config) 
            : base(config.TelephoneStateMachineStateList, config.MaxEntries)
        {
            _config = config;

            //Configure event manager routing info.
            //Events are registered and mapped to handlers
            _config.DoEventMappings(this, _config.TelephoneActivities);
        }
    }
}
