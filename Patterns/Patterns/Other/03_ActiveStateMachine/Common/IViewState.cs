namespace Patterns.Other._03_ActiveStateMachine.Common
{
    public interface IViewState
    {
        string Name { get; }

        bool IsDefaultViewState { get; }
    }
}
