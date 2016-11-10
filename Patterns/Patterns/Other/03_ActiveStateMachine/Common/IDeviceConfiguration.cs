using System.Collections.Generic;

namespace Patterns.Other._03_ActiveStateMachine.ApplicationServices
{
    public interface IDeviceConfiguration
    {
        Dictionary<string, object> Devices { get; set; }
    }
}
