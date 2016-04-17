using System.Collections;

namespace Patterns._03_Behavioral.Visitor._001_Base
{
    class ObjectStructure
    {
        ArrayList elements = new ArrayList();

        public void Add(Element element)
        {
            elements.Add(element);
        }

        public void Remove(Element element)
        {
            elements.Remove(element);
        }

        public void Accept(Visitor visitor)
        {
            foreach (Element element in elements)
                element.Accept(visitor);
        }
    }
}
