using Patterns.Other._03_ActiveStateMachine.ApplicationServices;
using System.Collections.Generic;
using Patterns.Other._03_ActiveStateMachine.Common;

namespace Patterns.Other._03_ActiveStateMachine.TelephoneStateMachine
{
    public class TelephoneDeviceConfiguration : IDeviceConfiguration
    {
        public Dictionary<string, object> Devices { get; set; }

        private DeviceManager _devMan;

        public TelephoneDeviceConfiguration()
        {
            _devMan = DeviceManager.Instance;
            InitDevices();
        }

        private void InitDevices()
        {
            var bell = new DeviceBell("Bell", _devMan.RaiseDeviceManagerNotification);
            var phoneLine = new DevicePhoneLine("PhoneLine", _devMan.RaiseDeviceManagerNotification);
            var receiver = new DeviceReceiver("Receiver", _devMan.RaiseDeviceManagerNotification);

            Devices = new Dictionary<string, object> { { "Bell", bell }, { "PhoneLine", phoneLine }, { "Receiver", receiver } };
        }
    }
}
