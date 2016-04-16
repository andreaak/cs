using System;

namespace Patterns._02_Structural.Proxy._001_Base
{
    class RealSubject : Subject
    {
        public override void Request()
        {
            Console.WriteLine("RealSubject");
        }
    }
}
