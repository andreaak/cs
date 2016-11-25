using System.Collections.Generic;

namespace Patterns._04_Other._03_ActiveStateMachine.Common
{
    public interface IViewStateConfiguration
    {
        Dictionary<string, object> ViewStates { get; set; }

        string[] ViewStateList { get; set; }

        string DefaultViewState { get; }
    }
}
