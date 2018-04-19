using System.Collections.Generic;
using System.Text;
using System.Xml;
using TextConverter.Models;

namespace TextConverter
{
    public class XMLWriteHelper
    {
        public static void WriteWords(string path, IList<WordItem> words)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = new UTF8Encoding(false);

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

        public static void WriteVerbs(string path, IList<EnVerbItem> words)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = new UTF8Encoding(false);

            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("verbs");

                foreach (var word in words)
                {
                    writer.WriteStartElement("verb");

                    writer.WriteElementString("infinitive", word.Infinitive);
                    writer.WriteElementString("infinitive_tr", word.Infinitive);

                    writer.WriteElementString("pastSimple", word.PastSimple);
                    writer.WriteElementString("pastSimple_tr", word.PastSimpleTranscription);

                    writer.WriteElementString("pastParticiple", word.PastPaticiple);
                    writer.WriteElementString("pastParticiple_tr", word.PastPaticipleTranscription);

                    writer.WriteElementString("translation", word.Translation);

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
