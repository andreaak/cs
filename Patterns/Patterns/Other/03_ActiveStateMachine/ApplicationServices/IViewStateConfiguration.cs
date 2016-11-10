using System.Collections.Generic;

namespace Patterns.Other._03_ActiveStateMachine.ApplicationServices
{
    public interface IViewStateConfiguration
    {
        Dictionary<string, object> ViewStates { get; set; }

        string[] ViewStateList { get; set; }

        string DefaultViewState { get; set; }
    }
}
