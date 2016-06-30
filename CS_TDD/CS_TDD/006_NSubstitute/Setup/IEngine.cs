using System;

namespace CS_TDD._006_NSubstitute.Setup
{
    public interface IEngine
    {
        event EventHandler Idling;
        event EventHandler<LowFuelWarningEventArgs> LowFuelWarning;
        event Action<int> RevvedAt;
    }

    public class LowFuelWarningEventArgs : EventArgs
    {
        public int PercentLeft { get; private set; }

        public LowFuelWarningEventArgs(int percentLeft)
        {
            PercentLeft = percentLeft;
        }
    }
}
