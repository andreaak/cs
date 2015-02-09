using System;

namespace Patterns.Structural.Adapter.Example7
{
    class AdapteeNew : ITargetNew
    {
        public void MethodNew()
        {
            Console.WriteLine("AdapteeNew.MethodNew");
        }
    }
}
