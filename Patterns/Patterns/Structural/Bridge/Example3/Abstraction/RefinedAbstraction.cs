using System;
using System.Collections;
using System.Collections.Specialized;

namespace Patterns.Structural.Bridge.Example3
{
    class RefinedAbstraction : Abstraction
    {
        public RefinedAbstraction(int size)        
        {
            if (size > 10)
                this.Implementor = new Hashtable();
            else
                this.Implementor = new ListDictionary();
        }
    }
}
