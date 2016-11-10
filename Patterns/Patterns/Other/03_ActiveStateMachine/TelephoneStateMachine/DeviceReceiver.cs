using Patterns.Other._03_ActiveStateMachine.ApplicationServices;
using System;

namespace Patterns.Other._03_ActiveStateMachine.TelephoneStateMachine
{
    class DeviceReceiver : Device
    {
        public bool ReceiverLifted { get; set; }

        public DeviceReceiver(string deviceName, Action<string, string, string> eventCallBack)
            : base(deviceName, eventCallBack)
        { }

        public override void OnInit()
        {
            ReceiverLifted = false;
        }

        public void OnReceiverUp()
        {
            ReceiverLifted = true;
            DoNotificationCallback("OnReceiverUp", "Receiver lifted", "Receiver");
        }

        public void OnReceiverDown()
        {
            ReceiverLifted = false;
            DoNotificationCallback("OnReceiverDown", "Receiver down", "Receiver");
        }
    }
}
