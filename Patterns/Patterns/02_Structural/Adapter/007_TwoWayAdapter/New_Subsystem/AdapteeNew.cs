using System.Diagnostics;

namespace Patterns.Structural.Adapter._007_TwoWayAdapter
{
    class AdapteeNew : ITargetNew
    {
        public void MethodNew()
        {
            Debug.WriteLine("AdapteeNew.MethodNew");
        }
    }
}
