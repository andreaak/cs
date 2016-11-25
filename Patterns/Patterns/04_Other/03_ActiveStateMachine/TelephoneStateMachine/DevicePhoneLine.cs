using System;
using System.Media;
using Patterns._04_Other._03_ActiveStateMachine.ApplicationServices;

namespace Patterns._04_Other._03_ActiveStateMachine.TelephoneStateMachine
{
    class DevicePhoneLine : Device
    {
        public bool LineActiveExternal { get; set; }

        public bool LineActiveInternal { get; set; }

        public DevicePhoneLine(string deviceName, Action<string, string, string> eventCallBack)
            : base(deviceName, eventCallBack)
        { }

        public override void OnInit()
        {
            LineActiveExternal = false;
            LineActiveInternal = false;
        }

        //Simulation incoming part of line
        // - active control cannot be controlled by state machine
        public void ActiveExternal()
        {
            LineActiveExternal = true;
            DoNotificationCallback("OnLineExternalActive", "Phone line set to active", DevName);
        }

        //Internal line - controlled by state machine
        public void ActiveInternal()
        {
            LineActiveInternal = true;
        }

        public void OffInternal()
        {
            LineActiveInternal = false;
            SystemSounds.Hand.Play();
        }
    }
}
