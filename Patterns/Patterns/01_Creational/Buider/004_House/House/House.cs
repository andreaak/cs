using System.Collections;

namespace Patterns._01_Creational.Buider._004_House.House
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
