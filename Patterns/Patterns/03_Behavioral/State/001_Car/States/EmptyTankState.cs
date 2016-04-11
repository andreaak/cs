using System;
using System.Diagnostics;

namespace Patterns.Behavioral.State._001_Car.States
{
    class EmptyTankState : IState
    {
        private readonly Car _car;

        public EmptyTankState(Car car)
        {
            _car = car;
        }

        public void FillTank()
        {
            Debug.WriteLine("Теперь бак полный");
            _car.SetState(_car.FullTank);
        }

        public void TurnKey()
        {
            Debug.WriteLine("Без бензина не работаю");
        }

        public void Drive()
        {
            Debug.WriteLine("И как мы поедем без бензина? Никак!");
        }

        public void Stop()
        {
            Debug.WriteLine("Нет бензина - значит и так стоим");
        }
    }
}
