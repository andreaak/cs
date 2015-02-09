using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public class XMLElement
    {
        public string elementName;
        public string elementValue;
        private Dictionary<string, string> elementAttributes;
        private List<XMLElement> elements;
        //
        public XMLElement(string name)
        {
            elementName = name;
            elementAttributes = new Dictionary<string, string>();
            elements = new List<XMLElement>();
        }
        //
        public XMLElement(string name, string value)
            : this(name)
        {
            elementValue = value;
        }        
        //
        public void AddAttribute(string name, string value)
        {
            elementAttributes.Add(name, value);
        }        
        //
        public void AddElement(XMLElement element)
        {
            elements.Add(element);
        }    
        //
        public Dictionary<string, string> GetAttributes()
        {
            return elementAttributes;
        }        
    
        //
        public List<XMLElement> GetElements()
        {
            return elements;
        }  
        //
        public List<XMLElement> GetElements(string name, string attributeName, string attributeValue)
        {

            if (name != string.Empty && attributeName != string.Empty && attributeValue != string.Empty)
            {
                var valueQuery = from n in elements
                                 where n.elementName == name
                                 && n.GetAttributes().Keys.Contains(attributeName) == true
                                 && (n.GetAttributes())[attributeName] == attributeValue
                                 select n;
                return valueQuery.ToList<XMLElement>(); 
            }
            else if (name != string.Empty && attributeName != string.Empty)
            {
                var attributeQuery = from n in elements
                                     where n.elementName == name
                                     && n.GetAttributes().Keys.Contains(attributeName) == true
                                     select n;
                return attributeQuery.ToList<XMLElement>(); 

            }
            else if (name != string.Empty)
            {
                var elementQuery = from n in elements
                                   where n.elementName == name
                                   select n;
                return elementQuery.ToList<XMLElement>();
            }

            return null;
        }   
    }
}
