using System;
using System.Diagnostics;

namespace Patterns.Behavioral.State._001_Car.States
{
    class FullTankState : IState
    {
        private readonly Car _car;

        public FullTankState(Car car)
        {
            _car = car;
        }

        public void FillTank()
        {
            Debug.WriteLine("В меня столько не влезет");
        }

        public void TurnKey()
        {
            Debug.WriteLine("Дрын дын дын дын дын тррррррррр");
            _car.SetState(_car.EngineStarted);
        }

        public void Drive()
        {
            Debug.WriteLine("Сначала заведи меня");
        }

        public void Stop()
        {
            Debug.WriteLine("Я и так стою");
        }
    }
}
