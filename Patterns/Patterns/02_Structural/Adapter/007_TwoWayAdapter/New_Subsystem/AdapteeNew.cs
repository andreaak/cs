using System.Diagnostics;

namespace Patterns._02_Structural.Adapter._007_TwoWayAdapter.New_Subsystem
{
    class AdapteeNew : ITargetNew
    {
        public void MethodNew()
        {
            Debug.WriteLine("AdapteeNew.MethodNew");
        }
    }
}
