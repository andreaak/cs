namespace Patterns.Behavioral.State.Example1.States
{
    interface IState
    {
        void FillTank();
        void TurnKey();
        void Drive();
        void Stop();
    }
}
