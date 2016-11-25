using System.Collections.Generic;

namespace Patterns._04_Other._03_ActiveStateMachine.Common
{
    public interface IDeviceConfiguration
    {
        Dictionary<string, object> Devices { get; set; }
    }
}
