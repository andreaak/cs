using System;
using System.Collections;

namespace Creational.Builder._004_House
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
