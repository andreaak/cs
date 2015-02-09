using System;

namespace Patterns.Structural.Adapter.Example7
{
    class AdapteeOld : ITargetOld
    {
        public void MethodOld()
        {
            Console.WriteLine("AdapteeOld.MethodOld");
        }
    }
}
