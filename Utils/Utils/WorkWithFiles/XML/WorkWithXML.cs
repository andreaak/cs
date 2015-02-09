using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Collections;
using System.Xml.Linq;
using System.Linq;

namespace Utils
{
    public static class WorkWithXML
    {
        static XDocument xDocument;
        static XmlWriter xmlWr;
        static string path;
        //
        public static bool OpenFile(string filePath)
        {
            CloseFile();
            bool isOk = false;
            if (filePath == "" || !File.Exists(filePath))
            {
                return isOk;
            }
            //
            try
            {
                xDocument = XDocument.Load(filePath);
                path = filePath;
                isOk = true;
            }
            catch (Exception)
            {
            	
            }
            return isOk;
        }
        //
        public static void SaveFile()
        {
            if (path != string.Empty)
            {
                xDocument.Save(path);
            }
        }
        //
        public static void CloseFile()
        {
            path = string.Empty;
            xDocument = null;
        }        
        //
        public static IEnumerable<XElement> ReadElement(string elementName)
        {
            if (xDocument == null)
            {
                return null;
            }
            IEnumerable<XElement> elements =
                from e in xDocument.Descendants(elementName)
                select e;

            return elements;
        }
        //
        public static IEnumerable<XElement> ReadElement(string elementName, string attributeName)
        {
            if (xDocument == null)
            {
                return null;
            }
            IEnumerable<XElement> elements =
                from e in xDocument.Descendants(elementName)
                where ((string)e.Attribute(attributeName) != null)
                select e;

            return elements;
        }        
        //
        public static IEnumerable<XElement> ReadElement(string elementName, string attributeName, string attributeValue)
        {
            if (xDocument == null)
            {
                return null;
            }
            IEnumerable<XElement> elements =
                from e in xDocument.Descendants(elementName)
                where (
                    (string)e.Attribute(attributeName) != null
                    && e.Attribute(attributeName).Value == attributeValue
                )
                select e;

            return elements;
        }
        //
        public static int DeleteElement(string elementName, string attributeName, string attributeValue)
        {
 
            IEnumerable<XElement> temp = null;
            int deleted = 0;
            if (elementName != string.Empty && attributeName != string.Empty && attributeValue != string.Empty)
            {
                temp = ReadElement(elementName, attributeName, attributeValue);
            }
            else if (elementName != string.Empty && attributeName != string.Empty)
            {
                temp = ReadElement(elementName, attributeName);
            }
            else if (elementName != string.Empty)
            {
                temp = ReadElement(elementName);
            }
            //
            deleted = temp.Count();

            temp.Remove<XElement>();
            
            return deleted;
        }
        //
        public static string normalize(string data)
        {
            if (data != null)
            {
                data = data.Replace('\n', ' ');
                data = data.Replace("  ", " ");
                char[] removechar = { ';' };
                return data.Trim();
            }
            return "";
        }
        //
        public static string normalize(XAttribute data)
        {
            
            if (data != null)
            {
                String DataStr;
                DataStr = data.Value.Replace('\n', ' ');
                DataStr = DataStr.Replace("  ", " ");
                char[] removechar = { ';' };
                //return (data.TrimEnd(removechar)).Trim();
                return DataStr.Trim();
            }
            return "";
        }
        //
        public static bool OpenWriter(string filePath)
        {
            try
            {
                xmlWr = XmlWriter.Create(filePath);
                xmlWr.WriteStartDocument();
            }
            catch (Exception)
            {
                CloseWriter();
                return false;
            }
            return true;
        }
        //
        public static void CloseWriter()
        {
            if (xmlWr == null)
            {
                return;
            }
            xmlWr.WriteEndDocument();
            xmlWr.Flush();
            xmlWr.Close();
        }
        //
        public static void WriteElement(XMLElement element)
        {
            if (xmlWr == null)
            {
                return;
            } 
            xmlWr.WriteStartElement(element.elementName);
            Dictionary<string, string> attributes = element.GetAttributes();
            foreach (string attributeName in attributes.Keys)
            {
                xmlWr.WriteAttributeString(attributeName, attributes[attributeName]);
            }
            if (element.elementValue != string.Empty)
            {
                xmlWr.WriteString(element.elementValue);
            }
            foreach (XMLElement element_ in element.GetElements())
            {
                WriteElement(element_);
            }
            xmlWr.WriteEndElement();
        }

    }
}