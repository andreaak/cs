namespace Patterns._03_Behavioral.State._001_Car.States
{
    interface IState
    {
        void FillTank();
        void TurnKey();
        void Drive();
        void Stop();
    }
}
