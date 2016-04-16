using System.Collections;
using System.Collections.Specialized;

namespace Patterns._02_Structural.Bridge._003_Collections.Abstraction
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
