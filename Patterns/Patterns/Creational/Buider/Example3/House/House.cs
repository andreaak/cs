using System;
using System.Collections;

namespace Creational.Builder.Example3
{
    class House
    {
        ArrayList parts = new ArrayList();

        public void Add(object part)
        {
            parts.Add(part);
        }
    }
}
