using System.Collections.Generic;
using Patterns.Other._03_ActiveStateMachine.ApplicationServices;
using Patterns.Other._03_ActiveStateMachine.StateMachine;

namespace Patterns.Other._03_ActiveStateMachine.TelephoneStateMachine
{
    public class TelephoneStateMachineConfiguration
    {
        //List of states
        public Dictionary<string, State> TelephoneStateMachineStateList { get; set; }

        //List of activities in the system
        public TelephoneActivities TelephoneActivities { get; set; }

        //Max number of entries in trigger queue
        public int MaxEntries = 50;

        public EventManager TelephoneEventManager;

        public ViewManager TelephoneViewManager;

        public DeviceManager TelephoneDeviceManager;

        public LogManager TelephoneLogManager;

        public TelephoneStateMachineConfiguration()
        {
            BuildConfig();
        }

        private void BuildConfig()
        {
            //Transitions and actions
            #region TransitionsAndActions

            //Create object holding implementation of all system actions
            TelephoneActivities = new TelephoneActivities();

            //Create actions and map action methods into the corresponding action object
            //Device actions
            var actionBellRings = new StateMachineAction("ActionBellRings", TelephoneActivities.ActionBellRings);
            var actionBellSilent = new StateMachineAction("ActionBellSilent", TelephoneActivities.ActionBellSilent);
            var actionLineOff = new StateMachineAction("ActionLineOff", TelephoneActivities.ActionLineOff);
            var actionLineActive = new StateMachineAction("ActionLineActive", TelephoneActivities.ActionLineActive);

            //View actions
            var actionViewPhoneRings = new StateMachineAction("ActionViewPhoneRings", TelephoneActivities.ActionViewPhoneRings);
            var actionViewPhoneIdle = new StateMachineAction("ActionViewPhoneIdle", TelephoneActivities.ActionViewPhoneIdle);
            var actionViewTalking = new StateMachineAction("ActionViewTalking", TelephoneActivities.ActionViewTalking);
            var actionViewErrorPhoneRings = new StateMachineAction("ActionViewErrorPhoneRings", TelephoneActivities.ActionErrorPhoneRings);

            //Create transitions and corresponding triggers, states need to be added
            var emptyList = new List<StateMachineAction>();//to avoid null ref exception, use empty list where no actions are used

            //Transition IncomingCall
            var ICActions = new List<StateMachineAction>();
            ICActions.Add(actionViewPhoneRings);
            var transitionIncomingCall = new Transition("TransitionIncomingCall", "StatePhoneIdle", "StatePhoneRings", emptyList, ICActions, "OnLineExternalActive");

            //Transition ErrorPhoneRings
            var EPRActions = new List<StateMachineAction>();
            EPRActions.Add(actionViewErrorPhoneRings);
            var transitionErrorPhoneRings = new Transition("TransitionErrorPhoneRings", "StatePhoneRings", "StatePhoneRings", emptyList, EPRActions, "OnBellBroken");

            //Transition CallBlocked
            var CBActions = new List<StateMachineAction>();
            CBActions.Add(actionViewPhoneIdle);
            var transitionCallBlocked = new Transition("TransitionCallBlocked", "StatePhoneRings", "StatePhoneIdle", emptyList, CBActions, "OnReceiverDown");

            //Transition CallAccepted
            var CAActions = new List<StateMachineAction>();
            CAActions.Add(actionViewTalking);
            var transitionCallAccepted = new Transition("TransitionCallAccepted", "StatePhoneRings", "StateTalking", emptyList, CAActions, "OnReceiverUp");

            //Transition CallEnded
            var CEActions = new List<StateMachineAction>();
            CEActions.Add(actionViewPhoneIdle);
            var transitionCallEnded = new Transition("TransitionCallEnded", "StateTalking", "StatePhoneIdle", emptyList, CEActions, "OnReceiverDown");

            #endregion

            #region States

            //State : PhoneIdle
            var transitionsPhoneIdle = new Dictionary<string, Transition>();
            transitionsPhoneIdle.Add("TransitionIncomingCall", transitionIncomingCall);
            var entryActionsPhoneIdle = new List<StateMachineAction>();
            var exitActionsPhoneIdle = new List<StateMachineAction>();
            var phoneIdle = new State("StatePhoneIdle", transitionsPhoneIdle, entryActionsPhoneIdle, exitActionsPhoneIdle, true);

            //State : PhoneRings
            var transitionsPhoneRings = new Dictionary<string, Transition>();
            transitionsPhoneRings.Add("TransitionCallBlocked", transitionCallBlocked);
            transitionsPhoneRings.Add("TransitionCallAccepted", transitionCallAccepted);
            transitionsPhoneRings.Add("TransitionErrorPhoneRings", transitionErrorPhoneRings);
            var entryActionsPhoneRings = new List<StateMachineAction>();
            entryActionsPhoneRings.Add(actionBellRings);
            var exitActionsPhoneRings = new List<StateMachineAction>();
            exitActionsPhoneRings.Add(actionBellSilent);
            var phoneRings = new State("StatePhoneRings", transitionsPhoneRings, entryActionsPhoneRings, exitActionsPhoneRings);


            //State : Talking
            var transitionsTalking = new Dictionary<string, Transition>();
            transitionsTalking.Add("TransitionCallEnded", transitionCallEnded);
            var entryActionsTalking = new List<StateMachineAction>();
            entryActionsTalking.Add(actionLineActive);
            var exitActionsTalking = new List<StateMachineAction>();
            exitActionsTalking.Add(actionLineOff);
            //exitActionsTalking.AddFirst(lineDown);
            var talking = new State("StateTalking", transitionsTalking, entryActionsTalking, exitActionsTalking);


            TelephoneStateMachineStateList = new Dictionary<string, State>
            {
                { "StatePhoneIdle", phoneIdle},
                { "StatePhoneRings", phoneRings},
                { "StateTalking", talking},
            };

            #endregion

            //Application services
            TelephoneEventManager = EventManager.Instance;
            TelephoneViewManager = ViewManager.Instance;
            TelephoneDeviceManager = DeviceManager.Instance;
            TelephoneLogManager = LogManager.Instance;
        }

        public void DoEventMappings(TelephoneStateMachine telephoneStateMachine, TelephoneActivities telephoneActivities)
        {
            //Register all events
            #region Register events

            //Events implemented for use case
            TelephoneEventManager.RegisterEvent("TelephoneUiEvent", telephoneActivities);
            TelephoneEventManager.RegisterEvent("TelephoneDeviceEvent", telephoneActivities);

            //Framework / infrastructure events
            TelephoneEventManager.RegisterEvent("StateMachineEvent", telephoneStateMachine);
            //TelephoneEventManager.RegisterEvent("UINotification", TelephoneViewManager);
            TelephoneEventManager.RegisterEvent("DeviceManagerNotification", TelephoneDeviceManager);
            TelephoneEventManager.RegisterEvent("EventManagerEvent", TelephoneEventManager);
            TelephoneEventManager.RegisterEvent("ViewManagerEvent", TelephoneViewManager);
            TelephoneEventManager.RegisterEvent("DeviceManagerEvent", TelephoneDeviceManager);

            #endregion

            //Subscribe handlers to events registered with event manager

            #region Event Mappings

            //Logging
            //TelephoneEventManager.SubscribeEvent("UINotification", "LogEventHandler", TelephoneLogManager);
            TelephoneEventManager.SubscribeEvent("DeviceManagerNotification", "LogEventHandler", TelephoneLogManager);
            TelephoneEventManager.SubscribeEvent("StateMachineEvent", "LogEventHandler", TelephoneLogManager);
            TelephoneEventManager.SubscribeEvent("EventManagerEvent", "LogEventHandler", TelephoneLogManager);
            TelephoneEventManager.SubscribeEvent("ViewManagerEvent", "LogEventHandler", TelephoneLogManager);
            TelephoneEventManager.SubscribeEvent("DeviceManagerEvent", "LogEventHandler", TelephoneLogManager);

            //Notifications / Triggers
            //TelephoneEventManager.SubscribeEvent("UINotification", "InternalNotificationHandler", telephoneStateMachine);
            TelephoneEventManager.SubscribeEvent("ViewManagerEvent", "InternalNotificationHandler", telephoneStateMachine);
            TelephoneEventManager.SubscribeEvent("DeviceManagerNotification", "InternalNotificationHandler", telephoneStateMachine);

            //System event listeners in managers
            TelephoneEventManager.SubscribeEvent("TelephoneUiEvent", "ViewCommandHandler", TelephoneViewManager);
            TelephoneEventManager.SubscribeEvent("TelephoneDeviceEvent", "DeviceCommandHandler", TelephoneDeviceManager);
            TelephoneEventManager.SubscribeEvent("StateMachineEvent", "SystemEventHandler", TelephoneViewManager);
            TelephoneEventManager.SubscribeEvent("StateMachineEvent", "SystemEventHandler", TelephoneDeviceManager);

            //Sends UI button clicls to device manager
            TelephoneEventManager.SubscribeEvent("ViewManagerEvent", "DeviceCommandHandler", TelephoneDeviceManager);

            #endregion
        }
    }
}
