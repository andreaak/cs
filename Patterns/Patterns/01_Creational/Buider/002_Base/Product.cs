using System.Collections;
using System.Diagnostics;

namespace Patterns._01_Creational.Buider._002_Base
{
    class Product
    {
        ArrayList parts = new ArrayList();

        public void Add(string part)
        {
            parts.Add(part);
        }

        public void Show()
        {
            foreach (string part in parts)
                Debug.WriteLine(part);
        }
    }
}
