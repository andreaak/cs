namespace Patterns._02_Structural.Facade._001_WashingMachine.WashingMachine
{
    class WashingMachine
    {
        private Dryer _dryer;
        private Engine _engine;
        private Thermo _thermo;
        private WaterManagingSubsystem _water;

        public WashingMachine(Dryer dryer, Engine engine, Thermo thermo, WaterManagingSubsystem water)
        {
            _dryer = dryer;
            _engine = engine;
            _thermo = thermo;
            _water = water;
        }

        public int GetTemperature()
        {
            return _thermo.GetTemperature();
        }

        public void WashCotton()
        {
            _water.FillWater(10);
            _thermo.WarmUp(70);
            _engine.Rotate();
            _engine.Rotate();
            _engine.Rotate();
            _engine.Stop();
            _water.EmptyWater();
            _dryer.Dry(30, 1000);
            _water.FillWater(15);
            _thermo.WarmUp(50);
            _engine.Rotate();
            _engine.Rotate();
            _engine.Rotate();
            _engine.Stop();
            _water.EmptyWater();
            _dryer.Dry(60, 1500);
        }

        public void WashWool()
        {
            _water.FillWater(7);
            _thermo.WarmUp(30);
            _engine.Rotate();
            _engine.Stop();
            _water.EmptyWater();
            _dryer.Dry(20, 500);
            _water.FillWater(10);
            _thermo.WarmUp(20);
            _engine.Rotate();
            _engine.Stop();
            _water.EmptyWater();
            _dryer.Dry(60, 700);
        }
    }
}
