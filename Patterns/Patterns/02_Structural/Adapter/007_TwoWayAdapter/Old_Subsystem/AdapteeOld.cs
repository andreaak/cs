using System.Diagnostics;

namespace Patterns.Structural.Adapter._007_TwoWayAdapter
{
    class AdapteeOld : ITargetOld
    {
        public void MethodOld()
        {
            Debug.WriteLine("AdapteeOld.MethodOld");
        }
    }
}
