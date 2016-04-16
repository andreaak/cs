using System.Diagnostics;

namespace Patterns._02_Structural.Adapter._007_TwoWayAdapter.Old_Subsystem
{
    class AdapteeOld : ITargetOld
    {
        public void MethodOld()
        {
            Debug.WriteLine("AdapteeOld.MethodOld");
        }
    }
}
