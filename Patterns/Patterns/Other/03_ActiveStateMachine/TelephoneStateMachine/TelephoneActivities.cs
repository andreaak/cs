using System;
using System.Media;

namespace Patterns.Other._03_ActiveStateMachine.TelephoneStateMachine
{
    class TelephoneActivities
    {
        //events to communicate from state machine to managers - wiring will be done via event manager
        public event EventHandler<StateMachineEventArgs> TelephoneUiEvent;
        public event EventHandler<StateMachineEventArgs> TelephoneDeviceEvent;

        //Device actons
        public void ActionBellRings()
        {
            RaiseDeviceEvent("Bell", "Rings");
        }

        public void ActionBellSilent()
        {
            RaiseDeviceEvent("Bell", "Silent");
        }

        public void ActionLineActive()
        {
            RaiseDeviceEvent("PhoneLine", "ActiveInternal");
        }

        public void ActionLineOff()
        {
            RaiseDeviceEvent("PhoneLine", "OffInternal");
        }
        //Device actons

        //View actons
        public void ActionViewPhoneRings()
        {
            RaiseTelephoneUiEvent("ViewPhoneRings");
        }

        public void ActionViewPhoneIdle()
        {
            RaiseTelephoneUiEvent("ViewPhoneIdle");
            SystemSounds.Beep.Play();
        }

        public void ActionViewTalking()
        {
            RaiseTelephoneUiEvent("ViewTalking");
        }
        //View actons

        public void RaiseTelephoneUiEvent(string command)
        {
            var arg = new StateMachineEventArgs(command, "UI command", StateMachineEventType.Command, "State machine action", "View Manager");
            if (TelephoneDeviceEvent != null)
                TelephoneDeviceEvent(this, arg);
        }

        public void RaiseDeviceEvent(string target, string command)
        {
            var arg = new StateMachineEventArgs(command, "Device command", StateMachineEventType.Command, "State machine action", target);
            if (TelephoneDeviceEvent != null)
                TelephoneDeviceEvent(this, arg);
        }
    }
}
