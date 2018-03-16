using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TextConverter
{
    public class XMLWriteHelper
    {
        public static void Write(string path, IList<WordItem> words)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("words");

                foreach (WordItem word in words)
                {
                    writer.WriteStartElement("word");

                    foreach (var lang in CSVReader.Items)
                    {
                        string value = word.GetItem(lang);
                        if(!string.IsNullOrEmpty(value))
                        {
                            writer.WriteElementString(lang, value);
                        }
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
