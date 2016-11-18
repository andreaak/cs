using Patterns.Other._03_ActiveStateMachine.ApplicationServices;
using System;
using System.Media;

namespace Patterns.Other._03_ActiveStateMachine.TelephoneStateMachine
{
    class DeviceBell : Device
    {
        public bool Ringing { get; set; }

        public DeviceBell(string deviceName, Action<string, string, string> eventCallBack)
            : base(deviceName, eventCallBack)
        { }

        //Initialization
        public override void OnInit()
        {
            Ringing = false;
        }

        public void Rings()
        {
            Ringing = true;
            SystemSounds.Hand.Play();
        }

        public void Silent()
        {
            Ringing = false;
        }
    }
}
