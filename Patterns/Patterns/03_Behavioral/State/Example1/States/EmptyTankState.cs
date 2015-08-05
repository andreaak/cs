using System;
using System.Diagnostics;

namespace Patterns.Behavioral.State.Example1.States
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
            Console.WriteLine("Теперь бак полный");
            _car.SetState(_car.FullTank);
        }

        public void TurnKey()
        {
            Console.WriteLine("Без бензина не работаю");
        }

        public void Drive()
        {
            Console.WriteLine("И как мы поедем без бензина? Никак!");
        }

        public void Stop()
        {
            Console.WriteLine("Нет бензина - значит и так стоим");
        }
    }
}
