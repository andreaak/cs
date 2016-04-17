using System.Diagnostics;

namespace Patterns._03_Behavioral.Command._002_Base
{
    class Receiver
    {
        public void Action()
        {
            Debug.WriteLine("Receiver");
        }
    }
}
